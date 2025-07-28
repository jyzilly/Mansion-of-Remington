using UnityEngine;

public class TestPlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;  // 이동 속도
    public float rotationSpeed = 700f;  // 회전 속도 (마우스를 따라 회전)

    void Update()
    {
        // 이동을 위한 키보드 입력 받기
        float horizontal = Input.GetAxis("Horizontal");  // A/D 또는 Left/Right 화살표
        float vertical = Input.GetAxis("Vertical");      // W/S 또는 Up/Down 화살표

        // 이동 방향 계산
        Vector3 moveDirection = new Vector3(horizontal, 0f, vertical).normalized;

        // 이동
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime, Space.World);

        // 마우스를 향해 회전하기
        RotateTowardsMouse();
    }

    void RotateTowardsMouse()
    {
        // 마우스 위치를 월드 좌표로 변환
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 10f; // 카메라와의 거리 조정 (이 값은 카메라의 Z값에 따라 다를 수 있음)
        Vector3 worldMousePos = Camera.main.ScreenToWorldPoint(mousePos);

        // 플레이어와 마우스 간의 방향 벡터 계산
        Vector3 direction = worldMousePos - transform.position;
        direction.y = 0f;  // Y축 회전 제한

        // 회전
        if (direction.sqrMagnitude > 0.1f)  // 작은 움직임은 회전하지 않음
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }
}

