using UnityEngine;

public class TapeOnePictureController : MonoBehaviour
{
    [Header("Default Settings")]
    [SerializeField] private GameObject TapeOnePlane;
    [SerializeField] private GameObject TapeOnePicture;
    [SerializeField] private GameObject TapeOneTagPlace;


    private void OnTriggerEnter(Collider other)
    {
        //�´� ����������Ʈ�ϰ� �ε�ĥ �� �ν��ϴ� �͵��ϰ� ���Ƴ��� �͵� ��ȭ��ȭ ��Ų��. �տ� �ִ� ����, ���� �׸�, Collider
        if (other.gameObject.tag == "TapeOneTagPlace")
        {
            TapeOnePlane.SetActive(false);
            TapeOnePicture.SetActive(false);
            TapeOneTagPlace.SetActive(false);
        }
    }

}
