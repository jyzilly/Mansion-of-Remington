using UnityEngine;

public class Cube4ChessButton : MonoBehaviour
{
    public int TheChessButton = 0;

    public bool TheCube4Result = false;

    private void Update()
    {
        TheChessButtonOnClick();
    }

    //테스트용, 논리순서 생각용도로
    //마우스 클릭을 감지하고, 클릭된 오브젝트가 체스 버튼인지 확인
    //VR 환경에서는 이 로직을 마우스가 아닌 VR 컨트롤러의 포인터에서 Ray를 발사하는 방식으로 수정해야 함

    private void TheChessButtonOnClick()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit))
            {
                if(hit.transform.gameObject.tag == "ChessButton")
                {
                    TheChessButton = 1;
                }
            }
        }
    }


    //체스판에 상호작용

}
