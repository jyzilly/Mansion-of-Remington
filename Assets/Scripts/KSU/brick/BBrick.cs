using UnityEngine;
using Photon.Pun;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class BBrick : MonoBehaviourPun
{
    public float maxDistance;
    public WBrick linkWBrick;
    public WBrick linkWBrick2;
    private Vector3 startPos = Vector3.zero;
    private Quaternion initialRotation;
    private bool isGrab;        
    private Transform handTr;
    private XRGrabInteractable grab;

    private void Start()
    {
        startPos = transform.position;
        initialRotation = transform.rotation;
        grab = GetComponent<XRGrabInteractable>();
    }

    private void Update()
    {
        // �ҳ��϶��� ����
        if (PhotonNetwork.LocalPlayer.CustomProperties.ContainsKey("Role") && PhotonNetwork.LocalPlayer.CustomProperties["Role"].ToString() == "Boy")
        {
            Debug.Log("�ҳ� ������" + gameObject);

            Vector3 changePos = startPos - transform.position;

            // ��ȭ���� 0�϶��� �Է� �ȹ���
            if (changePos == Vector3.zero) return;

            // ���� ������ ��ȭ������ ��ȯ�ؼ� ����.
            Vector3 worldMovement = transform.TransformDirection(changePos);

            // ���� ����
            transform.rotation = initialRotation;

            // �Ÿ� ����
            SetMaxDis();

            // ����� ���� �̵�
            linkWBrick.MoveWBrick(worldMovement);
            linkWBrick2.MoveWBrick(worldMovement);
        }

        if (isGrab)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, handTr.position.z);
        }
    }

    // ���� �Ÿ� �����ϴ� �Լ�
    private void SetMaxDis()
    {
        float distanceFromStart = Vector3.Distance(startPos, transform.position);

        // ���� �Ÿ��� ����������
        if (distanceFromStart > maxDistance)
        {
            Vector3 direction = transform.position - startPos;
            direction = direction.normalized * maxDistance;
            transform.position = startPos + direction;
        }
    }

    // ���� �ڵ��� ��ġ���� �����ͼ�
    // �� ���� Ư�� ��ǥ�� ����(exit �ɶ�����)
    
    // �׷��� ������
    public void OnGrab(SelectEnterEventArgs args)
    {
        isGrab = true;

        // ���� ������ ������.
        handTr = args.interactorObject.transform;

    }

    // �׷��� ������
    public void OffGrab(SelectExitEventArgs args)
    {
        isGrab = false;

        // ���� ������ null��
        handTr = null;
    }
}
