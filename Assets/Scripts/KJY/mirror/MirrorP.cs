using UnityEngine;


//위치로 인식해서 맞는 위치에 배치했을 때 다른 공간 생기게 되고 그 안에 버튼 있고 
//동시 누르면 정답으로 처리 
//배치한 뒤 1초 2초의 시간 가격을 줘야 함 
//위치 인식 - 매칭하는 것  - 타겟 & Trigger   
//맞는 위치에 두면 mirror & backPan 비활성화 된다.

public class MirrorP : MonoBehaviour
{
    //퍼즐이 성공했을 때 호출될 콜백 함수를 정의
    public delegate void MirroDelegate();
    public MirroDelegate mirroSucessCallback;

    [Header("Default Settings")]
    [SerializeField] private GameObject Mirror;
    [SerializeField] private GameObject MirrorBackpan;
    [SerializeField] private GameObject Wall;

    [SerializeField] private MirrorButtonController mirrorbutton;
    [SerializeField] private MirroInsideButtonController mirrorinsidebutton;

    public bool TheResult = false;

    private void Update()
    {
        //비교연산자로 동시 눌렀는지 확인
        if (mirrorbutton.TheButtonisPressed == true && mirrorinsidebutton.TheButtonisPressed == true && !TheResult)
        {
            //Debug.Log("동시 눌렀다");
            TheResult = true;
            //성공시 외부 시스템에 퍼즐이 완료되었음을 알림
            mirroSucessCallback?.Invoke();
        }
        
    }

    //거울 위치의 태그틀 통해, 거울 공간을 열리는 과정
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "TheMirrorPlace")
        {
            //Debug.Log("작동");

            Mirror.SetActive(false);
            MirrorBackpan.SetActive(false);
            Wall.SetActive(false);

            //Invoke("Mirror.SetActive(false)", 1f);
            //Invoke("Mirrorbackpan.SetActive(false)", 1f);
            //Invoke("Wall.SetActive(false)", 1f);

        }
    }


}
