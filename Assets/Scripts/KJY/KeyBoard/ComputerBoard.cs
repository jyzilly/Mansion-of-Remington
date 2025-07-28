using UnityEngine;
using UnityEngine.UI;

//컴퓨터 기믹
//컴퓨터 화면 구성, 키로 입력하면 컴퓨터 화면 나옴, CCTV, Mail 등 화면

public class ComputerBoard : MonoBehaviour
{
    [SerializeField] private GameObject passwordScreen;
    [SerializeField] private Image LoginImg;
    [SerializeField] private Image CCTVIMg;
    [SerializeField] private Image MailIMg;
    [SerializeField] private GameObject mainScreen;
    [SerializeField] private GameObject mailScreen;
    [SerializeField] private GameObject cctvScreen;


    [SerializeField] private Button CCTVButton;
    [SerializeField] private Button MailButton;
    [SerializeField] private Button MemoButton;

    private bool Result = false;


    private void Start()
    {
        CCTVButton.onClick.AddListener(CCTVButtonOnClick);
    }

    //초기 로그인 성공 시 호출되어 메인 화면으로 전환
    private void LoginEvent()
    {
        if(Result == true)
        {
            //비밀번호 창과 로그인 아이콘을 숨기고 메인 화면을 표시
            LoginImg.enabled = false;
            passwordScreen.SetActive(false);
            mainScreen.SetActive(true);

            //상태 플래그를 다시 false로 리셋하여 다음 비밀번호 입력을 준비
            Result = false;
        }
    }

    //CCTV 버튼 클릭 시 호출되어 비밀번호 확인 절차를 시작
    private void CCTVButtonOnClick()
    {
        mainScreen.SetActive(false);
        passwordScreen.SetActive(true);
        CCTVIMg.enabled = true;
    }

    //CCTV 비밀번호 입력 성공 시 호출되어 CCTV 화면으로 전환
    private void CCTVEvent()
    {
        if(Result == true)
        {
            CCTVIMg.enabled = false;
            passwordScreen.SetActive(false);
            cctvScreen.SetActive(true);
            Result = false;
        }
    }

}
