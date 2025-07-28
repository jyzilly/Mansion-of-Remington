using UnityEngine;
using System.Collections.Generic;
using TMPro;
using System.Linq;

public class gameS : MonoBehaviour
{
    public GameObject emptySpace;
    private Camera _camera;
    [SerializeField] private SlidePuzzle[] tiles;

    [SerializeField]
    GameObject offArt;
    [SerializeField]
    GameObject onArt;

    public GameObject[] puzzlePieces;        // 퍼즐 조각들을 저장할 배열
    public List<GameObject> puzzlePiecesList = null;
    private Vector3[] initialPositions;      // 각 퍼즐 조각의 초기 위치를 저장할 배열
    private Vector3[] currentPositions;
    public int emptyIndex = 8;

    //private Vector3[] initialPositions;
    private List<int> puzzleIndices = new List<int>();

    private bool puzzleSolved = false;
    bool isend = false;

    void Start()
    {
        puzzlePiecesList = puzzlePieces.ToList();
        //if (hit.transform != null && hit.transform.gameObject == emptySpace)
        //{
        //    Debug.Log("Empty space clicked!");
        //    return;
        //}

        //if (emptySpace == null)
        //{
        //    Debug.LogError("Empty Space is not assigned in the Inspector!");
        //}
        //else
        //{
        //    Debug.Log("Empty Space assigned: " + emptySpace.name);
        //}

        _camera = Camera.main;
        // 초기 위치를 저장
        initialPositions = new Vector3[puzzlePieces.Length];
        currentPositions = new Vector3[puzzlePieces.Length];
        for (int i = 0; i < puzzlePieces.Length; ++i)
        {
            initialPositions[i] = puzzlePieces[i].transform.position;
            currentPositions[i] = initialPositions[i];
            puzzleIndices.Add(i);
        }
        ShufflePuzzle();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
                Debug.Log("클릭");

            if (Physics.Raycast(ray, out hit))
            {
                ////if (hit.transform == null)
                ////{
                ////    Debug.LogError("Raycast hit object is null.");
                ////    return;  // hit.transform이 null이면 종료
                ////}

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
                    SlidePuzzle thisTile = hit.transform.GetComponent<SlidePuzzle>();
                    //if (thisTile == null)
                    //{
                    //    Debug.LogError("SlidePuzzle component is missing on the hit object.");
                    //    return;  // SlidePuzzle 컴포넌트가 없으면 처리하지 않음
                    //}

                    emptySpace.transform.position = thisTile.transform.position;                
                    thisTile.transform.position = lastEmptySpace;
                    
                    //Vector3.Lerp(thisTile.targetPosition, lastEmptySpace, 0.05f);

                    //puzzlePieces[i].GetComponent<SlidePuzzle>().targetPosition = initialPositions[shuffledIndies[initialPositions]];
                    //emptySpace.transform.position = hit.transform.position;
                    //hit.transform.position = lastEmptySpace;
                    //int tileIndex = findIndex(thisTile);

                    //tiles[emptySpaceIndex] = tiles[tileIndex];
                    //tiles[tileIndex] = null;
                    //emptySpaceIndex = tilesIndex;

                    // UpdateCurrentPositions();
                }

                //if (hit.transform != null && hit.transform.gameObject == emptySpace)
                //{
                //    Debug.Log("Empty space clicked!");
                //    return;
                //}


                //else
                //{
                //    // 빈 공간이 아닌 다른 퍼즐 조각이 클릭되었을 때
                //    Debug.Log("Piece clicked: " + hit.transform.name);
                //    // 퍼즐 조각이 클릭되었을 때 수행할 동작을 여기에 추가
                //}
            }
        }


        if (!puzzleSolved && IsPuzzleSolved())
        {
            puzzleSolved = true;
            Debug.Log("끝!");
            onArt.SetActive(true);
            offArt.SetActive(false);
        }

        //if (emptySpace == null)
        //{
        //    Debug.LogError("Empty Space is not assigned in the Inspector!");
        //}
    }

    void ShufflePuzzle()
    {
        bool isValid = false;

        while (!isValid)
        {
            ShufflePieces();
            Debug.Log("검사 중");
            isValid = isSolvable();
        }
            Debug.Log("풀 수 있다!!!!");
    }


    //public List<GameObject> puzzlePieces = new List<GameObject>();
    //private List<Vector3> initialPositions = new List<Vector3>();
    void ShufflePieces()
    {
        //List<int> shuffledIndices = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8 };
        //System.Random rand = new System.Random();


        for (int i = 0; i < puzzlePiecesList.Count; ++i)
        {
            Debug.Log("셔플 전 :" + puzzlePieces[i]);
            
            GameObject temp = puzzlePieces[i];
            int RD = Random.Range(i, puzzlePiecesList.Count);
            puzzlePieces[i] = puzzlePiecesList[RD];
            //puzzlePieces[RD] = temp;
            //puzzlePieces[i].transform.position = initialPositions[i];

            puzzlePiecesList.RemoveAt(RD);


            Debug.Log("셔플 후:" + puzzlePieces[i]);


            

        }

        //List<GameObject> GMlist = GameObject.FindObjectOfType<GameObject>().ToList();



            //    // currentPositions[i] = puzzlePieces[i].transform.position;


        //Debug.Log("셔플 전:");
        //for (int i = 0; i < puzzlePieces.Length; ++i)
        //{
        //    SlidePuzzle slidePuzzle = puzzlePieces[i].GetComponent<SlidePuzzle>();
        //    if (slidePuzzle != null)
        //    {
        //        Debug.Log("퍼즐 조각 " + i + " 값: " + slidePuzzle.GetPuzzleValue());  // 값 출력
        //    }
        //}


        //for (int i = 0; i < shuffledIndices.Count; ++i)
        //{
        //    //Debug.Log("셔플전: " + string.Join(", ", puzzlePieces[i]));


        //    Debug.Log("셔플 :" + puzzlePieces[i]);


        //    //int temp = shuffledIndices[i];
        //    int randomIndex = Random.Range(i, shuffledIndices.Count);
        //    //shuffledIndices[i] = shuffledIndices[randomIndex];

        //    //puzzlePieces[i] = shuffledIndices[i];
        //    puzzlePieces[i] = puzzlePieces[];
        //    puzzlePieces[i].transform.position = initialPositions[];
        //    //Debug.Log(shuffledIndices[j]);

        //    Debug.Log(randomIndex);
        //    shuffledIndices.RemoveAt(randomIndex);


        //    Debug.Log("셔플 후:" + puzzlePieces[i]);


        //    // currentPositions[i] = puzzlePieces[i].transform.position;
        //    SlidePuzzle slidePuzzle = puzzlePieces[i].GetComponent<SlidePuzzle>();
        //    if (slidePuzzle != null)
        //    {
        //        slidePuzzle.targetPosition = puzzlePieces[i].transform.position;

        //        //Debug.Log("퍼즐 조각 " + j + " 값: " + slidePuzzle.GetPuzzleValue());  // 값 출력

        //    }
        // Debug.Log(shuffledIndices[i]);
        //shuffledIndices[randomIndex] = temp;
        //Debug.Log(temp);
        //if (i == 8)
        //{
        //    isend = true;
        //}


        // }

        //if (isend) 
        //{ 
        //    for (int j = 0; j < puzzlePieces.Length; ++j)
        //    {
        //        //puzzlePieces[i].GetComponent<SlidePuzzle>().targetPosition = initialPositions[shuffledIndices[i]];
        //        //SlidePuzzle.targetPosition = initialPositions[shuffledIndices[i]];

        //        puzzlePieces[j].transform.position = initialPositions[shuffledIndices[j]];
        //        //Debug.Log(shuffledIndices[j]);

        //        // currentPositions[i] = puzzlePieces[i].transform.position;
        //        SlidePuzzle slidePuzzle = puzzlePieces[j].GetComponent<SlidePuzzle>();
        //        if (slidePuzzle != null)
        //        {
        //            slidePuzzle.targetPosition = puzzlePieces[j].transform.position;

        //            //Debug.Log("퍼즐 조각 " + j + " 값: " + slidePuzzle.GetPuzzleValue());  // 값 출력

        //        }
        //        //Debug.Log("셔플후: " + string.Join(", ", puzzlePieces[i]));
        //        // Debug.Log("셔플후: " + string.Join(", ", slidePuzzle));
        //    }
        //    }


    }

    bool isSolvable()
    {
        List<int> puzzleValues = new List<int>();
        for (int i = 0; i < puzzlePieces.Length; ++i)
        {
            if (puzzlePieces[i].activeSelf)
            {
                puzzleValues.Add(i);
                Debug.Log("풀 수 있음");

            }
        }

        int inversions = 0;
        for (int i = 0; i < puzzleValues.Count; ++i)
        {
            for (int j = i + 1; j < puzzleValues.Count; ++j)
            {
                Debug.Log("풀 수 있음2");
                if (puzzleValues[i] > puzzleValues[j])
                {
                    ++inversions;
                }
            }
        }
        Debug.Log("풀 수 있음3");

        return inversions % 2 == 0;

        //int inversions = 0;
        //for (int i = 0; i < puzzlePieces.Length; ++i)
        //{
        //    for (int j = i + 1; j < puzzlePieces.Length; ++j)
        //    {
        //        Debug.Log("풀 수 있음2");
        //        if (puzzlePieces[i] > puzzlePieces[j])
        //        {
        //            ++inversions;
        //        }
        //    }
        //}
        //Debug.Log("풀 수 있음3");

        //return inversions % 2 == 0;

        //        List<int> puzzleValues = new List<int>();

        //        // 퍼즐 조각의 값(1, 2, 3, ..., N)을 가져와 puzzleValues 리스트에 추가합니다.
        //        for (int i = 0; i < puzzlePieces.Length; ++i)
        //        {
        //            if (puzzlePieces[i].activeSelf)  // 활성화된 퍼즐 조각만 고려
        //            {
        //                SlidePuzzle puzzle = puzzlePieces[i].GetComponent<SlidePuzzle>();
        //                if (puzzle != null)
        //                {
        //                    // 퍼즐의 값 (예: 1, 2, 3, ...)을 가져옴
        //                    int value = puzzle.GetPuzzleValue();  // GetPuzzleValue() 함수가 퍼즐의 실제 값 반환

        //                    //Debug.Log("퍼즐 값 추가 전: " + value);

        //                    puzzleValues.Add(value);
        //                    //Debug.Log("퍼즐 값 추가 후: " + string.Join(", ", puzzleValues));

        //                }
        //            }
        //        }

        ////Debug.Log("Puzzle Values: " + string.Join(", ", puzzleValues));


        //        int inversions = 0;
        //        // 역전 수 계산
        //        for (int i = 0; i < puzzleValues.Count; ++i)
        //        {

        //            for (int j = i + 1; j < puzzleValues.Count; ++j)
        //            {
        //                //Debug.Log("풀수있는지 검사하고있음");
        //                // i번과 j번 값 비교 (역전 계산)
        //                if (puzzleValues[i] > puzzleValues[j])
        //                {

        //                //Debug.Log("못풀수도 있음 인버전 추가중");
        //                    ++inversions;
        //                }
        //            }
        //        }

        //        // 역전 수가 짝수이면 풀 수 있는 퍼즐, 홀수이면 풀 수 없는 퍼즐
        //        //Debug.Log("Inversions: " + inversions);
        //        //Debug.Log(inversions % 2 == 0 ? "풀 수 있음" : "풀 수 없음");

        //        return inversions % 2 == 0;
    }


    bool IsPuzzleSolved()
    {
        float threshold = 0.1f; // 허용 오차
        for (int i = 0; i < puzzlePieces.Length; ++i)
        {
            if (Vector3.Distance(puzzlePieces[i].transform.position, initialPositions[i]) > threshold)
            {
                return false; // 하나라도 허용 오차를 넘으면 해결되지 않은 상태
            }
        }
        return true;
    }

    //void UpdateCurrentPositions()
    //{
    //    for (int i = 0; i < puzzlePieces.Length; ++i)
    //    {
    //        currentPositions[i] = puzzlePieces[i].transform.position;
    //    }
    //}
}

//void ShufflePuzzle()
//{
//    bool isValid = false;

//    while (!isValid)
//    {
//        ShufflePieces();
//        isValid = isSolvable();
//    }
//}

//void ShufflePieces()
//{
//    List<int> shuffledIndices = new List<int>(puzzleIndices);
//    System.Random rand = new System.Random();

//    // 셔플
//    for (int i = 0; i < shuffledIndices.Count; ++i)
//    {
//        int temp = shuffledIndices[i];
//        int randomIndex = rand.Next(i, shuffledIndices.Count);
//        shuffledIndices[i] = shuffledIndices[randomIndex];
//        shuffledIndices[randomIndex] = temp;
//    }

//    // 셔플된 순서에 맞게 퍼즐 위치 업데이트
//    for (int i = 0; i < puzzlePieces.Length; ++i)
//    {
//        puzzlePieces[i].transform.position = initialPositions[shuffledIndices[i]];
//    }
//}

//bool isSolvable()
//{
//    List<int> puzzleValues = new List<int>();
//    for (int i = 0; i < puzzlePieces.Length; ++i)
//    {
//        if (puzzlePieces[i].activeSelf)
//        {
//            // puzzlePieces[i]가 실제 퍼즐 값을 가져야 한다. 예: puzzlePieces[i].Value
//            puzzleValues.Add(puzzlePieces[i].Value);
//        }
//    }

//    int inversions = 0;
//    // 인버전 계산
//    for (int i = 0; i < puzzleValues.Count; ++i)
//    {
//        for (int j = i + 1; j < puzzleValues.Count; ++j)
//        {
//            if (puzzleValues[i] > puzzleValues[j])
//            {
//                ++inversions;
//            }
//        }
//    }

//    // 3x3 퍼즐에 대한 솔버블 조건 예시 (홀수 인버전은 풀 수 없음)
//    return (inversions % 2 == 0); // 간단히 홀수 인버전 수를 체크하는 방법
//}




//void ShufflePuzzle()
//{
//    for (int i = 0; i < puzzlePieces.Length; ++i)
//    {
//        int randomIndex = Random.Range(0, puzzlePieces.Length);
//        Vector3 tempPosition = puzzlePieces[i].transform.position;
//        puzzlePieces[i].transform.position = puzzlePieces[randomIndex].transform.position;
//        puzzlePieces[randomIndex].transform.position = tempPosition;
//    }
//}

//public void Shuffle()
//{
//    for (int i = 0; i <= 7; ++i)
//    {

//        var lastPos = tiles[i].targetPosition;
//        int randomIndex = randomIndex.Range(0, 7);
//        tiles[i].targetPosition = tiles[randomIndex].targetPosition;
//        tiles[randomIndex].targetPosition = lastPos;

//        var tile = tiles[i];
//        tiles[i] = tiles[randomIndex];
//        tiles[randomIndex] = tile;
//    }
//}

//public int findIndex(SlidePuzzle ts)
//{
//    for (int i =0; i < tiles.Length; ++i)
//    {
//        if (tiles[i] != null)
//        {
//            if (tiles[i] == ts)
//            {
//                return i;
//            }
//        }
//    }

//    return -1;
//}
