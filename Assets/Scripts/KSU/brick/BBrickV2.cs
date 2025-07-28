using UnityEngine;
using Photon.Pun;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.UIElements;

public class BBrickV2 : MonoBehaviourPun
{
    public WBrick linkWBrick;
    public WBrick linkWBrick2;
    private Vector3 startPos = Vector3.zero;

    public float maxDis;
    public bool moveFrontDirX;
    public bool moveFrontDirZ;
    public bool moveBackDirX;
    public bool moveBakcDirZ;
    private float positionx;
    private float positionz;

    private void Awake()
    {
        positionx = transform.position.x;
        positionz = transform.position.z;
        startPos = transform.position;
    }

    private void Update()
    {
        MoveBlock();

        // 소년일때만 실행
        if (PhotonNetwork.LocalPlayer.CustomProperties.ContainsKey("Role") && PhotonNetwork.LocalPlayer.CustomProperties["Role"].ToString() == "Boy")
        {
            Vector3 changePos = startPos - transform.position;

            // 변화량이 0일때는 입력 안받음
            if (changePos == Vector3.zero) return;

            // 현재 방향의 변화량으로 전환해서 보냄.
            Vector3 worldMovement = transform.TransformDirection(changePos);

            // 연결된 벽돌 이동
            linkWBrick.MoveWBrick(worldMovement);
            linkWBrick2.MoveWBrick(worldMovement);
        }
    }

    private void MoveBlock()
    {
        if (moveFrontDirX)
        {
            if (transform.position.x >= positionx + maxDis)
            {
                transform.position = new Vector3(positionx + maxDis, transform.position.y, transform.position.z);
            }
            else if (transform.position.x <= positionx)
            {
                transform.position = new Vector3(positionx, transform.position.y, transform.position.z);
            }
        }
        else if (moveFrontDirZ)
        {
            if (transform.position.z >= positionz + maxDis)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, positionz + maxDis);
            }
            else if (transform.position.z <= positionz)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, positionz);
            }
        }
        else if (moveBackDirX)
        {
            if (transform.position.x <= positionx - maxDis)
            {
                transform.position = new Vector3(positionx - maxDis, transform.position.y, transform.position.z);
            }
            else if (transform.position.x >= positionx)
            {
                transform.position = new Vector3(positionx, transform.position.y, transform.position.z);
            }
        }
        else if (moveBakcDirZ)
        {
            if (transform.position.z <= positionz - maxDis)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, positionz - maxDis);
            }
            else if (transform.position.z >= positionz)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, positionz);
            }
        }
    }
}
