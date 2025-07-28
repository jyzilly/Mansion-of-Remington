using UnityEngine;

public class BongTagPlaceManager : MonoBehaviour
{

    public bool TheResult = false;
    [SerializeField] private BongController bongcontroller;
    [SerializeField] private PictureBongController picturebongcontroller;
    [SerializeField] private GameObject Fuse;

    private void Update()
    {
        if (TheResult == false)
        {
            //비교연산자로 결과를 판단, 성공하면 결과 ture로 바꾸고, 아이템 fuse 활성화 시킨다. 
            if (bongcontroller.TheBongOnTouch == true && picturebongcontroller.TheBongOnTouch == true)
            {
                TheResult = true;
                Fuse.SetActive(true);
                Debug.Log("동시 접촉");
            }
        }

    }
}
