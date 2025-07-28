using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class ChainLimit : MonoBehaviour
{
    public XRGrabInteractable GrabInteractable;
    public HingeJoint hJoint = null;
    

    public Transform chainStart; // ��罽�� ���� ��
    public Transform chainEnd;   // ��罽�� �ٸ� �� ��
    public float maxChainLength = 7.3f; // ��罽�� �ִ� ����

    private Vector3 startToEnd; // ���ۿ��� �������� ����

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
                Debug.Log("Ž��");
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
