using Photon.Pun;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Inputs;

public class StartSetting : MonoBehaviourPun
{
    private InputActionManager manager;
    private XROrigin origin;
    private void Awake()
    {
        manager = GetComponent<InputActionManager>();
        origin = GetComponent<XROrigin>();
    }

    private void Start()
    {
        if (photonView.IsMine)
        {
            manager.enabled = true;
            origin.enabled = true;
        }
    }
}
