using Unity.VRTemplate;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class PutObject : MonoBehaviour
{
    [SerializeField]
    [Tooltip("������Ʈ ���� ��ġ")]
    private Transform detectPos;
    [SerializeField]
    [Tooltip("Ž�� ����(�ڽ� �ݶ��̴�)")]
    private Vector3 detectRange;
    [SerializeField]
    [Tooltip("Ž���� ������Ʈ �̸�")]
    private string detectGOName;
    [SerializeField]
    [Tooltip("������Ʈ �ٿ����� ����")]
    private Vector3 detectAngle;
    [SerializeField]
    [Tooltip("Ž���� ��ü�� layer")]
    private LayerMask detectLayer;

    private bool detected = false;

    private void Update()
    {
        if (!detected)
        {
            Detect();
        }
    }

    // ������ �ϸ� ��ü ��ġ�� �ű�� �Լ�
    private void Detect()
    {
        Collider[] detectColliders = Physics.OverlapBox(detectPos.position, detectRange, Quaternion.identity, detectLayer);

        foreach (Collider collider in detectColliders)
        {
            if (collider.name == detectGOName)
            {
                GameObject targetGo = collider.gameObject;

                // �׷� ��Ȱ��ȭ
                targetGo.GetComponent<XRGrabInteractable>().enabled = false;

                // ��ġ�� �ű�.
                targetGo.transform.position = detectPos.position;

                // ȸ����
                targetGo.transform.rotation = Quaternion.Euler(detectAngle);

                // �߷� off
                targetGo.GetComponent<Rigidbody>().useGravity = false;

                // Ű�׸�ƽ on
                targetGo.GetComponent<Rigidbody>().isKinematic = true;

                // ��ȣ�ۿ�Ǵ� �����ۿ� off
                targetGo.GetComponent<Rigidbody>().linearVelocity = Vector3.zero;

                // transform ��ġ ����
                targetGo.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ;

                // �׷��ؼ� ������ �ְ� �����ϴ� �κе� �ʿ��ҵ�
                targetGo.GetComponent<XRKnob>().enabled = true;

                // Ž����.(update ȣ�� off)
                detected = true;

                targetGo.GetComponent<KeyInteraction>().inserted = true;
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (detectPos == null) return;

        // detectrange��ŭ ���������� ���̰�
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(detectPos.position, detectRange);

        // detectpos��ġ�� ������
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(detectPos.position, 0.1f);
    }
}
