using UnityEngine;

public class TestPlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;  // �̵� �ӵ�
    public float rotationSpeed = 700f;  // ȸ�� �ӵ� (���콺�� ���� ȸ��)

    void Update()
    {
        // �̵��� ���� Ű���� �Է� �ޱ�
        float horizontal = Input.GetAxis("Horizontal");  // A/D �Ǵ� Left/Right ȭ��ǥ
        float vertical = Input.GetAxis("Vertical");      // W/S �Ǵ� Up/Down ȭ��ǥ

        // �̵� ���� ���
        Vector3 moveDirection = new Vector3(horizontal, 0f, vertical).normalized;

        // �̵�
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime, Space.World);

        // ���콺�� ���� ȸ���ϱ�
        RotateTowardsMouse();
    }

    void RotateTowardsMouse()
    {
        // ���콺 ��ġ�� ���� ��ǥ�� ��ȯ
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 10f; // ī�޶���� �Ÿ� ���� (�� ���� ī�޶��� Z���� ���� �ٸ� �� ����)
        Vector3 worldMousePos = Camera.main.ScreenToWorldPoint(mousePos);

        // �÷��̾�� ���콺 ���� ���� ���� ���
        Vector3 direction = worldMousePos - transform.position;
        direction.y = 0f;  // Y�� ȸ�� ����

        // ȸ��
        if (direction.sqrMagnitude > 0.1f)  // ���� �������� ȸ������ ����
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }
}

