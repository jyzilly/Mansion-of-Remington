using UnityEngine;

public class SlidePuzzle : MonoBehaviour
{
    //    public GameObject[] puzzlePieces;        // ���� �������� ������ �迭
    //    private Vector3[] initialPositions;      // �� ���� ������ �ʱ� ��ġ�� ������ �迭
    //    public int emptyIndex = 8;               // �� ������ �ε��� (3x3 ���񿡼� 8�� �ε����� �� ����)

    //public Vector3 targetPosition;

    private void Start()
    {
        
    
        //targetPosition = transform.position;
        //// �ʱ� ��ġ�� ����
        //initialPositions = new Vector3[puzzlePieces.Length];
        //for (int i = 0; i < puzzlePieces.Length; i++)
        //{
        //    initialPositions[i] = puzzlePieces[i].transform.position;
        //}
        //ShufflePuzzle();
    }

    void Update()
    {
        //transform.position = Vector3.Lerp(transform.position, targetPosition, 0.05f);
        //if (Input.GetMouseButtonDown(0))  // ���콺 ���� ��ư Ŭ��
        //{
        //    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //    RaycastHit hit;

        //    if (Physics.Raycast(ray, out hit))
        //    {
        //        // Ŭ���� ���� ������ hit.collider�� �����˴ϴ�.
        //        GameObject clickedPiece = hit.collider.gameObject;
        //        int clickedIndex = System.Array.IndexOf(puzzlePieces, clickedPiece);

        //        if (IsAdjacent(clickedIndex, emptyIndex))
        //        {
        //            OnPieceClicked(clickedPiece);
        //        }
        //    }
        //}
    }


    public int GetPuzzleValue()
    {
        // ���� ������ ���� �� ��ȯ (��: ���� ��ȣ�� �ִٸ� �� ��ȣ ��ȯ)
        return int.Parse(gameObject.name);  // ��: ���� ������ "1", "2", "3" ���� �̸��� ���� ���
    }

   

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

    ////void OnMouseDown()
    ////{
    ////    int index = System.Array.IndexOf(puzzlePieces, this.gameObject); // Ŭ���� ���� ������ �ε����� ����
    ////    if (IsAdjacent(index, emptyIndex))
    ////    {
    ////        OnPieceClicked(this.gameObject);  // OnPieceClicked�� ���� ȣ��
    ////    }
    ////}

    //public void OnPieceClicked(GameObject clickedPiece)
    //{
    //    int clickedIndex = System.Array.IndexOf(puzzlePieces, clickedPiece);

    //    if (IsAdjacent(clickedIndex, emptyIndex))
    //    {
    //        // Ŭ���� ������ �� ������ ��ġ�� ��ȯ
    //        Vector3 tempPosition = puzzlePieces[emptyIndex].transform.position;
    //        puzzlePieces[emptyIndex].transform.position = puzzlePieces[clickedIndex].transform.position;
    //        puzzlePieces[clickedIndex].transform.position = tempPosition;

    //        emptyIndex = clickedIndex; // �� ���� �ε��� ������Ʈ
    //    }
    //}

    //bool IsAdjacent(int index1, int index2)
    //{
    //    int[] adjacentIndexes = new int[] { index1 - 1, index1 + 1, index1 - 3, index1 + 3 };

    //    // �׸��� ��� üũ
    //    if ((index1 % 3 == 0 && index2 == index1 - 1) || (index1 % 3 == 2 && index2 == index1 + 1))
    //    {
    //        return false;
    //    }

    //    return System.Array.Exists(adjacentIndexes, element => element == index2);
    //}

    //bool IsPuzzleSolved()
    //{
    //    // ������ �ذ�Ǿ����� üũ
    //    for (int i = 0; i < puzzlePieces.Length; ++i)
    //    {
    //        if (puzzlePieces[i].transform.position != initialPositions[i])
    //        {
    //            return false;
    //        }
    //    }
    //    return true;
    //}
}
