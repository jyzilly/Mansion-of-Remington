using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class KeyBoard : MonoBehaviour
{
    public TMP_InputField InputTheText;
    //소문자 버튼
    public GameObject NormalButton;
    //대문자 버튼
    public GameObject CapsButton;
    //대소문자 전환키
    private bool caps;


    //정답들
    private string TheLoginResult = "1234";
    private string TheCCTVResult = "1234";
    private string TheCCTVYearResult = "19780416";
    private string TheMailResult = "1234";
    private string TheMail2Result = "2495";

    private int curStep = 0;

    private void Start()
    {
        caps = false;
    }

    //각 문자 키의 OnClick() 이벤트에 연결
    public void InsertChar(string C)
    {
        InputTheText.text += C;
    }

    //백스페이스 기능, 이벤트 호출
    public void DeleteChar()
    {
        if(InputTheText.text.Length > 0)
        {
            InputTheText.text = InputTheText.text.Substring(0, InputTheText.text.Length - 1);
        }
    }

    //스페이스 기능, 이벤트 호출
    public void InputSpace()
    {
        InputTheText.text += " ";
    }

    //대소문자 전화 기능, 이벤트 호출
    public void CapsPressed()
    {
        if(!caps)
        {
            NormalButton.SetActive(false);
            CapsButton.SetActive(true);
            caps = true;
        }
        else
        {
            CapsButton.SetActive(false);
            NormalButton.SetActive(true);
            caps = false;
        }
    }

    //Enter키 기능, 이벤트 호출 
    public void EnterKey()
    {

        if (curStep == 0)
        {

        }
        else if (curStep == 2)
        {

        }
        else if(curStep == 3)
        {

        }
    }
    


}
