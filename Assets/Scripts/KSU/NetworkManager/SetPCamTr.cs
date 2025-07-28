using UnityEngine;

public class SetPCamTr : MonoBehaviour
{
    [SerializeField]
    private GameObject pCam;
    [SerializeField]
    private CameraScreen pCameraScreen;
    [SerializeField]
    private PlayerCameraController playercamController;
    [SerializeField]
    private Transform LeftHand;
    [SerializeField]
    private Transform RightHand;


    private GameObject pastcam;
    private CrankController crank;

    void Start()
    {
        pastcam = GameObject.Find("PastCamera");

        pastcam.GetComponent<TrackingCamera>().pCamTr = pCam.transform;
        pastcam.GetComponent<CameraFrustumCollider>().camScreen = pCameraScreen;
        pastcam.GetComponent<CameraFrustumCollider>().playerControl = playercamController;
        crank = GameObject.FindAnyObjectByType<CrankController>();
        crank.LeftDeviceTr = LeftHand;
        crank.RightDeviceTr = RightHand;
    }
}
