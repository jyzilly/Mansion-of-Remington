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
        float lerptime = 0f; // lerptime 초기화
        float duration = 3f; // 선형보간에 걸리는 시간 (3초)

        yield return new WaitForSeconds(0.5f); // 0.5초 대기

        Vector3 startPosition = targetTr.position; // 시작 위치 저장
        Quaternion startRotation = targetTr.rotation; // 시작 회전 저장

        while (lerptime < duration)
        {
            lerptime += Time.deltaTime; // 매 프레임 시간 만큼 증가
            float t = lerptime / duration; // 진행 비율 (0~1)

            targetTr.position = Vector3.Lerp(startPosition, transform.position, t); // 위치 보간
            targetTr.rotation = Quaternion.Lerp(startRotation, transform.rotation, t); // 회전 보간

            yield return null; // 다음 프레임까지 대기
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
