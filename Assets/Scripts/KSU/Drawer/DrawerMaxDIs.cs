using UnityEngine;

public class DrawerMaxDIs : MonoBehaviour
{
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
    }

    private void Update()
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
