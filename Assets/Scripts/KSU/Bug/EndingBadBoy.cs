using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class EndingBadBoy : MonoBehaviour
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
            networkGM.boyBadEnding = true;
            networkGM.endingDelegate?.Invoke();
        }
    }
}
