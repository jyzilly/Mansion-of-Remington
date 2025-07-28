using TMPro;
using UnityEngine;

public class KeyBoardButton : MonoBehaviour
{
    private KeyBoard keyBaord;
    private TextMeshProUGUI buttonText;

    private void Start()
    {
        keyBaord = GetComponentInParent<KeyBoard>();
        buttonText = GetComponentInChildren<TextMeshProUGUI>();

        //키의 이름이 한 글자인 문자 입력 키로 간주하고 자동 설정을 진행
        if (buttonText.text.Length ==1)
        {
            NameToButtonText();
            GetComponentInChildren<ButtonVR>().onRelease.AddListener(delegate { keyBaord.InsertChar(buttonText.text); });
        }
    }

    //UI테스트 설정하는 함수, 이름표시
    private void NameToButtonText()
    {
        buttonText.text = gameObject.name;
    }

}