using Unity.VRTemplate;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class PutObject : MonoBehaviour
{
    [SerializeField]
    [Tooltip("오브젝트 붙일 위치")]
    private Transform detectPos;
    [SerializeField]
    [Tooltip("탐지 범위(박스 콜라이더)")]
    private Vector3 detectRange;
    [SerializeField]
    [Tooltip("탐지할 오브젝트 이름")]
    private string detectGOName;
    [SerializeField]
    [Tooltip("오브젝트 붙였을때 각도")]
    private Vector3 detectAngle;
    [SerializeField]
    [Tooltip("탐지할 물체의 layer")]
    private LayerMask detectLayer;

    private bool detected = false;

    private void Update()
    {
        if (!detected)
        {
            Detect();
        }
    }

    // 감지를 하면 물체 위치를 옮기는 함수
    private void Detect()
    {
        Collider[] detectColliders = Physics.OverlapBox(detectPos.position, detectRange, Quaternion.identity, detectLayer);

        foreach (Collider collider in detectColliders)
        {
            if (collider.name == detectGOName)
            {
                GameObject targetGo = collider.gameObject;

                // 그랩 비활성화
                targetGo.GetComponent<XRGrabInteractable>().enabled = false;

                // 위치를 옮김.
                targetGo.transform.position = detectPos.position;

                // 회전값
                targetGo.transform.rotation = Quaternion.Euler(detectAngle);

                // 중력 off
                targetGo.GetComponent<Rigidbody>().useGravity = false;

                // 키네마틱 on
                targetGo.GetComponent<Rigidbody>().isKinematic = true;

                // 상호작용되던 물리작용 off
                targetGo.GetComponent<Rigidbody>().linearVelocity = Vector3.zero;

                // transform 위치 고정
                targetGo.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ;

                // 그랩해서 돌릴수 있게 설정하는 부분도 필요할듯
                targetGo.GetComponent<XRKnob>().enabled = true;

                // 탐지됨.(update 호출 off)
                detected = true;

                targetGo.GetComponent<KeyInteraction>().inserted = true;
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (detectPos == null) return;

        // detectrange만큼 빨간색으로 보이게
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(detectPos.position, detectRange);

        // detectpos위치를 보여줌
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(detectPos.position, 0.1f);
    }
}
