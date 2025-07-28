using UnityEngine;

public class LuckPictureController : MonoBehaviour
{
    [Header("Default Settings")]
    [SerializeField] private GameObject LuckPlane;
    [SerializeField] private GameObject LuckPicture;
    [SerializeField] private GameObject LuckTagPlace;

    //맞는 사진오브젝트하고 부딪칠 때 인식하는 것들하고 막아놓은 것들 비화성화 시킨다. 손에 있는 사진, 막는 그림, Collider
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
