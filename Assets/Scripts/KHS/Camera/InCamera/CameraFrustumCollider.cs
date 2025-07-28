using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(MeshFilter), typeof(MeshCollider))]
public class CameraFrustumCollider : MonoBehaviour
{
    public Camera targetCamera;  // 대상 카메라
    public float detectionDistance = 2f;  // 제한 거리

    private MeshFilter meshFilter = null;
    private MeshCollider meshCollider = null;

    public CameraScreen camScreen;
    public PlayerCameraController playerControl;

    [SerializeField]
    private List<GameObject> onTriggerCap = new List<GameObject>();
    [SerializeField]
    private List<GameObject> onTriggerTrans = new List<GameObject>();

    [SerializeField]
    private bool presentCam = false;

    private bool oneTime = false;


    private void Awake()
    {
        targetCamera = GetComponent<Camera>();
        camScreen = FindAnyObjectByType<CameraScreen>();
        playerControl = FindAnyObjectByType<PlayerCameraController>();
        meshFilter = GetComponent<MeshFilter>();
        meshCollider = GetComponent<MeshCollider>();
    }
    private void Start()
    {
        if(transform.name == "PresentCamera")
        {
            presentCam = true;
        }
        if (targetCamera == null)
        {
            targetCamera = GetComponent<Camera>();
        }

        if (targetCamera == null)
        {
            Debug.LogError("CameraFrustumCollider: Camera component not found!");
            return;
        }

        CreateFrustumMesh();
        FrustumColliderSwitch();
    }

    private void Update()
    {
        if (playerControl != null && camScreen != null && !oneTime)
        {
            camScreen.CamColliderSwitchCallback += FrustumColliderSwitch;
            playerControl.CaptureCallback += OnCapture;
            playerControl.TransferCallback += OnTransfer;
            oneTime = true;
        }
    }


    private void OnTriggerEnter(Collider _collider)
    {
        if(_collider?.GetComponent<GeneratePhoto>())
        {
            onTriggerCap.Add(_collider.gameObject);
            onTriggerCap.Sort((a, b) =>
            {
                int priorityA = a.GetComponent<GeneratePhoto>()?.gimmickPriority ?? int.MaxValue;
                int priorityB = b.GetComponent<GeneratePhoto>()?.gimmickPriority ?? int.MaxValue;

                return priorityA.CompareTo(priorityB);
            });
        }
        if (_collider?.GetComponent<TransferPhoto>())
        {
            onTriggerTrans.Add(_collider.gameObject);
            onTriggerTrans.Sort((a, b) =>
            {
                int priorityA = a.GetComponent<TransferPhoto>()?.gimmickPriority ?? int.MaxValue;
                int priorityB = b.GetComponent<TransferPhoto>()?.gimmickPriority ?? int.MaxValue;

                return priorityA.CompareTo(priorityB);
            });
        }
    }
    private void OnTriggerExit(Collider _collider)
    {
        if (_collider?.GetComponent<GeneratePhoto>())
        {
            onTriggerCap.Remove(_collider.gameObject);
        }
        if (_collider?.GetComponent<TransferPhoto>())
        {
            onTriggerTrans.Remove(_collider.gameObject);
        }
    }

    private void CreateFrustumMesh()
    {
        // 카메라 Frustum의 8개 꼭짓점 계산
        Vector3[] corners = new Vector3[8];

        // 가까운 평면 (Near Clip Plane)
        float near = targetCamera.nearClipPlane;
        float far = detectionDistance;
        float halfHeightNear = Mathf.Tan(targetCamera.fieldOfView * Mathf.Deg2Rad / 2f) * near;
        float halfWidthNear = halfHeightNear * targetCamera.aspect;
        float halfHeightFar = Mathf.Tan(targetCamera.fieldOfView * Mathf.Deg2Rad / 2f) * far;
        float halfWidthFar = halfHeightFar * targetCamera.aspect * 0.7f;

        // Near Plane
        corners[0] = new Vector3(-halfWidthNear, -halfHeightNear, near);
        corners[1] = new Vector3(halfWidthNear, -halfHeightNear, near);
        corners[2] = new Vector3(halfWidthNear, halfHeightNear, near);
        corners[3] = new Vector3(-halfWidthNear, halfHeightNear, near);

        // Far Plane
        corners[4] = new Vector3(-halfWidthFar, -halfHeightFar, far);
        corners[5] = new Vector3(halfWidthFar, -halfHeightFar, far);
        corners[6] = new Vector3(halfWidthFar, halfHeightFar, far);
        corners[7] = new Vector3(-halfWidthFar, halfHeightFar, far);

        // Mesh 생성
        Mesh mesh = new Mesh();
        mesh.vertices = corners;
        mesh.triangles = new int[]
        {
            // Near Plane
            0, 1, 2,
            2, 3, 0,
            // Far Plane
            4, 5, 6,
            6, 7, 4,
            // Sides
            0, 1, 5,
            5, 4, 0,
            1, 2, 6,
            6, 5, 1,
            2, 3, 7,
            7, 6, 2,
            3, 0, 4,
            4, 7, 3
        };

        mesh.RecalculateNormals();

        // MeshFilter와 MeshCollider에 적용
        meshFilter.mesh = mesh;
        meshCollider.sharedMesh = mesh;
        meshCollider.convex = true;  // Trigger로 사용하려면 Convex 설정 필요
        meshCollider.isTrigger = true;
    }
    private void FrustumColliderSwitch()
    {
        if(camScreen.IsPast == presentCam)
        {
            meshCollider.enabled = false;
            onTriggerCap.Clear();
            onTriggerTrans.Clear();
        }
        else
        {
            meshCollider.enabled = true;
            onTriggerCap.Clear();
            onTriggerTrans.Clear();
        }
    }
    private void OnCapture()
    {
        if (onTriggerCap.Count > 0)
        {
            Debug.Log("Capture On");
            onTriggerCap[0].GetComponent<GeneratePhoto>().OnPhoto();
            if (onTriggerCap[0].GetComponent<FireBurnOutShadingChain>() != null)
            {
                onTriggerCap[0].GetComponent<FireBurnOutShadingChain>().FireFadeOut();
                onTriggerCap.RemoveAt(0);
            }
            else if (!onTriggerCap[0].GetComponent<NoUnactiveObj>())
            {
                onTriggerCap[0].gameObject.SetActive(false);
                onTriggerCap.RemoveAt(0);
            }
        }
        else
        {
            Debug.Log("No Capture");
        }
    }
    private void OnTransfer()
    {
        if (onTriggerTrans.Count > 0)
        {
            Debug.Log("Transfer On");
            onTriggerTrans[0].GetComponent<TransferPhoto>().OnTransfer();
            onTriggerTrans[0].gameObject.SetActive(false);
            onTriggerTrans.RemoveAt(0);
        }
        else
        {
            Debug.Log("No Transfer");
        }
    }
}
