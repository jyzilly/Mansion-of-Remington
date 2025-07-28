using UnityEngine;

public class Crank : MonoBehaviour
{
    //������ ������ �� ť��� �ε�ġ�� ���� �տ� �ִ� �������� ��Ȱ��ȭ�ǰ�
    //ť�꿡 �ִ� ������ Ȱ��ȭ ��Ų��.

    [SerializeField] private GameObject Crank1;
    [SerializeField] private GameObject Crank2;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "InputSencor")
        {
            Crank1.SetActive(false);
            Crank2.SetActive(true);
        }
    }

}
