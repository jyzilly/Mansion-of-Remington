using System.Collections;
using UnityEngine;

public class GPhotoResult : MonoBehaviour
{
    //private Material mat = null;
    private MeshRenderer meshRenderer = null;
    private BoxCollider boxCollider = null;
    private GResponse resTrigger = null;
    private FireBurnOutShading matEffect = null;
    

    private void Awake()
    {
        //mat = GetComponent<MeshRenderer>().material;
        meshRenderer = GetComponent<MeshRenderer>();
        boxCollider = GetComponent<BoxCollider>();
        resTrigger = GetComponent<GResponse>();
        matEffect = GetComponent<FireBurnOutShading>();
    }
    private void Start()
    {
        meshRenderer.enabled = false;
        boxCollider.enabled = false;
        resTrigger.OnResponseCallback = CreatePhoto;
    }
    private void CreatePhoto(bool _State)
    {
        Debug.Log(_State);
        if (_State)
        {
            meshRenderer.enabled = true;
            boxCollider.enabled = true;
            if (matEffect != null)
            {
                matEffect.FireFadeIn();
            }
        }
    }
}
