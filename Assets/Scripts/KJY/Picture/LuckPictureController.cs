using UnityEngine;

public class LuckPictureController : MonoBehaviour
{
    [Header("Default Settings")]
    [SerializeField] private GameObject LuckPlane;
    [SerializeField] private GameObject LuckPicture;
    [SerializeField] private GameObject LuckTagPlace;

    //�´� ����������Ʈ�ϰ� �ε�ĥ �� �ν��ϴ� �͵��ϰ� ���Ƴ��� �͵� ��ȭ��ȭ ��Ų��. �տ� �ִ� ����, ���� �׸�, Collider
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "LuckTagPlace")
        {
            LuckPlane.SetActive(false);
            LuckPicture.SetActive(false);
            LuckTagPlace.SetActive(false);
        }
    }


}
