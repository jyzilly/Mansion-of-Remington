using UnityEngine;
using Photon.Pun;

public class SmallTile : MonoBehaviourPun
{

    private SmallChessBoard board;
    private void Start()
    {
        GameObject boardGo = GameObject.Find("P_SmallBoard");
        board = boardGo.GetComponent<SmallChessBoard>();
    }

    public void Correct(string _name)
    {
        if (gameObject.name == _name)
        {
            Material mat = gameObject.GetComponent<Renderer>().material;
            mat.EnableKeyword("_EMISSION");
            mat.SetColor("_EmissionColor", Color.green * 60f);

            board.currentIdx++;

            photonView.RPC("CorrectRPC", RpcTarget.Others, _name);
        }
    }

    public void Wrong(string _name)
    {
        if (gameObject.name == _name)
        {
            Material mat = gameObject.GetComponent<Renderer>().material;
            mat.EnableKeyword("_EMISSION");
            mat.SetColor("_EmissionColor", Color.red * 60f);

            board.Reset();

            photonView.RPC("WrongRPC", RpcTarget.Others, _name);
        }
    }

    public void Exit(string _name)
    {
        if (gameObject.name == _name)
        {
            Material mat = gameObject.GetComponent<Renderer>().material;
            mat.DisableKeyword("_EMISSION");

            photonView.RPC("ExitRPC", RpcTarget.Others, _name);
        }
    }

    [PunRPC]
    private void CorrectRPC(string _name)
    {
        if (gameObject.name == _name)
        {
            Material mat = gameObject.GetComponent<Renderer>().material;
            mat.EnableKeyword("_EMISSION");
            mat.SetColor("_EmissionColor", Color.green * 60f);
            board.currentIdx++;
        }
    }

    [PunRPC]
    private void WrongRPC(string _name)
    {
        if (gameObject.name == _name)
        {
            Material mat = gameObject.GetComponent<Renderer>().material;
            mat.EnableKeyword("_EMISSION");
            mat.SetColor("_EmissionColor", Color.red * 60f);

            board.Reset();
        }
    }

    [PunRPC]
    private void ExitRPC(string _name)
    {
        if (gameObject.name == _name)
        {
            Material mat = gameObject.GetComponent<Renderer>().material;
            mat.DisableKeyword("_EMISSION");
        }
    }

}