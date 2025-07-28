using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextComparer : MonoBehaviour
{
    public OpenDoor2 opendoor;

    public List<ChainTextDet> chainDetList = null;
    [SerializeField]
    private string compareStr = null;
    
    public bool IsPressed = false;
    [SerializeField]
    private string answerStr = string.Empty;

    public TextMeshPro tmp = null;

    private void Update()
    {
        if (IsPressed)
        {
            ComparePressed();
            IsPressed = false;
        }
    }
    public void ComparePressed()
    {
        foreach (ChainTextDet textDet in chainDetList)
        {
            textDet.LogRedText();
            if (textDet.onLight != string.Empty)
            {
                compareStr += textDet.onLight;
            }
        }

        if( compareStr.ToString() == answerStr )
        {
            Debug.Log(compareStr + " : " + answerStr);
            tmp.text = compareStr + "=" + answerStr;
            Debug.Log("Same!");
            // ������ �ݹ� �߰� (���� �������)
            StartCoroutine(opendoor.AnimateDoors());
        }
        else
        {
            Debug.Log(compareStr + " : " + answerStr);
            tmp.text = compareStr + "!=" + answerStr;
            Debug.Log("Different!");
            // ������ �ݹ� �߰� (�ϴ� �Ǵ��� Ȯ�� ���� �κ�)
        }
        compareStr = string.Empty;
    }
}
