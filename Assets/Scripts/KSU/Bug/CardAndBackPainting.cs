using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class CardAndBackPainting : MonoBehaviour
{
    private XRGrabInteractable grab;
    public Collider Collider;

    private void Start()
    {
        grab = GetComponent<XRGrabInteractable>();
    }

    private void Update()
    {
        if (grab.isSelected)
        {
            Collider.enabled = false;
        }
    }
}
