using System.Collections;
using UnityEngine;

public class Mane : MonoBehaviour
{
    public delegate void TurnDelegate();

    public TurnDelegate turnCallback = null;

    private Vector3 updateRot = Vector3.zero;
    private float yStart;
    private float yEnd;
    manneManager Manager;

    private bool isRotate = false;

    [SerializeField] private float successAngleY = 0f;

    public Vector3 RotationEuler
    {
        get { return transform.localEulerAngles; }
    }

    private void Awake()
    {
        updateRot = transform.localEulerAngles;
        Manager = GetComponent<manneManager>();
    }

    // 90도 돌리는거
    public void Rotate90()
    {
        if (isRotate) return;
        StartCoroutine(RotateCoroutine(transform.localEulerAngles.y));
    }

    // 돌리는 코루틴
    private IEnumerator RotateCoroutine(float _startY)
    {
        isRotate = true;

        yStart = _startY;
        yEnd = yStart + 90f;

        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime;
            updateRot.y = Mathf.Lerp(yStart, yEnd, t);
            transform.localRotation = Quaternion.Euler(updateRot);
            AudioManager.instance.PlaySfx(AudioManager.sfx.mannequin);
            yield return null;
        }

        if (yEnd >= 360f)
        {
            updateRot.y = 0f;
            transform.localRotation = Quaternion.Euler(updateRot);
            AudioManager.instance.PlaySfx(AudioManager.sfx.mannequin);

        }

        isRotate = false;

        turnCallback?.Invoke();
    }

    // 정답이 맞는지 확인
    public bool IsSuccess()
    {
        return transform.localEulerAngles.y == successAngleY;
    }
}
