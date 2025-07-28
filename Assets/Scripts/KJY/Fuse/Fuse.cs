using UnityEngine;
using UnityEngine.UI;

//퓨지하고 부딪치면, 손에 있는 걸 비활성화되고, 장착된 퓨즈 활성화 시킨다.

public class Fuse : MonoBehaviour
{
    //퓨즈 두 개 
    [Header("Fuse Settings")]
    [SerializeField] private GameObject fuse1;
    [SerializeField] private GameObject fuse1_1;
    //환경라이트
    [Header("Light Settings")]
    [SerializeField] private GameObject TheLight;

    [Header("Next Gimmick")]
    [SerializeField] private GameObject TheDeractionObject;
    [Header("Door Picture1")]
    [SerializeField] private GameObject TheDoorPicture;
    

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag =="FusePlace")
        {
            
            fuse1.SetActive(false);
            fuse1_1.SetActive(true);
            TheDeractionObject.SetActive(true);
            TheDoorPicture.SetActive(true);

            TheLight.SetActive(true);


        }
    }


    public void TheLightCan()
    {
        //TheLight.SetActive(true);
        Debug.Log("Light");
    }


}
