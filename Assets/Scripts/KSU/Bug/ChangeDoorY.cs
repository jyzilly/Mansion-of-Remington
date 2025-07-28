using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class ChangeDoorY : MonoBehaviour
{
    public GameObject childObject;  // �ڽ� ������Ʈ�� �Ҵ��� ����
    public float duration = 1f;     // ȸ�� ��ȭ�� �ɸ��� �ð� (��)
    public float rotationY;

    private Quaternion startRotation;  // ���� ȸ�� ��
    private Quaternion endRotation;    // ��ǥ ȸ�� ��
    private float elapsedTime = 0f;    // ��� �ð�

    private XRGrabInteractable grab;

    

    void Start()
    {
        // �ڽ� ������Ʈ�� ���� ȸ�� ���� ������
        startRotation = childObject.transform.rotation;

        // ��ǥ ȸ�� �� ���� (Y ���� 90���� ����)
        endRotation = Quaternion.Euler(startRotation.eulerAngles.x, rotationY, startRotation.eulerAngles.z);

        grab = GetComponent<XRGrabInteractable>();
    }

    private void Update()
    {
        if (grab.isSelected)
        {
            grab.enabled = false;
            StartCoroutine(RotateDoor());
        }
    }

    private IEnumerator RotateDoor()
    {
        float elapsedTime = 0f;  // ��� �ð� �ʱ�ȭ

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;  // ��� �ð� ����

            // Lerp�� ����Ͽ� ȸ�� �� �ε巴�� ����
            childObject.transform.rotation = Quaternion.Lerp(startRotation, endRotation, elapsedTime / duration);

            yield return null;  // ���� �����ӱ��� ��ٸ�
        }

        // ���������� ��ǥ ȸ�� ���� ��Ȯ�� ����
        childObject.transform.rotation = endRotation;
    }
}
