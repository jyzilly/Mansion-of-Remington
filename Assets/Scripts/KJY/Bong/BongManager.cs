using UnityEngine;


//두 봉의 결과를 가져와서 비교연사자로 동시접촉했는지 판단, 그리고 동시접촉시 생성하는 프로젝트를 황성화 시키기

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
        //    //비교연산자로 결과를 판단, 성공하면 결과 ture로 바꾸고, 아이템 fuse 활성화 시킨다. 
        //    if(bongcontroller.TheBongOnTouch == true && picturebongcontroller.TheBongOnTouch == true)
        //    {
        //        TheResult = true;
        //        Fuse.SetActive(true);
        //        Debug.Log("동시 접촉");
        //    }
        //}
        
    }

    //사진을 벽그림(bong 있는 곳)하고 부딪치면 BongPlane 비활성화, 사진 비활성화,  Collider - BongTagPlace비활성화(잡아야 해서 앞에 Collider 있으면 안됨)
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
