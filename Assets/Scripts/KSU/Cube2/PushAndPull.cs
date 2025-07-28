using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class PushAndPull : MonoBehaviour
{
    [SerializeField]
    [Tooltip("������Ʈ ���� ��ġ")]
    private Transform detectPos;
    [SerializeField]
    [Tooltip("������Ʈ �ٿ����� ����")]
    private Vector3 detectAngle;
    [SerializeField]
    [Tooltip("Ž�� ����(�ڽ� �ݶ��̴�)")]
    private Vector3 detectRange;
    [SerializeField]
    [Tooltip("Ž���� ������Ʈ�� �̸�")]
    private string[] detectGONames;
    [SerializeField]
    [Tooltip("Ž���� ��ü�� layer")]
    private LayerMask detectLayer;
    [SerializeField]
    [Tooltip("��ġ�� �־����� �׷� ��Ȱ��ȭ �ð�")]
    private float enabledGrabTime;
    [SerializeField]
    [Tooltip("�ٽ� ���������� Ž���ϱ���� Ȱ��ȭ�ð�")]
    private float enalbeDetectTime;

    [HideInInspector]
    public GameObject curGO; // ���� �� �ִ� ���ӿ�����Ʈ
    private XRGrabInteractable curGrab; // ���� ������Ʈ�� grab��ũ��Ʈ

    private bool isPush = false; // ���� �� �ִ��� ����
    private bool pullCoroutineOnce = false;

    private void Update()
    {
        if (!isPush)
        {
            Push();
        }

        // �ٽ� ����������(isPush�� true�� ���¿��� �ش� ������Ʈ�� grab������)
        // curgo -> null, isPush�� ���ʵ� false�� �ٲٱ�
        if (curGO == null) return;
        if (curGrab.isSelected && isPush)
        {
            if (pullCoroutineOnce) return;
            StartCoroutine(ChangePullCoroutine());
        }
    }

    // Push(�ֱ�)
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

                    // �׷� ��Ȱ��ȭ �� ������ �ð� �� Ȱ��ȭ
                    StartCoroutine(EnabledGrabCoroutine(targetGo));

                    // ��ġ�� �ű�.
                    targetGo.transform.position = detectPos.position;
                    targetGo.transform.eulerAngles = detectAngle;

                    // �߷� off
                    // targetGo.GetComponent<Rigidbody>().useGravity = false;

                    // ��ȣ�ۿ�Ǵ� �����ۿ� off
                    targetGo.GetComponent<Rigidbody>().linearVelocity = Vector3.zero;

                    // transform ��ġ ����
                    targetGo.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ | 
                        RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;

                    // Ž����.(update ȣ�� off)
                    isPush = true;

                    // �Լ� ������
                    return;
                }
            }
        }
    }

    // ���� �ð��� �׷� Ȱ��ȭ
    private IEnumerator EnabledGrabCoroutine(GameObject _target)
    {
        _target.GetComponent<XRGrabInteractable>().enabled = false;

        yield return new WaitForSeconds(enabledGrabTime);

        _target.GetComponent<XRGrabInteractable>().enabled = true;
    }

    // ���� �ð��� isPush �� false
    private IEnumerator ChangePullCoroutine()
    {
        pullCoroutineOnce = true;

        yield return new WaitForSeconds(enalbeDetectTime);

        curGO.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;

        isPush = false;
        curGO = null;

        pullCoroutineOnce = false;
    }

    // detect ������ ������.
    private void OnDrawGizmos()
    {
        // detectrange��ŭ ���������� ���̰�
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(detectPos.position, detectRange);

        // detectpos��ġ�� ������
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(detectPos.position, 0.1f);
    }
}
