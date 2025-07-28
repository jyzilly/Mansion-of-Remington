using UnityEngine;

public class PadInteraction : MonoBehaviour
{
    public delegate void PadInteractionCallback(string _name);
    public PadInteractionCallback padCallback;

    public void Interaction()
    {
        padCallback?.Invoke(gameObject.name);
    }
}
