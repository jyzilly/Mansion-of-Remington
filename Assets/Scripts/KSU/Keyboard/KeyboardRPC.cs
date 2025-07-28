using UnityEngine;
using Photon.Pun;

[RequireComponent(typeof(PhotonView))]
public class KeyboardRPC : MonoBehaviourPun
{

    public bool TheButtonisPressed = false;

    public void SetButton()
    {
        photonView.RPC("SetButtonRPC", RpcTarget.All);
    }

    public void SetButtonExit()
    {
        photonView.RPC("SetButtonExitRPC", RpcTarget.All);
    }

    [PunRPC]
    private void SetButtonRPC()
    {
        TheButtonisPressed = true;
    }

    [PunRPC]
    public void SetButtonExitRPC()
    {
        TheButtonisPressed = false;
    }

}