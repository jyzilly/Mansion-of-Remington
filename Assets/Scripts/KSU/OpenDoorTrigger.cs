using System.Collections;
using UnityEngine;

public class OpenDoorTrigger : MonoBehaviour
{
    public Transform RightDoor;
    public Transform LeftDoor;

    public bool opened = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && !opened)
        {
            opened = true;
            StartCoroutine(ChangeY());
        }
    }

    private IEnumerator ChangeY()
    {
        float elapsedTime = 0f;

        Quaternion targetLeftDoorRotation = Quaternion.Euler(LeftDoor.eulerAngles.x, 270f, LeftDoor.eulerAngles.z);
        Quaternion targetRightDoorRotation = Quaternion.Euler(RightDoor.eulerAngles.x, 90f, RightDoor.eulerAngles.z);

        while (elapsedTime < 1f)
        {
            // �ð��� ����Ͽ� ȸ��
            LeftDoor.rotation = Quaternion.Slerp(LeftDoor.rotation, targetLeftDoorRotation, elapsedTime / 1f);
            RightDoor.rotation = Quaternion.Slerp(RightDoor.rotation, targetRightDoorRotation, elapsedTime / 1f);

            // �ð� ���
            elapsedTime += Time.deltaTime;

            // ��� ���
            yield return null;
        }
    }
}
