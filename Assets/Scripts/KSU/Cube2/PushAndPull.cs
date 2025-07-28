using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class PushAndPull : MonoBehaviour
{
    [SerializeField]
    [Tooltip("오브젝트 붙일 위치")]
    private Transform detectPos;
    [SerializeField]
    [Tooltip("오브젝트 붙였을때 각도")]
    private Vector3 detectAngle;
    [SerializeField]
    [Tooltip("탐지 범위(박스 콜라이더)")]
    private Vector3 detectRange;
    [SerializeField]
    [Tooltip("탐지할 오브젝트들 이름")]
    private string[] detectGONames;
    [SerializeField]
    [Tooltip("탐지할 물체의 layer")]
    private LayerMask detectLayer;
    [SerializeField]
    [Tooltip("위치에 넣었을때 그랩 비활성화 시간")]
    private float enabledGrabTime;
    [SerializeField]
    [Tooltip("다시 가져갔을때 탐지하기까지 활성화시간")]
    private float enalbeDetectTime;

    [HideInInspector]
    public GameObject curGO; // 현재 들어가 있는 게임오브젝트
    private XRGrabInteractable curGrab; // 게임 오브젝트의 grab스크립트

    private bool isPush = false; // 현재 들어가 있는지 여부
    private bool pullCoroutineOnce = false;

    private void Update()
    {
        if (!isPush)
        {
            Push();
        }

        // 다시 가져갔을때(isPush가 true인 상태에서 해당 오브젝트가 grab됬을때)
        // curgo -> null, isPush를 몇초뒤 false로 바꾸기
        if (curGO == null) return;
        if (curGrab.isSelected && isPush)
        {
            if (pullCoroutineOnce) return;
            StartCoroutine(ChangePullCoroutine());
        }
    }

    // Push(넣기)
    private void Push()
    {
        Collider[] detectColliders = Physics.OverlapBox(detectPos.position, detectRange, Quaternion.identity, detectLayer);

        foreach (Collider collider in detectColliders)
        {
            foreach(string name in detectGONames)
            {
                if (name == collider.name)
                {
                    GameObject targetGo = collider.gameObject;

                    curGO = targetGo;
                    curGrab = targetGo.GetComponent<XRGrabInteractable>();

                    // 그랩 비활성화 후 지정한 시간 뒤 활성화
                    StartCoroutine(EnabledGrabCoroutine(targetGo));

                    // 위치를 옮김.
                    targetGo.transform.position = detectPos.position;
                    targetGo.transform.eulerAngles = detectAngle;

                    // 중력 off
                    // targetGo.GetComponent<Rigidbody>().useGravity = false;

                    // 상호작용되던 물리작용 off
                    targetGo.GetComponent<Rigidbody>().linearVelocity = Vector3.zero;

                    // transform 위치 고정
                    targetGo.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ | 
                        RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;

                    // 탐지됨.(update 호출 off)
                    isPush = true;

                    // 함수 끝내기
                    return;
                }
            }
        }
    }

    // 일정 시간후 그랩 활성화
    private IEnumerator EnabledGrabCoroutine(GameObject _target)
    {
        _target.GetComponent<XRGrabInteractable>().enabled = false;

        yield return new WaitForSeconds(enabledGrabTime);

        _target.GetComponent<XRGrabInteractable>().enabled = true;
    }

    // 일정 시간후 isPush 값 false
    private IEnumerator ChangePullCoroutine()
    {
        pullCoroutineOnce = true;

        yield return new WaitForSeconds(enalbeDetectTime);

        curGO.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;

        isPush = false;
        curGO = null;

        pullCoroutineOnce = false;
    }

    // detect 범위를 보여줌.
    private void OnDrawGizmos()
    {
        // detectrange만큼 빨간색으로 보이게
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(detectPos.position, detectRange);

        // detectpos위치를 보여줌
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(detectPos.position, 0.1f);
    }
}
