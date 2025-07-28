using UnityEngine;

public class DebugGizmoDraw : MonoBehaviour
{
    private void OnDrawGizmos()
    {
        // ���� X�� (����)
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + transform.right);

        // ���� Y�� (�ʷ�)
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + transform.up);

        // ���� Z�� (�Ķ�)
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, transform.position + transform.forward);
    }
}
