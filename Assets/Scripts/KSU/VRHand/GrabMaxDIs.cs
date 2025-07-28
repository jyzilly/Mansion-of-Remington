using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class GrabMaxDIs : MonoBehaviour
{
    public float maxDis;
    public GameObject attachGo;
    public XRGrabInteractable grabInteractable;

    private GameObject attachHand;
    private float curDistance;

    private void Awake()
    {
        grabInteractable.selectEntered.AddListener(GrabOn);
    }

    private void Update()
    {
        if (attachHand != null)
        {
            curDistance = Vector3.Distance(attachGo.transform.position, attachHand.transform.position);

            if (curDistance > maxDis)
            {
                grabInteractable.enabled = false;
                grabInteractable.enabled = true;
                attachHand = null;
                Debug.Log("±×·¦ ¶³¾îÁü");
            }
        }
    }

    private void GrabOn(SelectEnterEventArgs args)
    {
        attachHand = args.interactorObject.transform.gameObject;
    }
}
