using Photon.Pun;
using UnityEngine;

public class WBrick : MonoBehaviourPun
{
    private Vector3 startPos = Vector3.zero;

    private void Start()
    {
        startPos = transform.position;
    }

    public void MoveWBrick(Vector3 _changePos)
    {
        photonView.RPC("MoveWBrickRPC", RpcTarget.All, _changePos);
    }

    [PunRPC]
    private void MoveWBrickRPC(Vector3 _changePos)
    {
        Vector3 localMovement = transform.InverseTransformDirection(_changePos);

        transform.position = startPos - localMovement;
    }
}
