using UnityEngine;

public class TempPlayer : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;           // �̵� �ӵ�
    public float sprintMultiplier = 1.5f; // �޸��� �ӵ� ����

    [Header("Mouse Settings")]
    public float mouseSensitivity = 100f; // ���콺 ����
    public Transform cameraTransform;     // ī�޶� Transform (1��Ī ����)

    private CharacterController controller; // CharacterController ������Ʈ
    private float xRotation = 0f;          // ī�޶� ���� ȸ����

    void Start()
    {
        // CharacterController ��������
        controller = GetComponent<CharacterController>();

        // ���콺 Ŀ�� ����� �� ����
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        HandleMovement();
        HandleMouseLook();
    }

    private void HandleMovement()
    {
        // �̵� �Է�
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        // �̵� ���� ���
        Vector3 move = transform.right * moveX + transform.forward * moveZ;

        // �̵� �ӵ� ���
        float speed = moveSpeed;
        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed *= sprintMultiplier; // �޸���
        }

        controller.Move(move * speed * Time.deltaTime);
    }

    private void HandleMouseLook()
    {
        // ���콺 �Է�
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // ī�޶� ���� ȸ�� ���
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); // ���� ȸ�� ����

        // ī�޶� �� �÷��̾� ȸ��
        cameraTransform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }
}
