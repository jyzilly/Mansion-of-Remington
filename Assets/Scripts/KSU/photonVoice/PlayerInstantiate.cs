using Photon.Pun;
using UnityEngine;

public class PlayerInstantiate : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private GameObject playerPrefab;
    [SerializeField]
    private Vector3 instantiatePos;

    private void SpawnPlayer()
    {
        Instantiate(playerPrefab, instantiatePos, Quaternion.identity);
    }

    public override void OnJoinedRoom()
    {
        SpawnPlayer();
    }
}
