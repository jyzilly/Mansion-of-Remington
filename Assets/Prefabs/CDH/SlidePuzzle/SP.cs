using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;

public class SP : MonoBehaviour
{
    public GameObject emptySpace;
    //private Camera _camera;
    public Camera cam = null;

    [SerializeField]
    GameObject offArt;
    [SerializeField]
    GameObject onArt;

    public List<GameObject> puzzlePieces = new List<GameObject>();
    private List<GameObject> CheckPieces = new List<GameObject>();
    private List<Vector3> initialPositions = new List<Vector3>();
    public int emptyIndex = 8;

    private bool puzzleSolved = false;
    bool isEnd = false;

    private void Start()
    {
        //_camera = Camera.main;

        for (int i = 0; i < puzzlePieces.Count; ++i)
        {
            CheckPieces.Add(puzzlePieces[i]);
            Debug.Log(puzzlePieces[i] + "Ǯ��üũ");
            initialPositions.Add(puzzlePieces[i].transform.position);
        }
        ShufflePuzzle();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            Debug.Log("Ŭ��");

            if (Physics.Raycast(ray, out hit))
            {

                Debug.Log(hit.transform.name);


                if (hit.transform.gameObject == emptySpace)
                {
                    Debug.Log("Empty space clicked!");
                    // �� ������ Ŭ������ �� ������ ������ ���⿡ �߰�
                    return;
                }

                if (Vector3.Distance(emptySpace.transform.position, hit.transform.position) < 0.8)
                {
                    Vector3 lastEmptySpace = emptySpace.transform.position;
                    GameObject thisTile = hit.transform.gameObject;

                    if ((hit.transform.position.x - emptySpace.transform.position.x) > 0)
                    {
                        int j = 0;
                        for (int i = 0; i <= puzzlePieces.Count; ++i)
                        {
                            ++j;
                            if (puzzlePieces[i] == thisTile)
                            {
                                puzzlePieces[j + 1] = puzzlePieces[j];
                                puzzlePieces[j] = emptySpace;
                                Debug.Log(CheckPieces[j]);
                                Debug.Log(puzzlePieces[j]);

                                puzzlePieces[j + 1].transform.position = puzzlePieces[j].transform.position;
                                puzzlePieces[j].transform.position = emptySpace.transform.position;
                            }
                        }
                    }
                    else if ((hit.transform.position.x - emptySpace.transform.position.x) < 0)
                    {
                        int j = 0;
                        for (int i = 0; i <= puzzlePieces.Count; ++i)
                        {
                            ++j;
                            if (puzzlePieces[i] == thisTile)
                            {
                                puzzlePieces[j - 1] = puzzlePieces[j];
                                puzzlePieces[j] = emptySpace;

                                puzzlePieces[j - 1].transform.position = puzzlePieces[j].transform.position;
                                puzzlePieces[j].transform.position = emptySpace.transform.position;
                            }
                        }
                    }
                    else if ((hit.transform.position.y - emptySpace.transform.position.y) > 0)
                    {
                        int j = 0;
                        for (int i = 0; i < puzzlePieces.Count; ++i)
                        {
                            if (puzzlePieces[i] == thisTile)
                            {


                                Debug.Log(j + 3);
                                if ( + 3 < puzzlePieces.Count)
                                {
                                puzzlePieces[j+3] = puzzlePieces[j];  // puzzlePieces[4]�� puzzlePieces[1]�� ���� ��ü�� ����Ų��

                                // puzzlePieces[1]�� empty�� �ֱ�
                                puzzlePieces[j] = emptySpace;  // puzzlePieces[1]�� empty ������ ����Ű�� �ȴ�

                                // ��ġ�� �ٲ���� �Ѵٸ�:
                                puzzlePieces[j+3].transform.position = initialPositions[j];
                                puzzlePieces[j].transform.position = emptySpace.transform.position;
                                    //puzzlePieces[i + 3] = puzzlePieces[i];
                                    //puzzlePieces[i] = emptySpace;
                                }
                                else
                                {
                                    Debug.Log(puzzlePieces[i]);
                                    Debug.LogWarning("�ε��� �ʰ�: i + 3�� ���� ���� �����ϴ�.");
                                }
                            }

                            //if (puzzlePieces[i] == thisTile)
                            //{
                            //    puzzlePieces[i + 3] = thisTile;
                            //    puzzlePieces[i] = emptySpace;

                            //}
                        }
                    }
                    else if ((hit.transform.position.y - emptySpace.transform.position.y) < 0)
                    {
                        int j = 0;
                        for (int i = 0; i < puzzlePieces.Count; ++i)
                        {
                            ++j;
                            if (puzzlePieces[i] == thisTile)
                            {

                                //i - 1�� ���� ���� �ִ��� üũ
                                if (j - 3 >= 0)
                                {
                                    puzzlePieces[j - 3] = puzzlePieces[j];
                                    puzzlePieces[j] = emptySpace;

                                    puzzlePieces[j - 3].transform.position = puzzlePieces[j].transform.position;
                                    puzzlePieces[j].transform.position = emptySpace.transform.position;
                                }
                                else
                                {
                                    Debug.LogWarning("�ε��� �ʰ�: i - 1�� ���� ���� �����ϴ�.");
                                }
                            }
                            //if (puzzlePieces[i] == thisTile)
                            //{
                            //    puzzlePieces[i - 1] = puzzlePieces[i];
                            //    puzzlePieces[i] = emptySpace;

                            //}
                        }
                    }

                    //int value = thisTile.GetPuzzleValue();
                    //for (int i = 0; i < puzzlePieces.Count+1; ++i)
                    //{
                    //    if (puzzlePieces[i] == thisTile)
                    //    {
                    //        return;
                    //    }

                    //}

                    //emptySpace.transform.position = hit.transform.position;
                    //hit.transform.position = lastEmptySpace;
                }
            }
        }

       // Debug.Log(puzzleSolved);
        //Debug.Log(IsPuzzleSolved());
        if  (IsPuzzleSolved())
        {
            puzzleSolved = true;
            Debug.Log("��!");
            onArt.SetActive(true);
            offArt.SetActive(false);
        }
    }

    void ShufflePuzzle()
    {
        bool isValid = false;

        while (!isValid)
        {
            ShufflePieces();
            isValid = isSolvable();
        }
    }


    void ShufflePieces()
    {
        for (int i =0; i < puzzlePieces.Count-1; ++i)
        {
            GameObject GM = puzzlePieces[i];
            //Vector3 Ini = initialPositions[i];
            int RD = Random.Range(i, puzzlePieces.Count-1);
            puzzlePieces[i] = puzzlePieces[RD];
            puzzlePieces[i].transform.position = initialPositions[i];
            //initialPositions[RD] = puzzlePieces[i].transform.position;
            //initialPositions[i] = initialPositions[RD];
            puzzlePieces[RD] = GM;
            //initialPositions[RD] = Ini;

            SlidePuzzle slidePuzzle = puzzlePieces[i].GetComponent<SlidePuzzle>();
            if (slidePuzzle != null)
            {
                slidePuzzle.transform.position = puzzlePieces[i].transform.position;

            }
        }
    }

    bool isSolvable()
    {
        List<int> puzzleValues = new List<int>();

        for (int i = 0; i < puzzlePieces.Count-1; ++i)
        {
            if (puzzlePieces[i] != null)
            {
                SlidePuzzle puzzle = puzzlePieces[i].GetComponent<SlidePuzzle>();
                if(puzzle != null)
                {
                    //Debug.Log(puzzle + "��");
                    int value = puzzle.GetPuzzleValue();
                    //Debug.Log(puzzle + "��");
                    //Debug.Log("���� �� �߰� ��: " + value);
                    puzzleValues.Add(value);
                    //Debug.Log("���� �� �߰� ��: " + value);

                    //Debug.Log(puzzlePieces.Count);
                }
            }
            if (i == 7)
            {
                isEnd = true;
            }
        }

        int inversions = 0;
       // Debug.Log(isEnd);
        if (isEnd)
        {
           // Debug.Log("���� ��");
            for (int i = 0; i < puzzleValues.Count-1; ++i)
            {
                for (int j = i + 1; j < puzzleValues.Count-1; ++j)
                {
                    //Debug.Log(puzzleValues[i] + "/" + puzzleValues[j]);
                    if (puzzleValues[i] > puzzleValues[j])
                    {
                       // Debug.Log("��Ǯ���� ���� �ι��� �߰���");
                        ++inversions;
                    }
                }
            }
        }

        Debug.Log("Inversions: " + inversions);
        Debug.Log(inversions % 2 == 0 ? "Ǯ �� ����" : "Ǯ �� ����");

        return inversions % 2 == 0;
    }

    bool IsPuzzleSolved()
    {

        for (int i = 0; i < puzzlePieces.Count; ++i)
        {
               // Debug.Log(CheckPieces[i] + "Ǯ��üũ");
               // Debug.Log(puzzlePieces[i] + "Ǯ��üũ");
            if (CheckPieces[i] != puzzlePieces[i])
            {
                return false;
            }
        }
            return true;

        //float threshold = 0.1f;
        //for (int i = 0; i < puzzlePieces.Count; ++i)
        //{
        //    if (Vector3.Distance(puzzlePieces[i].transform.position, initialPositions[i]) > threshold)
        //    {
        //        return false;
        //    }
        //    puzzleSolved = true;
        //}
        //return true;
    }
}
