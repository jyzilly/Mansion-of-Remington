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
        // 애니메이션을 위한 초기 상태와 목표 상태 설정
        float startRotation = 0f;  // 시작 회전 값
        float targetRotation = 90f;  // 목표 회전 값

        Vector3 startScale = lDoor.localScale;  // 시작 스케일 값
        Vector3 startScale2 = rDoor.localScale;
        // 부모의 x 스케일을 고려하여 목표 스케일 설정
        Vector3 targetScale = new Vector3(0.5f * transform.localScale.x, lDoor.localScale.y, lDoor.localScale.z);  // 목표 스케일 값
        Vector3 targetScale2 = new Vector3(-0.5f * transform.localScale.x, rDoor.localScale.y, rDoor.localScale.z);  // 목표 스케일 값

        float elapsedTime = 0f;  // 경과 시간

        while (elapsedTime < 1f)
        {
            // 경과 시간에 비례하여 회전과 스케일 보간
            float t = elapsedTime / 1f;

            // 회전 (Z축)
            float currentRotation = Mathf.Lerp(startRotation, targetRotation, t);
            float currentRotation2 = Mathf.Lerp(-180f, -90f, t);

            lDoor.localRotation = Quaternion.Euler(lDoor.localRotation.eulerAngles.x, lDoor.localRotation.eulerAngles.y, currentRotation);
            rDoor.localRotation = Quaternion.Euler(rDoor.localRotation.eulerAngles.x, rDoor.localRotation.eulerAngles.y, currentRotation2);

            // 스케일 (x축) -> 부모의 영향을 고려하여 변경
            lDoor.localScale = Vector3.Lerp(startScale, targetScale, t);
            rDoor.localScale = Vector3.Lerp(startScale2, targetScale2, t);

            // 경과 시간 증가
            elapsedTime += Time.deltaTime;

            // 다음 프레임으로 대기
            yield return null;
        }

        // 정확한 목표 값에 도달하도록 강제로 설정
        lDoor.localRotation = Quaternion.Euler(lDoor.localRotation.eulerAngles.x, lDoor.localRotation.eulerAngles.y, targetRotation);
        rDoor.localRotation = Quaternion.Euler(rDoor.localRotation.eulerAngles.x, rDoor.localRotation.eulerAngles.y, -90f);
        lDoor.localScale = targetScale;
        rDoor.localScale = targetScale2;
    }
}
