using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.XR.Content.Interaction;

public class Cube1Levers : MonoBehaviour
{
    //4개의 레버 연결
    [SerializeField] private GameObject[] levers; 

    private float[] leverXValues;
    private float[] leverZValues;

    private int curindex0 = 0;
    private int curindex1 = 0;
    private int curindex2 = 0;
    private int curindex3 = 0;

    //정답
    public bool TheLeverResult = false;

    private void Start()
    {
        leverXValues = new float[4];
        leverZValues = new float[4];
    }


    private void Update()
    {
        SaveTheValue();

        //4개다 움직였으면 결과 Check
        if (leverXValues[3] != 0f && !TheLeverResult)
        {
            CheckTheResult();
        }
    }


    //각도를 저장
    private void SaveTheValue()
    {
        for (int i = 0; i < levers.Length; i++)
        {
            //leverXValues[i] = levers[i].AngleX;
            //leverZValues[i] = levers[i].AngleZ;
        }
    }

    //상 : x == 60 ~ 65 
    //하 : x == -60 ~ -65
    //좌 : y == 60 ~ 65
    //우 : y == -60 ~ -65
    //상좌 : x,y  == 35 ~ 45
    //상우 : x == 35 ~ 45 , y == -65 ~ -75
    //하좌 : x == -35 ~ -45, y == 35 ~ 45
    //하우 : x == -35 ~ -45, y == -65 ~ -75

    //오일러 각도로 변경
    //상 : x == 300 ~ 335
    //하 : x == 25 ~ 60
    //좌 : z == 300 ~ 335
    //우 : z == 25 ~ 60

    //정답 상 하 좌 우 
    private void CheckTheResult()
    {
        Debug.Log("CheckTheResult 실행중");

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
            Debug.Log("정답");
            TheLeverResult = true;

            //레버 움직이지 못하게 막는다. (각 레버의 XRJoystick 스크립트 비활성화)
            //foreach (Cube1LeversController lever in levers)
            //{
            //    //lever.gameObject.GetComponent<XRJoystick1>().enabled = false;
            //}
        }
    }

}
