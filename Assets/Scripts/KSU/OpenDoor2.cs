using System.Collections;
using Photon.Realtime;
using UnityEngine;
public class OpenDoor2 : MonoBehaviour
{
    public Transform lDoor;
    public Transform rDoor;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            StartCoroutine(AnimateDoors());
        }
    }

    public IEnumerator AnimateDoors()
    {
        // �ִϸ��̼��� ���� �ʱ� ���¿� ��ǥ ���� ����
        float startRotation = 0f;  // ���� ȸ�� ��
        float targetRotation = 90f;  // ��ǥ ȸ�� ��

        Vector3 startScale = lDoor.localScale;  // ���� ������ ��
        Vector3 startScale2 = rDoor.localScale;
        // �θ��� x �������� ����Ͽ� ��ǥ ������ ����
        Vector3 targetScale = new Vector3(0.5f * transform.localScale.x, lDoor.localScale.y, lDoor.localScale.z);  // ��ǥ ������ ��
        Vector3 targetScale2 = new Vector3(-0.5f * transform.localScale.x, rDoor.localScale.y, rDoor.localScale.z);  // ��ǥ ������ ��

        float elapsedTime = 0f;  // ��� �ð�

        while (elapsedTime < 1f)
        {
            // ��� �ð��� ����Ͽ� ȸ���� ������ ����
            float t = elapsedTime / 1f;

            // ȸ�� (Z��)
            float currentRotation = Mathf.Lerp(startRotation, targetRotation, t);
            float currentRotation2 = Mathf.Lerp(-180f, -90f, t);

            lDoor.localRotation = Quaternion.Euler(lDoor.localRotation.eulerAngles.x, lDoor.localRotation.eulerAngles.y, currentRotation);
            rDoor.localRotation = Quaternion.Euler(rDoor.localRotation.eulerAngles.x, rDoor.localRotation.eulerAngles.y, currentRotation2);

            // ������ (x��) -> �θ��� ������ ����Ͽ� ����
            lDoor.localScale = Vector3.Lerp(startScale, targetScale, t);
            rDoor.localScale = Vector3.Lerp(startScale2, targetScale2, t);

            // ��� �ð� ����
            elapsedTime += Time.deltaTime;

            // ���� ���������� ���
            yield return null;
        }

        // ��Ȯ�� ��ǥ ���� �����ϵ��� ������ ����
        lDoor.localRotation = Quaternion.Euler(lDoor.localRotation.eulerAngles.x, lDoor.localRotation.eulerAngles.y, targetRotation);
        rDoor.localRotation = Quaternion.Euler(rDoor.localRotation.eulerAngles.x, rDoor.localRotation.eulerAngles.y, -90f);
        lDoor.localScale = targetScale;
        rDoor.localScale = targetScale2;
    }
}
