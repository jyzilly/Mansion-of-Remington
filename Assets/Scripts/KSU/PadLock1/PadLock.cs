using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PadLock : MonoBehaviour
{
    public delegate void PadClearCallback();
    public PadClearCallback padClear; // clear했을때 콜백되는 함수.

    public TMP_Text text;  // TMP_Text 컴포넌트를 연결
    public List<PadInteraction> padInteractions;
    public string password;
    private int currentValue = 0; // 숫자의 비트값 (초기값 0000)
    public bool clear = false;


    private void Start()
    {
        text.text = "0000";

        foreach (PadInteraction pad in padInteractions)
        {
            pad.padCallback += IncrementValue;
        }
    }

    private void Update()
    {
        if (text.text == password && !clear)
        {
            clear = true;
            padClear?.Invoke();
        }
    }

    // 지정된 자리에 해당하는 값을 증가시키는 함수
    private void IncrementValue(string _name)
    {
        int positionValue = 0;

        Debug.Log(_name);

        if (_name == "Pad1")
        {
            Debug.Log(1);
            positionValue = 1000;
        }
        else if (_name == "Pad2")
        {
            Debug.Log(2);
            positionValue = 0100;
        }
        else if (_name == "Pad3")
        {
            Debug.Log(3);
            positionValue = 0010;
        }
        else if (_name == "Pad4")
        {
            Debug.Log(4);
            positionValue = 0001;
        }

        // 각 자리를 독립적으로 증가시키기 위해 각 자리의 숫자를 추출
        int thousands = (currentValue / 1000) % 10; // 첫 번째 자리 (1000 자리)
        int hundreds = (currentValue / 100) % 10;   // 두 번째 자리 (100 자리)
        int tens = (currentValue / 10) % 10;        // 세 번째 자리 (10 자리)
        int ones = currentValue % 10;               // 네 번째 자리 (1 자리)

        // positionValue에 따라 해당 자리 값을 증가시킴
        if (positionValue == 1000)
        {
            thousands = (thousands + 1) % 10; // 첫 번째 자리 증가
        }
        else if (positionValue == 0100)
        {
            hundreds = (hundreds + 1) % 10;  // 두 번째 자리 증가
        }
        else if (positionValue == 0010)
        {
            tens = (tens + 1) % 10;          // 세 번째 자리 증가
        }
        else if (positionValue == 0001)
        {
            ones = (ones + 1) % 10;          // 네 번째 자리 증가
        }

        // 새로운 값을 합쳐서 currentValue로 설정
        currentValue = thousands * 1000 + hundreds * 100 + tens * 10 + ones;

        // 텍스트를 4자리 숫자로 표시 (예: 0000, 1000, 0100)
        text.text = currentValue.ToString("D4");
    }
}