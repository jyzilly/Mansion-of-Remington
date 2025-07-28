using UnityEngine;

public class TempPlayer : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;           // 이동 속도
    public float sprintMultiplier = 1.5f; // 달리기 속도 배율

    [Header("Mouse Settings")]
    public float mouseSensitivity = 100f; // 마우스 감도
    public Transform cameraTransform;     // 카메라 Transform (1인칭 시점)

    private CharacterController controller; // CharacterController 컴포넌트
    private float xRotation = 0f;          // 카메라 수직 회전값

    void Start()
    {
        // CharacterController 가져오기
        controller = GetComponent<CharacterController>();

        // 마우스 커서 숨기기 및 고정
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        HandleMovement();
        HandleMouseLook();
    }

    private void HandleMovement()
    {
        // 이동 입력
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        // 이동 방향 계산
        Vector3 move = transform.right * moveX + transform.forward * moveZ;

        // 이동 속도 계산
        float speed = moveSpeed;
        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed *= sprintMultiplier; // 달리기
        }

        controller.Move(move * speed * Time.deltaTime);
    }

    private void HandleMouseLook()
    {
        // 마우스 입력
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // 카메라 수직 회전 계산
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); // 상하 회전 제한

        // 카메라 및 플레이어 회전
        cameraTransform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }
}
