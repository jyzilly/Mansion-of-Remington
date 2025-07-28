using Photon.Pun;
using UnityEngine;

public class startrepoter : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(PhotonNetwork.LocalPlayer.CustomProperties["Role"].ToString() == "Woman")
        {
            AudioManager.instance.PlaySfx(AudioManager.sfx.nareO);
        }
    }

    


}
