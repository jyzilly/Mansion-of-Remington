using System.Collections;
using UnityEngine;
using Photon.Pun;

public class AnimalBoard : MonoBehaviourPun
{
    public delegate void AnimalBoardDelegate(string _animal);
    public AnimalBoardDelegate animalBtnCallback;

    [SerializeField]
    private GameObject rotateGo; // ���� ��ġ
    [SerializeField]
    private float rotateTime; // �����µ� �ɸ��� �ð�

    private GResponse res;

    private void Awake()
    {
        res = GetComponent<GResponse>();
    }

    private void Start()
    {
        res.OnResponseCallback += Interaction;
    }
    

    // ���� ���� ��ġ�� Ǯ������ �߻��ϴ� �Լ�
    private void Interaction(bool _state)
    {
        // ��ġ�� ���ư�����
        StartCoroutine(RotateGoCoroutine());
    }

    // 1�ʵ��� 180�� ����
    private IEnumerator RotateGoCoroutine()
    {
        float elapseTime = 0f;

        while (elapseTime < rotateTime)
        {
            elapseTime += Time.deltaTime;

            rotateGo.transform.Rotate((180f * Time.deltaTime) / rotateTime, 0f, 0f);

            yield return null;
        }
    }

    // ��Ű ��ư ��������
    public void PressMonkeyBtn()
    {
        photonView.RPC("PressMonkeyBtnRPC", RpcTarget.Others);
    }

    // �� ��ư ��������
    public void PressMouseBtn()
    {
        photonView.RPC("PressMouseBtnRPC", RpcTarget.Others);
    }

    [PunRPC]
    private void PressMonkeyBtnRPC()
    {
        animalBtnCallback?.Invoke("Monkey");
    }

    [PunRPC]
    private void PressMouseBtnRPC()
    {
        animalBtnCallback?.Invoke("Mouse");
    }
}
