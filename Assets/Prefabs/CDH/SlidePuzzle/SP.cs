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
            Debug.Log(puzzlePieces[i] + "풀이체크");
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
            Debug.Log("클릭");

            if (Physics.Raycast(ray, out hit))
            {

                Debug.Log(hit.transform.name);


                if (hit.transform.gameObject == emptySpace)
                {
                    Debug.Log("Empty space clicked!");
                    // 빈 공간을 클릭했을 때 수행할 동작을 여기에 추가
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
                                puzzlePieces[j+3] = puzzlePieces[j];  // puzzlePieces[4]는 puzzlePieces[1]과 같은 객체를 가리킨다

                                // puzzlePieces[1]에 empty를 넣기
                                puzzlePieces[j] = emptySpace;  // puzzlePieces[1]은 empty 공간을 가리키게 된다

                                // 위치도 바꿔줘야 한다면:
                                puzzlePieces[j+3].transform.position = initialPositions[j];
                                puzzlePieces[j].transform.position = emptySpace.transform.position;
                                    //puzzlePieces[i + 3] = puzzlePieces[i];
                                    //puzzlePieces[i] = emptySpace;
                                }
                                else
                                {
                                    Debug.Log(puzzlePieces[i]);
                                    Debug.LogWarning("인덱스 초과: i + 3이 범위 내에 없습니다.");
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

                                //i - 1이 범위 내에 있는지 체크
                                if (j - 3 >= 0)
                                {
                                    puzzlePieces[j - 3] = puzzlePieces[j];
                                    puzzlePieces[j] = emptySpace;

                                    puzzlePieces[j - 3].transform.position = puzzlePieces[j].transform.position;
                                    puzzlePieces[j].transform.position = emptySpace.transform.position;
                                }
                                else
                                {
                                    Debug.LogWarning("인덱스 초과: i - 1이 범위 내에 없습니다.");
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
            Debug.Log("끝!");
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
                    //Debug.Log(puzzle + "전");
                    int value = puzzle.GetPuzzleValue();
                    //Debug.Log(puzzle + "후");
                    //Debug.Log("퍼즐 값 추가 전: " + value);
                    puzzleValues.Add(value);
                    //Debug.Log("퍼즐 값 추가 후: " + value);

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
           // Debug.Log("복사 끝");
            for (int i = 0; i < puzzleValues.Count-1; ++i)
            {
                for (int j = i + 1; j < puzzleValues.Count-1; ++j)
                {
                    //Debug.Log(puzzleValues[i] + "/" + puzzleValues[j]);
                    if (puzzleValues[i] > puzzleValues[j])
                    {
                       // Debug.Log("못풀수도 있음 인버전 추가중");
                        ++inversions;
                    }
                }
            }
        }

        Debug.Log("Inversions: " + inversions);
        Debug.Log(inversions % 2 == 0 ? "풀 수 있음" : "풀 수 없음");

        return inversions % 2 == 0;
    }

    bool IsPuzzleSolved()
    {

        for (int i = 0; i < puzzlePieces.Count; ++i)
        {
               // Debug.Log(CheckPieces[i] + "풀이체크");
               // Debug.Log(puzzlePieces[i] + "풀이체크");
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
