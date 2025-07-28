using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class ChangeDoorY : MonoBehaviour
{
    public GameObject childObject;  // 자식 오브젝트를 할당할 변수
    public float duration = 1f;     // 회전 변화에 걸리는 시간 (초)
    public float rotationY;

    private Quaternion startRotation;  // 시작 회전 값
    private Quaternion endRotation;    // 목표 회전 값
    private float elapsedTime = 0f;    // 경과 시간

    private XRGrabInteractable grab;

    

    void Start()
    {
        // 자식 오브젝트의 현재 회전 값을 가져옴
        startRotation = childObject.transform.rotation;

        // 목표 회전 값 설정 (Y 값만 90으로 설정)
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
        float elapsedTime = 0f;  // 경과 시간 초기화

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;  // 경과 시간 갱신

            // Lerp를 사용하여 회전 값 부드럽게 보간
            childObject.transform.rotation = Quaternion.Lerp(startRotation, endRotation, elapsedTime / duration);

            yield return null;  // 다음 프레임까지 기다림
        }

        // 마지막으로 목표 회전 값을 정확히 적용
        childObject.transform.rotation = endRotation;
    }
}
