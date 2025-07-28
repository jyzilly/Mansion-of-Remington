using UnityEngine;

public class TestTile : MonoBehaviour
{
    private BigChessBoard board;
    private void Start()
    {
        GameObject boardGo = GameObject.Find("Board");
        board = boardGo.GetComponent<BigChessBoard>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (board.paths[board.currentIdx-1].ToString() == gameObject.name)
        {
            gameObject.GetComponent<Renderer>().material.color = Color.green;
            board.currentIdx++;
        }
        else
        {
            gameObject.GetComponent<Renderer>().material.color = Color.red;
            // 초기화 하는 코드
            board.Reset();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        gameObject.GetComponent<Renderer>().material.color = Color.gray;
    }
}
