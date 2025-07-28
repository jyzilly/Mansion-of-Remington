using UnityEngine;

public class DebugGizmoDraw : MonoBehaviour
{
    private void OnDrawGizmos()
    {
        // 로컬 X축 (빨강)
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + transform.right);

        // 로컬 Y축 (초록)
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + transform.up);

        // 로컬 Z축 (파랑)
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, transform.position + transform.forward);
    }
}
