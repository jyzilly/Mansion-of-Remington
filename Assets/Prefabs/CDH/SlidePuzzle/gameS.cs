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

    public GameObject[] puzzlePieces;        // ���� �������� ������ �迭
    public List<GameObject> puzzlePiecesList = null;
    private Vector3[] initialPositions;      // �� ���� ������ �ʱ� ��ġ�� ������ �迭
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
        // �ʱ� ��ġ�� ����
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
                Debug.Log("Ŭ��");

            if (Physics.Raycast(ray, out hit))
            {
                ////if (hit.transform == null)
                ////{
                ////    Debug.LogError("Raycast hit object is null.");
                ////    return;  // hit.transform�� null�̸� ����
                ////}

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
                    SlidePuzzle thisTile = hit.transform.GetComponent<SlidePuzzle>();
                    //if (thisTile == null)
                    //{
                    //    Debug.LogError("SlidePuzzle component is missing on the hit object.");
                    //    return;  // SlidePuzzle ������Ʈ�� ������ ó������ ����
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
                //    // �� ������ �ƴ� �ٸ� ���� ������ Ŭ���Ǿ��� ��
                //    Debug.Log("Piece clicked: " + hit.transform.name);
                //    // ���� ������ Ŭ���Ǿ��� �� ������ ������ ���⿡ �߰�
                //}
            }
        }


        if (!puzzleSolved && IsPuzzleSolved())
        {
            puzzleSolved = true;
            Debug.Log("��!");
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
            Debug.Log("�˻� ��");
            isValid = isSolvable();
        }
            Debug.Log("Ǯ �� �ִ�!!!!");
    }


    //public List<GameObject> puzzlePieces = new List<GameObject>();
    //private List<Vector3> initialPositions = new List<Vector3>();
    void ShufflePieces()
    {
        //List<int> shuffledIndices = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8 };
        //System.Random rand = new System.Random();


        for (int i = 0; i < puzzlePiecesList.Count; ++i)
        {
            Debug.Log("���� �� :" + puzzlePieces[i]);
            
            GameObject temp = puzzlePieces[i];
            int RD = Random.Range(i, puzzlePiecesList.Count);
            puzzlePieces[i] = puzzlePiecesList[RD];
            //puzzlePieces[RD] = temp;
            //puzzlePieces[i].transform.position = initialPositions[i];

            puzzlePiecesList.RemoveAt(RD);


            Debug.Log("���� ��:" + puzzlePieces[i]);


            

        }

        //List<GameObject> GMlist = GameObject.FindObjectOfType<GameObject>().ToList();



            //    // currentPositions[i] = puzzlePieces[i].transform.position;


        //Debug.Log("���� ��:");
        //for (int i = 0; i < puzzlePieces.Length; ++i)
        //{
        //    SlidePuzzle slidePuzzle = puzzlePieces[i].GetComponent<SlidePuzzle>();
        //    if (slidePuzzle != null)
        //    {
        //        Debug.Log("���� ���� " + i + " ��: " + slidePuzzle.GetPuzzleValue());  // �� ���
        //    }
        //}


        //for (int i = 0; i < shuffledIndices.Count; ++i)
        //{
        //    //Debug.Log("������: " + string.Join(", ", puzzlePieces[i]));


        //    Debug.Log("���� :" + puzzlePieces[i]);


        //    //int temp = shuffledIndices[i];
        //    int randomIndex = Random.Range(i, shuffledIndices.Count);
        //    //shuffledIndices[i] = shuffledIndices[randomIndex];

        //    //puzzlePieces[i] = shuffledIndices[i];
        //    puzzlePieces[i] = puzzlePieces[];
        //    puzzlePieces[i].transform.position = initialPositions[];
        //    //Debug.Log(shuffledIndices[j]);

        //    Debug.Log(randomIndex);
        //    shuffledIndices.RemoveAt(randomIndex);


        //    Debug.Log("���� ��:" + puzzlePieces[i]);


        //    // currentPositions[i] = puzzlePieces[i].transform.position;
        //    SlidePuzzle slidePuzzle = puzzlePieces[i].GetComponent<SlidePuzzle>();
        //    if (slidePuzzle != null)
        //    {
        //        slidePuzzle.targetPosition = puzzlePieces[i].transform.position;

        //        //Debug.Log("���� ���� " + j + " ��: " + slidePuzzle.GetPuzzleValue());  // �� ���

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

        //            //Debug.Log("���� ���� " + j + " ��: " + slidePuzzle.GetPuzzleValue());  // �� ���

        //        }
        //        //Debug.Log("������: " + string.Join(", ", puzzlePieces[i]));
        //        // Debug.Log("������: " + string.Join(", ", slidePuzzle));
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
                Debug.Log("Ǯ �� ����");

            }
        }

        int inversions = 0;
        for (int i = 0; i < puzzleValues.Count; ++i)
        {
            for (int j = i + 1; j < puzzleValues.Count; ++j)
            {
                Debug.Log("Ǯ �� ����2");
                if (puzzleValues[i] > puzzleValues[j])
                {
                    ++inversions;
                }
            }
        }
        Debug.Log("Ǯ �� ����3");

        return inversions % 2 == 0;

        //int inversions = 0;
        //for (int i = 0; i < puzzlePieces.Length; ++i)
        //{
        //    for (int j = i + 1; j < puzzlePieces.Length; ++j)
        //    {
        //        Debug.Log("Ǯ �� ����2");
        //        if (puzzlePieces[i] > puzzlePieces[j])
        //        {
        //            ++inversions;
        //        }
        //    }
        //}
        //Debug.Log("Ǯ �� ����3");

        //return inversions % 2 == 0;

        //        List<int> puzzleValues = new List<int>();

        //        // ���� ������ ��(1, 2, 3, ..., N)�� ������ puzzleValues ����Ʈ�� �߰��մϴ�.
        //        for (int i = 0; i < puzzlePieces.Length; ++i)
        //        {
        //            if (puzzlePieces[i].activeSelf)  // Ȱ��ȭ�� ���� ������ ���
        //            {
        //                SlidePuzzle puzzle = puzzlePieces[i].GetComponent<SlidePuzzle>();
        //                if (puzzle != null)
        //                {
        //                    // ������ �� (��: 1, 2, 3, ...)�� ������
        //                    int value = puzzle.GetPuzzleValue();  // GetPuzzleValue() �Լ��� ������ ���� �� ��ȯ

        //                    //Debug.Log("���� �� �߰� ��: " + value);

        //                    puzzleValues.Add(value);
        //                    //Debug.Log("���� �� �߰� ��: " + string.Join(", ", puzzleValues));

        //                }
        //            }
        //        }

        ////Debug.Log("Puzzle Values: " + string.Join(", ", puzzleValues));


        //        int inversions = 0;
        //        // ���� �� ���
        //        for (int i = 0; i < puzzleValues.Count; ++i)
        //        {

        //            for (int j = i + 1; j < puzzleValues.Count; ++j)
        //            {
        //                //Debug.Log("Ǯ���ִ��� �˻��ϰ�����");
        //                // i���� j�� �� �� (���� ���)
        //                if (puzzleValues[i] > puzzleValues[j])
        //                {

        //                //Debug.Log("��Ǯ���� ���� �ι��� �߰���");
        //                    ++inversions;
        //                }
        //            }
        //        }

        //        // ���� ���� ¦���̸� Ǯ �� �ִ� ����, Ȧ���̸� Ǯ �� ���� ����
        //        //Debug.Log("Inversions: " + inversions);
        //        //Debug.Log(inversions % 2 == 0 ? "Ǯ �� ����" : "Ǯ �� ����");

        //        return inversions % 2 == 0;
    }


    bool IsPuzzleSolved()
    {
        float threshold = 0.1f; // ��� ����
        for (int i = 0; i < puzzlePieces.Length; ++i)
        {
            if (Vector3.Distance(puzzlePieces[i].transform.position, initialPositions[i]) > threshold)
            {
                return false; // �ϳ��� ��� ������ ������ �ذ���� ���� ����
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

//    // ����
//    for (int i = 0; i < shuffledIndices.Count; ++i)
//    {
//        int temp = shuffledIndices[i];
//        int randomIndex = rand.Next(i, shuffledIndices.Count);
//        shuffledIndices[i] = shuffledIndices[randomIndex];
//        shuffledIndices[randomIndex] = temp;
//    }

//    // ���õ� ������ �°� ���� ��ġ ������Ʈ
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
//            // puzzlePieces[i]�� ���� ���� ���� ������ �Ѵ�. ��: puzzlePieces[i].Value
//            puzzleValues.Add(puzzlePieces[i].Value);
//        }
//    }

//    int inversions = 0;
//    // �ι��� ���
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

//    // 3x3 ���� ���� �ֹ��� ���� ���� (Ȧ�� �ι����� Ǯ �� ����)
//    return (inversions % 2 == 0); // ������ Ȧ�� �ι��� ���� üũ�ϴ� ���
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
