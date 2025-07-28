using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class EndingWoman : MonoBehaviour
{
    public NetworkManager networkGM;
    private XRGrabInteractable grab;

    private void Start()
    {
        grab = GetComponent<XRGrabInteractable>();
    }

    private void Update()
    {
        if (grab.isSelected)
        {
            networkGM.endingDelegate?.Invoke();
        }
    }
}