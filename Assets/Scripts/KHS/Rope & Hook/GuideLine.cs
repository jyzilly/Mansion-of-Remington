using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class GuideLine : MonoBehaviour
{
    public delegate void GuideArrivedDelegate();

    private GuideArrivedDelegate guideArrivedCallback = null;
    public GuideArrivedDelegate GuideArrivedCallback
    {
        get { return guideArrivedCallback; }
        set { guideArrivedCallback = value; }
    }

    private Transform targetTr = null;
    public GameObject nextChainGo = null;
    private bool isdetect = false;
    private bool isprecallback = false;

    private void Start()
    {
        isdetect = false;
        isprecallback = false;

    }
    private void OnTriggerEnter(Collider _collider)
    {
        if(_collider.name == "ChainLinkEnd" && _collider.GetComponent<XRGrabInteractable>().isSelected)
        {
            _collider.GetComponent<MeshRenderer>().material.EnableKeyword("_EMISSION");
        }
    }
    private void OnTriggerStay(Collider _collider)
    {
        if (_collider.name == "ChainLinkEnd" && !_collider.GetComponent<XRGrabInteractable>().isSelected && !isdetect)
        {
            isdetect=true;
            targetTr = _collider.transform;
            _collider.GetComponent<Rigidbody>().isKinematic = true;
            _collider.GetComponent<MeshRenderer>().material.DisableKeyword("_EMISSION");
            StartCoroutine(PositionMove());
        }
    }
    private void OnTriggerExit(Collider _collider)
    {
        if(_collider.name == "ChainLinkEnd")
        {
            isdetect = false;
            nextChainGo?.SetActive(false);
            _collider.GetComponent<MeshRenderer>().material.DisableKeyword("_EMISSION");
        }
    }

    private IEnumerator PositionMove()
    {
        float lerptime = 0f; // lerptime �ʱ�ȭ
        float duration = 3f; // ���������� �ɸ��� �ð� (3��)

        yield return new WaitForSeconds(0.5f); // 0.5�� ���

        Vector3 startPosition = targetTr.position; // ���� ��ġ ����
        Quaternion startRotation = targetTr.rotation; // ���� ȸ�� ����

        while (lerptime < duration)
        {
            lerptime += Time.deltaTime; // �� ������ �ð� ��ŭ ����
            float t = lerptime / duration; // ���� ���� (0~1)

            targetTr.position = Vector3.Lerp(startPosition, transform.position, t); // ��ġ ����
            targetTr.rotation = Quaternion.Lerp(startRotation, transform.rotation, t); // ȸ�� ����

            yield return null; // ���� �����ӱ��� ���
        }

        Debug.Log("End!!!");
        nextChainGo?.SetActive(true);
        if (!isprecallback)
        {
            GuideArrivedCallback?.Invoke();
            isprecallback = true;
        }
    }
}
