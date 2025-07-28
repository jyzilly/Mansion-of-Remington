using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.XR.Content.Interaction;

public class Cube1Levers : MonoBehaviour
{
    //4���� ���� ����
    [SerializeField] private GameObject[] levers; 

    private float[] leverXValues;
    private float[] leverZValues;

    private int curindex0 = 0;
    private int curindex1 = 0;
    private int curindex2 = 0;
    private int curindex3 = 0;

    //����
    public bool TheLeverResult = false;

    private void Start()
    {
        leverXValues = new float[4];
        leverZValues = new float[4];
    }


    private void Update()
    {
        SaveTheValue();

        //4���� ���������� ��� Check
        if (leverXValues[3] != 0f && !TheLeverResult)
        {
            CheckTheResult();
        }
    }


    //������ ����
    private void SaveTheValue()
    {
        for (int i = 0; i < levers.Length; i++)
        {
            //leverXValues[i] = levers[i].AngleX;
            //leverZValues[i] = levers[i].AngleZ;
        }
    }

    //�� : x == 60 ~ 65 
    //�� : x == -60 ~ -65
    //�� : y == 60 ~ 65
    //�� : y == -60 ~ -65
    //���� : x,y  == 35 ~ 45
    //��� : x == 35 ~ 45 , y == -65 ~ -75
    //���� : x == -35 ~ -45, y == 35 ~ 45
    //�Ͽ� : x == -35 ~ -45, y == -65 ~ -75

    //���Ϸ� ������ ����
    //�� : x == 300 ~ 335
    //�� : x == 25 ~ 60
    //�� : z == 300 ~ 335
    //�� : z == 25 ~ 60

    //���� �� �� �� �� 
    private void CheckTheResult()
    {
        Debug.Log("CheckTheResult ������");

        if (leverXValues[0] >= 300f && leverXValues[0] <= 335f) curindex0 = 1;
        else curindex0 = 0;

        if (leverXValues[1] <= 60f && leverXValues[1] >= 25f) curindex1 = 1;
        else curindex1 = 0;

        if (leverZValues[3] <= 335f && leverZValues[3] >= 300f) curindex2 = 1;
        else curindex2 = 0;

        if (leverZValues[2] >= 25f && leverZValues[2] <= 60f) curindex3 = 1;
        else curindex3 = 0;

        if (curindex0 == 1 && curindex1 == 1 && curindex2 == 1 && curindex3 == 1)
        {
            Debug.Log("����");
            TheLeverResult = true;

            //���� �������� ���ϰ� ���´�. (�� ������ XRJoystick ��ũ��Ʈ ��Ȱ��ȭ)
            //foreach (Cube1LeversController lever in levers)
            //{
            //    //lever.gameObject.GetComponent<XRJoystick1>().enabled = false;
            //}
        }
    }

}
