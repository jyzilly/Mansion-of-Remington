using UnityEngine;


//�� ���� ����� �����ͼ� �񱳿����ڷ� ���������ߴ��� �Ǵ�, �׸��� �������˽� �����ϴ� ������Ʈ�� Ȳ��ȭ ��Ű��

public class BongManager : MonoBehaviour
{
    //[Header("default setting")]
    //[SerializeField] private BongController bongcontroller;
    //[SerializeField] private PictureBongController picturebongcontroller;
    [Header("bong 2 and bongPlane")]
    [SerializeField] private GameObject BongPlane;
    [SerializeField] private GameObject BongTagPlace;
    [SerializeField] private GameObject BongPicture;

    //public bool TheResult = false;

    //[SerializeField] private GameObject Fuse;

    private void Update()
    {
        //if(TheResult == false)
        //{
        //    //�񱳿����ڷ� ����� �Ǵ�, �����ϸ� ��� ture�� �ٲٰ�, ������ fuse Ȱ��ȭ ��Ų��. 
        //    if(bongcontroller.TheBongOnTouch == true && picturebongcontroller.TheBongOnTouch == true)
        //    {
        //        TheResult = true;
        //        Fuse.SetActive(true);
        //        Debug.Log("���� ����");
        //    }
        //}
        
    }

    //������ ���׸�(bong �ִ� ��)�ϰ� �ε�ġ�� BongPlane ��Ȱ��ȭ, ���� ��Ȱ��ȭ,  Collider - BongTagPlace��Ȱ��ȭ(��ƾ� �ؼ� �տ� Collider ������ �ȵ�)
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "BongTagPlace")
        {
            BongPlane.SetActive(false);
            BongPicture.SetActive(false);
            BongTagPlace.SetActive(false);
        }
    }

}
