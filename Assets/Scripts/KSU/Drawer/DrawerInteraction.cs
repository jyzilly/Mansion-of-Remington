using UnityEngine;
using System.Collections;

public class DrawerInteraction : MonoBehaviour
{
    public bool IsInteraction = false;
    public bool IsOpen = false;
    public float interactionDis = 0f;
    public float lerpTime = 0f;


    private void Update()
    {
        if (IsInteraction && !IsOpen)
        {
            interactionDis = Mathf.Abs(interactionDis);
            StartCoroutine(Interaction());
            IsInteraction = false;
            IsOpen = true;
        }
        else if (IsInteraction && IsOpen)
        {
            interactionDis = -interactionDis;
            StartCoroutine(Interaction());
            IsInteraction = false;
            IsOpen = false;
        }
    }
    private IEnumerator Interaction()
    {
        float timeElapsed = 0f;
        Vector3 startPos = transform.position;
        Vector3 targetPos = startPos + transform.right * interactionDis;

        while (timeElapsed < lerpTime)
        {
            timeElapsed += Time.deltaTime;
            float lerpValue = timeElapsed / lerpTime;

            transform.position = Vector3.Lerp(startPos, targetPos, lerpValue);

            yield return null;
        }

        transform.position = targetPos;
    }
}
