using UnityEngine;

public class SlidePuzzle : MonoBehaviour
{
    //    public GameObject[] puzzlePieces;        // 퍼즐 조각들을 저장할 배열
    //    private Vector3[] initialPositions;      // 각 퍼즐 조각의 초기 위치를 저장할 배열
    //    public int emptyIndex = 8;               // 빈 공간의 인덱스 (3x3 퍼즐에서 8번 인덱스가 빈 공간)

    //public Vector3 targetPosition;

    private void Start()
    {
        
    
        //targetPosition = transform.position;
        //// 초기 위치를 저장
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
        //if (Input.GetMouseButtonDown(0))  // 마우스 왼쪽 버튼 클릭
        //{
        //    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //    RaycastHit hit;

        //    if (Physics.Raycast(ray, out hit))
        //    {
        //        // 클릭된 퍼즐 조각이 hit.collider로 감지됩니다.
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
        // 퍼즐 조각에 따라 값 반환 (예: 퍼즐에 번호가 있다면 그 번호 반환)
        return int.Parse(gameObject.name);  // 예: 퍼즐 조각이 "1", "2", "3" 등의 이름을 가질 경우
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
    ////    int index = System.Array.IndexOf(puzzlePieces, this.gameObject); // 클릭된 퍼즐 조각의 인덱스를 구함
    ////    if (IsAdjacent(index, emptyIndex))
    ////    {
    ////        OnPieceClicked(this.gameObject);  // OnPieceClicked를 직접 호출
    ////    }
    ////}

    //public void OnPieceClicked(GameObject clickedPiece)
    //{
    //    int clickedIndex = System.Array.IndexOf(puzzlePieces, clickedPiece);

    //    if (IsAdjacent(clickedIndex, emptyIndex))
    //    {
    //        // 클릭된 조각과 빈 공간의 위치를 교환
    //        Vector3 tempPosition = puzzlePieces[emptyIndex].transform.position;
    //        puzzlePieces[emptyIndex].transform.position = puzzlePieces[clickedIndex].transform.position;
    //        puzzlePieces[clickedIndex].transform.position = tempPosition;

    //        emptyIndex = clickedIndex; // 빈 공간 인덱스 업데이트
    //    }
    //}

    //bool IsAdjacent(int index1, int index2)
    //{
    //    int[] adjacentIndexes = new int[] { index1 - 1, index1 + 1, index1 - 3, index1 + 3 };

    //    // 그리드 경계 체크
    //    if ((index1 % 3 == 0 && index2 == index1 - 1) || (index1 % 3 == 2 && index2 == index1 + 1))
    //    {
    //        return false;
    //    }

    //    return System.Array.Exists(adjacentIndexes, element => element == index2);
    //}

    //bool IsPuzzleSolved()
    //{
    //    // 퍼즐이 해결되었는지 체크
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
