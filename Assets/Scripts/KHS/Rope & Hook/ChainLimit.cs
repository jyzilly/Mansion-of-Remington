using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class ChainLimit : MonoBehaviour
{
    public XRGrabInteractable GrabInteractable;
    public HingeJoint hJoint = null;
    

    public Transform chainStart; // 쇠사슬의 한쪽 끝
    public Transform chainEnd;   // 쇠사슬의 다른 쪽 끝
    public float maxChainLength = 7.3f; // 쇠사슬의 최대 길이

    private Vector3 startToEnd; // 시작에서 끝까지의 벡터

    void FixedUpdate()
    {
        bool isGrabbed = GrabInteractable.isSelected;

        if (isGrabbed)
        {
            Vector3 startToEnd = chainEnd.position - chainStart.position;
            float gCurrentLength = startToEnd.magnitude;
            Debug.Log(hJoint.currentForce.magnitude);
            
            if (hJoint.currentForce.magnitude >= 850f)
            {
                Debug.Log("탐지");
                GrabReset();
            }
            
        }
    }

    private void GrabReset()
    {
        StartCoroutine(GrabResetCoroutine());
    }

    private IEnumerator GrabResetCoroutine()
    {

        GrabInteractable.enabled = false;
        yield return new WaitForSeconds(0.1f);
        GrabInteractable.enabled = true;

        yield return null;
    }
}
