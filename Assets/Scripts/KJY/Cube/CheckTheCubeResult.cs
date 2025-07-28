using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

public class CheckTheCubeResult : MonoBehaviour
{
    //4개의 스크립트의 결과값이 가져와서 4개 다 true면 판정 통과하고 중간에 네모상자 올라와서 안에 레버 들어가 있다.

    [SerializeField] private Cube1Levers Cube1;
    [SerializeField] private Cube2Sound Cube2;
    [SerializeField] private PadLock Cube3;
    [SerializeField] private SmallChessBoard Cube4;
    [SerializeField] private Cubeparticle Ani;


    private bool C1Result = false;
    private bool C2Result = false;
    private bool C3Result = false;
    private bool C4Result = false;

    private bool makeInstance1 = false;
    private bool makeInstance2 = false;
    private bool makeInstance3 = false;
    private bool makeInstance4 = false;

    [SerializeField] private Transform[] CPos;
    private GameObject[] LightPrefabs = new GameObject[4];
    [SerializeField] private GameObject LightPrefab;

    [SerializeField] private GameObject Mirror;

    [SerializeField] private int CurIndex = 0;




    private void Start()
    {
   
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            TheFinalResult();
        }

        if (CurIndex < 4)
        {
            UpdateTheCubeResult();
        }
        else if (CurIndex == 4)
        {
            TheFinalResult();
        }
        //if(CurIndex < 4)
        //{

        //    CheckTheResult();
        //}
    }

    private void UpdateTheCubeResult()
    {
        C1Result = Cube1.TheLeverResult;
        C2Result = Cube2.TheCube2Result;
        C3Result = Cube3.clear;
        C4Result = Cube4.clear;

        CheckTheResult();

    }

    private void CheckTheResult()
    {
        if(C1Result == true && !makeInstance1)
        {
            makeInstance1 = true;
            LightPrefabs[0] = Instantiate(LightPrefab, CPos[0].position, Quaternion.Euler(0f, 0f, 90f), transform);
        }
        if(C2Result == true && !makeInstance2)
        {
            makeInstance2 = true;
            LightPrefabs[1] = Instantiate(LightPrefab, CPos[1].position, Quaternion.Euler(0f, 0f, 90f), transform);
        }
        if (C3Result == true && !makeInstance3)
        {
            makeInstance3 = true;
            LightPrefabs[2] = Instantiate(LightPrefab, CPos[2].position, Quaternion.Euler(0f, 0f, 90f), transform);
        }
        if (C4Result == true && !makeInstance4)
        {
            makeInstance4 = true;
            LightPrefabs[3] = Instantiate(LightPrefab, CPos[3].position, Quaternion.Euler(0f, 0f, 90f), transform);
        }

        if (C1Result == true && C2Result == true && C3Result == true && C4Result == true)
        {
            CurIndex = 4;
        }
        //Debug.Log("AllCubeResult" + CurIndex);

    }


    private void TheFinalResult()
    {

        Ani.StartAnimation();
        StartCoroutine(SetMirror());
        //if (Input.GetMouseButtonDown(0))
        //{
        //    Debug.Log("D");
        //    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //    RaycastHit hit;

        //    if(Physics.Raycast(ray, out hit))
        //    {
        //        if(hit.transform.gameObject.tag == "Handle")
        //        {
        //            Debug.Log("This is  " + hit.transform.gameObject.tag);
        //            Destroy(hit.transform.gameObject);
        //        }
        //    }
        //}

        //레버가 인벤토리에 들어간다.
    }

    private IEnumerator SetMirror()
    {
        yield return new WaitForSeconds(1.5f);

        Collider[] childColliders = Mirror.GetComponentsInChildren<Collider>();

        foreach (Collider col in childColliders)
        {
            col.enabled = true;
        }
        Mirror.GetComponent<Rigidbody>().isKinematic = false;
    }


}
