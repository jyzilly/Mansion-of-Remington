using UnityEngine;

public class TPhotoResult : MonoBehaviour
{
    private MeshRenderer meshrenderer = null;
    private SphereCollider sphCollider = null;
    private GResponse resTrigger = null;

    private void Awake()
    {
        meshrenderer = GetComponent<MeshRenderer>();
        sphCollider = GetComponent<SphereCollider>();
        resTrigger = GetComponent<GResponse>();
    }
    private void Start()
    {
        meshrenderer.enabled = false;
        sphCollider.enabled = false;
        resTrigger.OnResponseCallback = TransferOBJ;
    }

    private void TransferOBJ(bool _State)
    {
        if (_State)
        {
            meshrenderer.enabled = true;
            sphCollider.enabled = true;
            transform.GetComponent<Rigidbody>().isKinematic = false;
        }
        else
        {
            meshrenderer.enabled = false;
            sphCollider.enabled = false;
        }
    }
}
