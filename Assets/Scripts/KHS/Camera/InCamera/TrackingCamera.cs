using UnityEngine;

public class TrackingCamera : MonoBehaviour
{
    public Transform pCamTr =null;
    [SerializeField]
    private Transform tCamTr = null;


    [SerializeField]
    private Vector3 originMap = Vector3.zero;
    [SerializeField]
    private Vector3 targetMap = Vector3.zero;
    private Vector3 offsetMap = Vector3.zero;

    private void Start()
    {
        offsetMap = targetMap - originMap;
        tCamTr.position = pCamTr.position + offsetMap;
        tCamTr.rotation = pCamTr.rotation;
    }
    private void FixedUpdate()
    {
        tCamTr.position = pCamTr.position + offsetMap;
        tCamTr.rotation = pCamTr.rotation;
    }
}
