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


            // �ʱ�ȭ �ϴ� �ڵ�
            board.Reset();

            // Ʋ���� ���� �˸��� �ڵ� (�����϶�� �ϸ� �ɵ�)
            ChangeSmallBoardWrong(gameObject.name);

            return;
        }

        Debug.Log("trigger��");
        // ��ȣ�� �̸��� ��ġ�Ҷ�
        if (board.paths[board.currentIdx-1].ToString() == gameObject.name)
        {
            //Material mat = gameObject.GetComponent<Renderer>().material;
            //mat.EnableKeyword("_EMISSION");
            //mat.SetColor("_EmissionColor", Color.green * 60f);
            AudioManager.instance.PlaySfx(AudioManager.sfx.succes);

            // �°� ���� �˸��� �ڵ� (������ ���ڷ�)
            ChangeSmallBoardCorrect(gameObject.name);

            board.currentIdx++;
        }
        else
        {
            //Material mat = gameObject.GetComponent<Renderer>().material;
            //mat.EnableKeyword("_EMISSION");
            //mat.SetColor("_EmissionColor", Color.red * 60f);
            AudioManager.instance.PlaySfx(AudioManager.sfx.chess);


            // �ʱ�ȭ �ϴ� �ڵ�
            board.Reset();

            // Ʋ���� ���� �˸��� �ڵ� (�����϶�� �ϸ� �ɵ�)
            ChangeSmallBoardWrong(gameObject.name);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Material mat = gameObject.GetComponent<Renderer>().material;
        mat.DisableKeyword("_EMISSION");

        // ���Դٴ°� �˸��� �ڵ� (������ ���ڷ�)
        ChangeSmallBoardExit(gameObject.name);
    }

    private void ChangeSmallBoardCorrect(string _name)
    {
        // Ư�� ��ũ��Ʈ�� ���� ��� ��ü�� ã��
        SmallTile[] targets = FindObjectsByType<SmallTile>(FindObjectsSortMode.None);

        // ã�� ��� ��ü�� �޽����� broadcast
        foreach (SmallTile target in targets)
        {
            target.Correct(_name);
        }
    }

    private void ChangeSmallBoardWrong(string _name)
    {
        // Ư�� ��ũ��Ʈ�� ���� ��� ��ü�� ã��
        SmallTile[] targets = FindObjectsByType<SmallTile>(FindObjectsSortMode.None);

        // ã�� ��� ��ü�� �޽����� broadcast
        foreach (SmallTile target in targets)
        {
            target.Wrong(_name);
        }
    }

    private void ChangeSmallBoardExit(string _name)
    {
        // Ư�� ��ũ��Ʈ�� ���� ��� ��ü�� ã��
        SmallTile[] targets = FindObjectsByType<SmallTile>(FindObjectsSortMode.None);

        // ã�� ��� ��ü�� �޽����� broadcast
        foreach (SmallTile target in targets)
        {
            target.Exit(_name);
        }
    }
}
