using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerCameraController))]
public class CameraScreen : MonoBehaviour
{
    public delegate void CameraColliderSwitchDelegate();

    private CameraColliderSwitchDelegate camColliderSwitchCallback = null;

    public CameraColliderSwitchDelegate CamColliderSwitchCallback
    {
        get { return camColliderSwitchCallback; }
        set { camColliderSwitchCallback = value; }
    }

    [SerializeField]
    private MeshRenderer screenMR = null;
    [SerializeField]
    private RenderTexture presentScreen = null;
    [SerializeField]
    private RenderTexture pastScreen = null;

    private PlayerCameraController playerControl;
    

    private bool isPast = false;

    public bool IsPast
    {
        get { return isPast; }
    }
    private void Awake()
    {
        playerControl = GetComponent<PlayerCameraController>();
    }
    private void Start()
    {
        playerControl.ScreenChangeCallback += ScreenChange;
        isPast = false;
        screenMR.material.SetTexture("_BaseMap", presentScreen);
        screenMR.material.SetTexture("_EmissionMap", presentScreen);
    }

    private void ScreenChange()
    {
        Debug.Log("CameraScreen Change!");
        isPast = !isPast;
        ChangeRenderTex();
        CamColliderSwitchCallback?.Invoke();
    }

    private void ChangeRenderTex()
    {
        if(!isPast)
        {
            screenMR.material.SetTexture("_BaseMap", presentScreen);
            screenMR.material.SetTexture("_EmissionMap", presentScreen);
        }
        else if(isPast)
        {
            screenMR.material.SetTexture("_BaseMap", pastScreen);
            screenMR.material.SetTexture("_EmissionMap", pastScreen);
        }
        else
        {
            Debug.Log("이상한 렌더 텍스쳐 적용중");
        }
    }
}
