using UnityEngine;

public class Crank : MonoBehaviour
{
    //도르레 가지고 그 큐브랑 부딪치는 순간 손에 있는 도르레가 비활성화되고
    //큐브에 있는 도를레 활성화 시킨다.

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
