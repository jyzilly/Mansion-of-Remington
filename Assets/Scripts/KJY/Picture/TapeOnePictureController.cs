using UnityEngine;

public class TapeOnePictureController : MonoBehaviour
{
    [Header("Default Settings")]
    [SerializeField] private GameObject TapeOnePlane;
    [SerializeField] private GameObject TapeOnePicture;
    [SerializeField] private GameObject TapeOneTagPlace;


    private void OnTriggerEnter(Collider other)
    {
        //맞는 사진오브젝트하고 부딪칠 때 인식하는 것들하고 막아놓은 것들 비화성화 시킨다. 손에 있는 사진, 막는 그림, Collider
        if (other.gameObject.tag == "TapeOneTagPlace")
        {
            TapeOnePlane.SetActive(false);
            TapeOnePicture.SetActive(false);
            TapeOneTagPlace.SetActive(false);
        }
    }

}
