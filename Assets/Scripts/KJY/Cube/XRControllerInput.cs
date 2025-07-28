using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Locomotion.Turning;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;

public class XRControllerInput : MonoBehaviour
{
    #region Debug
    public TextMeshProUGUI bugInfo;
    #endregion

    #region Controllers
    [Header("Controllers Component Refs")]
    public UnityEngine.XR.Interaction.Toolkit.Interactors.XRRayInteractor leftRay;
    public UnityEngine.XR.Interaction.Toolkit.Interactors.XRRayInteractor rightRay;
    
    public SnapTurnProvider snapTurnProvider;
    public ContinuousTurnProvider continuousTurnProvider;
    public DynamicMoveProvider dynamicMoveProvider;
    
    private InputDevice _leftController;
    private InputDevice _rightController;
    
    private float _originalSnapTurnAmount;
    private float _originalContinuousTurnSpeed;
    private float _originalMoveSpeed;
    
    #endregion

    #region Fields
    private bool _isLocked;
    private bool _isRightPrimaryButtonPressed;
    private bool _isLeftJoystickUsed;
    #endregion

    #region PuzzleCube
    [Header("Other refs")]
    public GameObject puzzleCube;

    public Transform leftUpJoystick;
    public Transform rightUpJoystick;
    public Transform leftDownJoystick;
    public Transform rightDownJoystick;
    private (int, int) _curJoystick;   // (row index, col index)
    private List<List<Transform>> _joySticks;
    
    // TODO outline current joystick
    #endregion
    
    private enum Direction
    {
        // four direction in 2d
        Up,
        Down,
        Left,
        Right,
        // --------------------
    }
    
    private void Start()
    {
        _leftController = InputDevices.GetDeviceAtXRNode(XRNode.LeftHand);
        _rightController = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);

        // remember the original params
        _originalSnapTurnAmount = snapTurnProvider.turnAmount;
        _originalContinuousTurnSpeed = continuousTurnProvider.turnSpeed;
        _originalMoveSpeed = dynamicMoveProvider.moveSpeed;
        
        // initialize joystick on cube
        List<Transform> firstJoystickRow = new List<Transform>() { leftUpJoystick, rightUpJoystick };
        List<Transform> secondJoystickRow = new List<Transform>() { leftDownJoystick, rightDownJoystick };
        _joySticks = new List<List<Transform>>() { firstJoystickRow, secondJoystickRow };
        _curJoystick = (0, 0);
    }

    private void Update()
    {
        HandlingControllerInput();
    }

    /// <summary>
    /// process input
    /// </summary>
    private void HandlingControllerInput()
    {
        // enum mapping
        // CommonUsages.trigger trigger button (float)
        // CommonUsages.grip	grip button (float)
        // CommonUsages.primaryButton	main button/A or X (bool)
        // CommonUsages.secondaryButton	sub button/B or Y (bool)
        // CommonUsages.primary2DAxis	joystick (vector2)

        bool isPressed = _isRightPrimaryButtonPressed;
        // is it the moment when the 'A'(primary button) is pressed?
        if (_rightController.TryGetFeatureValue(CommonUsages.primaryButton, out _isRightPrimaryButtonPressed) && !isPressed && _isRightPrimaryButtonPressed)
        {
            Debug.Log("Press A");
            bugInfo.text = "Press A";
            // dose right ray cast puzzleCube?
            if (rightRay.TryGetCurrent3DRaycastHit(out RaycastHit hit))
            {
                if (hit.collider.gameObject == puzzleCube)
                {
                    // reverse lock state
                    _isLocked = !_isLocked;
                    
                    // lock movement and turn
                    if (_isLocked)
                    {
                        snapTurnProvider.turnAmount = 0;
                        continuousTurnProvider.turnSpeed = 0;
                        dynamicMoveProvider.moveSpeed = 0;
                    }
                    // unlock movement and turn
                    else
                    {
                        snapTurnProvider.turnAmount = _originalSnapTurnAmount;
                        continuousTurnProvider.turnSpeed = _originalContinuousTurnSpeed;
                        dynamicMoveProvider.moveSpeed = _originalMoveSpeed;
                    }
                }
            }
        }

        if (!_isLocked)
        {
            return;
        }

        const float threshold = math.SQRT2 / 2;
        // use left joystick to choose
        if (_leftController.TryGetFeatureValue(CommonUsages.primary2DAxis, out Vector2 leftThumbstickValue) && leftThumbstickValue.magnitude >= 0.1f && !_isLeftJoystickUsed)
        {
            Debug.Log("left joystick");
            _isLeftJoystickUsed = true;
            leftThumbstickValue = leftThumbstickValue.normalized;
            
            switch (leftThumbstickValue)
            {
                case {x:>=-threshold, x:<threshold, y:>=0}: // up
                    SwitchJoystickOnCube(Direction.Up);
                    break;
                case {x:>=-threshold, x:<threshold, y:<0}: // down
                    SwitchJoystickOnCube(Direction.Down);
                    break;
                case {x:<0, y:>= -threshold, y:<threshold}: // left
                    SwitchJoystickOnCube(Direction.Left);
                    break;
                case {x:>=0, y:>= -threshold, y:<threshold}: // right
                    SwitchJoystickOnCube(Direction.Right);
                    break;
                default:
                    Debug.LogError("error");
                    break;
            }
            bugInfo.text = $"left joystick: {_curJoystick}";
        }else if (leftThumbstickValue.magnitude < 0.1f)
        {
            _isLeftJoystickUsed = false;
        }
        
        // use right joystick to rotate
        if (_rightController.TryGetFeatureValue(CommonUsages.primary2DAxis, out Vector2 rightLeftThumbstickValue) && rightLeftThumbstickValue.magnitude >= 0.1f)
        {
            Debug.Log("right joystick");
            // Get rotation from quest touch (right hand coordinate), angle => [-pi/2, pi/2]
            float xRotateAngle = (math.acos(rightLeftThumbstickValue.x) - math.PI / 2 ) * 90 / math.PI;
            float yRotateAngle = (math.acos(rightLeftThumbstickValue.y) - math.PI / 2 ) * 90 / math.PI;
            bugInfo.text = $"right joystick: \n value {rightLeftThumbstickValue} \n angle ({math.acos(rightLeftThumbstickValue.x)}) \n 360 angel ({xRotateAngle}, {yRotateAngle})";

            // Rotation cube joystick (left hand coordinate)
            Transform curCubeJoystick = _joySticks[_curJoystick.Item1][_curJoystick.Item2];
            curCubeJoystick.localRotation = Quaternion.Euler(-yRotateAngle, xRotateAngle, 0);
        }
    }
    
    /// <summary>
    /// Switch joystick by direction
    /// </summary>
    /// <param name="direction"></param>
    private void SwitchJoystickOnCube(Direction direction)
    {
        int rows = _joySticks.Count, cols = _joySticks[0].Count;
        switch (direction)
        {
            case Direction.Up:
                _curJoystick.Item1 = (_curJoystick.Item1 - 1) % rows;
                break;
            case Direction.Down:
                _curJoystick.Item1 = (_curJoystick.Item1 + 1) % rows;
                break;
            case Direction.Left:
                _curJoystick.Item2 = (_curJoystick.Item2 - 1) % cols;
                break;
            case Direction.Right:
                _curJoystick.Item2 = (_curJoystick.Item2 + 1) % cols;
                break;
            default:
                Debug.LogError("error");
                break;
        }
    }
}
