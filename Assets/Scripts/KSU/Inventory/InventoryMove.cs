using UnityEngine;
using UnityEngine.InputSystem;

public class InventoryMove : MonoBehaviour
{
    public InputActionProperty bButtonPressAction;
    
    public GameObject uiElement;
    public float distance = 5f;   
    private bool isUIVisible = false;  
    public Camera playerCamera; 
    private Vector3 uiFixedPosition;

    private void OnEnable()
    {
        bButtonPressAction.action.performed += OnBButtonPressed;
        bButtonPressAction.action.Enable();
    }

    private void OnDisable()
    {
        bButtonPressAction.action.performed -= OnBButtonPressed;
        bButtonPressAction.action.Disable();
    }

    void Start()
    {
        // ó������ UI�� ��Ȱ��ȭ
        uiElement.SetActive(false);
    }

    // B��ư ��������
    private void OnBButtonPressed(InputAction.CallbackContext context)
    {
        // �κ��丮�� Ŵ.
        ToggleUI();
    }

    // UI�� ���̰ų� ����� �Լ�
    void ToggleUI()
    {
        isUIVisible = !isUIVisible;
        uiElement.transform.parent = null;

        if (isUIVisible)
        {
            // ������ 30��
            Vector3 rightDirection = (playerCamera.transform.right + playerCamera.transform.forward + playerCamera.transform.forward).normalized;

            // ���ϴ� �Ÿ���ŭ ��������
            uiFixedPosition = playerCamera.transform.position + rightDirection * distance;

            // �ش� ��ġ�� �̵�
            uiElement.transform.position = uiFixedPosition;

            // ui�� ī�޶� �ٶ󺸰� ȸ��
            uiElement.transform.rotation = Quaternion.LookRotation(uiElement.transform.position - playerCamera.transform.position);

            // ui�� Ŵ.
            uiElement.SetActive(true);
        }
        else
        {
            uiElement.SetActive(false);
        }
    }

}

