using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PadLock : MonoBehaviour
{
    public delegate void PadClearCallback();
    public PadClearCallback padClear; // clear������ �ݹ�Ǵ� �Լ�.

    public TMP_Text text;  // TMP_Text ������Ʈ�� ����
    public List<PadInteraction> padInteractions;
    public string password;
    private int currentValue = 0; // ������ ��Ʈ�� (�ʱⰪ 0000)
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

    // ������ �ڸ��� �ش��ϴ� ���� ������Ű�� �Լ�
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

        // �� �ڸ��� ���������� ������Ű�� ���� �� �ڸ��� ���ڸ� ����
        int thousands = (currentValue / 1000) % 10; // ù ��° �ڸ� (1000 �ڸ�)
        int hundreds = (currentValue / 100) % 10;   // �� ��° �ڸ� (100 �ڸ�)
        int tens = (currentValue / 10) % 10;        // �� ��° �ڸ� (10 �ڸ�)
        int ones = currentValue % 10;               // �� ��° �ڸ� (1 �ڸ�)

        // positionValue�� ���� �ش� �ڸ� ���� ������Ŵ
        if (positionValue == 1000)
        {
            thousands = (thousands + 1) % 10; // ù ��° �ڸ� ����
        }
        else if (positionValue == 0100)
        {
            hundreds = (hundreds + 1) % 10;  // �� ��° �ڸ� ����
        }
        else if (positionValue == 0010)
        {
            tens = (tens + 1) % 10;          // �� ��° �ڸ� ����
        }
        else if (positionValue == 0001)
        {
            ones = (ones + 1) % 10;          // �� ��° �ڸ� ����
        }

        // ���ο� ���� ���ļ� currentValue�� ����
        currentValue = thousands * 1000 + hundreds * 100 + tens * 10 + ones;

        // �ؽ�Ʈ�� 4�ڸ� ���ڷ� ǥ�� (��: 0000, 1000, 0100)
        text.text = currentValue.ToString("D4");
    }
}