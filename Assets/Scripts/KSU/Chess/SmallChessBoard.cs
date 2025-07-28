using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.Collections;
using static UnityEngine.Rendering.DebugUI.Table;

public class SmallChessBoard : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject tilePrefab;
    public float tileSize = 1f;
    public int boardSize = 8;
    public float spacing = 0.5f;
    public float startPos = -5.25f;
    public float scale = 1f;
    public int[] paths = { 1, 2, 3, 4, 3, 2, 10, 18, 26, 27, 28, 36, 44, 52, 60, 61, 62, 63, 64 };
    public int currentIdx = 1;
    public bool clear = false;
    public List<GameObject> tiles = new List<GameObject>();

    private bool check = false;

    private void Start()
    {
        tiles.Clear();
        Reset();

        StartCoroutine(CheckPlayerInstantiate());
    }

    private void Update()
    {
        if (currentIdx == 20 && !check)
        {
            StartCoroutine(CheckClear());
            check = true;
        }
    }

    private void CreateTiles()
    {
        // 8x8 ü���� ����
        for (int row = 0; row < boardSize; row++)
        {
            for (int col = 0; col < boardSize; col++)
            {
                // �� Ÿ���� ��ġ ���

                Vector3 localPosition = new Vector3(startPos + col * (tileSize + spacing), 0.1f, startPos + row * (tileSize + spacing));

                Vector3 position = transform.TransformPoint(localPosition);

                // Ÿ���� �����ϰ� ��ġ�� ����
                GameObject tile = PhotonNetwork.Instantiate(tilePrefab.name, position, Quaternion.identity);

                // ����ȭ(�ڽ����� �߰��ϴ°�)
                photonView.RPC("SetParentRPC", RpcTarget.OthersBuffered, tile.GetComponent<PhotonView>().ViewID, row, col);

                tile.transform.SetParent(transform);

                tile.name = "" + (row * boardSize + col + 1);  // ��ȣ�� �̸����� ����

                tile.transform.localScale = new Vector3(1f, 0.2f, 1f);
                tile.transform.localRotation = Quaternion.identity;

                // Ÿ�� ����Ʈ�� �߰�
                tiles.Add(tile);

                // ��ȣ�� Ÿ���� ��ܿ� ǥ���� ��� (�ɼ�)
                // ��ȣ �ؽ�Ʈ�� �����, Ÿ�Ͽ� �߰��� �� �ֽ��ϴ�.
                // �ؽ�Ʈ�� �ʿ� ���ٸ� ���� �����մϴ�.
                if (tile.GetComponentInChildren<TextMesh>() != null)
                {
                    TextMesh textMesh = tile.GetComponentInChildren<TextMesh>();
                    textMesh.text = (row * boardSize + col + 1).ToString();
                }
            }
        }
    }

    private IEnumerator CheckPlayerInstantiate()
    {
        while (true)
        {
            if (PhotonNetwork.LocalPlayer.CustomProperties.ContainsKey("Role"))
            {
                break;
            }

            yield return null;
        }

        yield return new WaitForSeconds(1f);

        if (PhotonNetwork.IsMasterClient)
        {
            CreateTiles();
        }
    }

    public void Reset()
    {
        currentIdx = 1;
    }

    private void DestroyTiles()
    {
        foreach (GameObject tile in tiles)
        {
            if (tile != null)
            {
                Destroy(tile);
            }
        }
        tiles.Clear();
    }

    [PunRPC]
    private void SetParentRPC(int _viewID, int _row, int _col)
    {
        Debug.Log("RPCȣ���");

        PhotonView photonView = PhotonView.Find(_viewID);

        GameObject tile = photonView.gameObject;

        tile.transform.SetParent(transform);

        tile.name = "" + (_row * boardSize + _col + 1);  // ��ȣ�� �̸����� ����

        tile.transform.localScale = new Vector3(1f, 0.2f, 1f);
        tile.transform.localRotation = Quaternion.identity;

        // Ÿ�� ����Ʈ�� �߰�
        tiles.Add(tile);

        // ��ȣ�� Ÿ���� ��ܿ� ǥ���� ��� (�ɼ�)
        // ��ȣ �ؽ�Ʈ�� �����, Ÿ�Ͽ� �߰��� �� �ֽ��ϴ�.
        // �ؽ�Ʈ�� �ʿ� ���ٸ� ���� �����մϴ�.
        if (tile.GetComponentInChildren<TextMesh>() != null)
        {
            TextMesh textMesh = tile.GetComponentInChildren<TextMesh>();
            textMesh.text = (_row * boardSize + _col + 1).ToString();
        }
    }

    private IEnumerator CheckClear()
    {
        yield return new WaitForSeconds(0.5f);

        if (currentIdx == 20)
        {
            clear = true;
            DestroyTiles();
            currentIdx = -1;
        }

        check = false;
    }


}
