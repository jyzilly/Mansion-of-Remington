using ExitGames.Client.Photon;
using Photon.Pun;
using UnityEngine;

public class RoleAdd : MonoBehaviourPunCallbacks
{
    public bool boy = false;
    public bool woman = false;

    public override void OnJoinedRoom()
    {
        if (boy)
        {
            Hashtable playerProperties = new Hashtable();
            playerProperties.Add("Role", "Boy");
            PhotonNetwork.LocalPlayer.SetCustomProperties(playerProperties);
        }
        else if (woman)
        {
            Hashtable playerProperties = new Hashtable();
            playerProperties.Add("Role", "Woman");
            PhotonNetwork.LocalPlayer.SetCustomProperties(playerProperties);
        }
    }
}
