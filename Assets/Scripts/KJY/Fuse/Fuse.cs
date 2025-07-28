using UnityEngine;
using UnityEngine.UI;

//ǻ���ϰ� �ε�ġ��, �տ� �ִ� �� ��Ȱ��ȭ�ǰ�, ������ ǻ�� Ȱ��ȭ ��Ų��.

public class Fuse : MonoBehaviour
{
    //ǻ�� �� �� 
    [Header("Fuse Settings")]
    [SerializeField] private GameObject fuse1;
    [SerializeField] private GameObject fuse1_1;
    //ȯ�����Ʈ
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
