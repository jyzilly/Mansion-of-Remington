using UnityEngine;

public class TapeTwoPictureController : MonoBehaviour
{
    [SerializeField] private GameObject TapeTwoPlane;
    [SerializeField] private GameObject TapeTwoPicture;
    [SerializeField] private GameObject TapeTwoTagPlace;


    private void OnTriggerEnter(Collider other)
    {
        //�´� ����������Ʈ�ϰ� �ε�ĥ �� �ν��ϴ� �͵��ϰ� ���Ƴ��� �͵� ��ȭ��ȭ ��Ų��. �տ� �ִ� ����, ���� �׸�, Collider
        if (other.gameObject.tag == "TapeTwoTagPlace")
        {
            TapeTwoPlane.SetActive(false);
            TapeTwoPicture.SetActive(false);
            TapeTwoTagPlace.SetActive(false);
        }
    }
}
