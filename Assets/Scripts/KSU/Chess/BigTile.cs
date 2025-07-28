using UnityEngine;

public class BigTile : MonoBehaviour
{
    private BigChessBoard board;
    private void Start()
    {
        GameObject boardGo = GameObject.Find("P_BigBoard");
        board = boardGo.GetComponent<BigChessBoard>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (board.currentIdx == 20)
        {
            //Material mat = gameObject.GetComponent<Renderer>().material;
            //mat.EnableKeyword("_EMISSION");
            //mat.SetColor("_EmissionColor", Color.red * 60f);
            AudioManager.instance.PlaySfx(AudioManager.sfx.chess);


            // 초기화 하는 코드
            board.Reset();

            // 틀리게 들어감을 알리는 코드 (리셋하라고 하면 될듯)
            ChangeSmallBoardWrong(gameObject.name);

            return;
        }

        Debug.Log("trigger됨");
        // 번호와 이름이 일치할때
        if (board.paths[board.currentIdx-1].ToString() == gameObject.name)
        {
            //Material mat = gameObject.GetComponent<Renderer>().material;
            //mat.EnableKeyword("_EMISSION");
            //mat.SetColor("_EmissionColor", Color.green * 60f);
            AudioManager.instance.PlaySfx(AudioManager.sfx.succes);

            // 맞게 들어감을 알리는 코드 (순서를 인자로)
            ChangeSmallBoardCorrect(gameObject.name);

            board.currentIdx++;
        }
        else
        {
            //Material mat = gameObject.GetComponent<Renderer>().material;
            //mat.EnableKeyword("_EMISSION");
            //mat.SetColor("_EmissionColor", Color.red * 60f);
            AudioManager.instance.PlaySfx(AudioManager.sfx.chess);


            // 초기화 하는 코드
            board.Reset();

            // 틀리게 들어감을 알리는 코드 (리셋하라고 하면 될듯)
            ChangeSmallBoardWrong(gameObject.name);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Material mat = gameObject.GetComponent<Renderer>().material;
        mat.DisableKeyword("_EMISSION");

        // 나왔다는걸 알리는 코드 (순서를 인자로)
        ChangeSmallBoardExit(gameObject.name);
    }

    private void ChangeSmallBoardCorrect(string _name)
    {
        // 특정 스크립트를 가진 모든 객체를 찾음
        SmallTile[] targets = FindObjectsByType<SmallTile>(FindObjectsSortMode.None);

        // 찾은 모든 객체에 메시지를 broadcast
        foreach (SmallTile target in targets)
        {
            target.Correct(_name);
        }
    }

    private void ChangeSmallBoardWrong(string _name)
    {
        // 특정 스크립트를 가진 모든 객체를 찾음
        SmallTile[] targets = FindObjectsByType<SmallTile>(FindObjectsSortMode.None);

        // 찾은 모든 객체에 메시지를 broadcast
        foreach (SmallTile target in targets)
        {
            target.Wrong(_name);
        }
    }

    private void ChangeSmallBoardExit(string _name)
    {
        // 특정 스크립트를 가진 모든 객체를 찾음
        SmallTile[] targets = FindObjectsByType<SmallTile>(FindObjectsSortMode.None);

        // 찾은 모든 객체에 메시지를 broadcast
        foreach (SmallTile target in targets)
        {
            target.Exit(_name);
        }
    }
}
