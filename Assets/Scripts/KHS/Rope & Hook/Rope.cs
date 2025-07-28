
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;


public class Rope : MonoBehaviour
{
    [SerializeField]
    private HookAttach hook = null;
    [SerializeField]
    private GameObject parentGo = null;
    private XRGrabInteractable grab = null;



    private void Awake()
    {
        hook = FindAnyObjectByType<HookAttach>();
        parentGo = transform.parent.gameObject;
        grab = GetComponent<XRGrabInteractable>();
    }
    private void Start()
    {
        hook.HookAttachCallback += SetAttach;
    }

    private void SetAttach()
    {
        Debug.Log("SetAttach ¡¯¿‘");
        grab.enabled = false;
        parentGo.SetActive(false);
    }
}
