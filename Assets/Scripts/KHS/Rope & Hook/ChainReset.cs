using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class ChainReset : MonoBehaviour
{
    public float maxVelocity = 20f;
    public float maxAngularVelocity = 50f;

    public List<Rigidbody> chainLinks; // 체인을 구성하는 Rigidbody 배열
    public Vector3[] initialPositions; // 초기 위치 저장
    public Quaternion[] initialRotations; // 초기 회전 저장
    public HookAttach hookAttach;

    private bool isInit = false;

    private void Awake()
    {
        chainLinks = GetComponentsInChildren<Rigidbody>().ToList();
        chainLinks.RemoveAt(0);
    }
    void Start()
    {
        isInit = false;
        // 초기 위치와 회전 값 저장
        initialPositions = new Vector3[chainLinks.Count];
        initialRotations = new Quaternion[chainLinks.Count];
        RecordInitialize();
        if (hookAttach != null)
        {
            hookAttach.HookArrivedCallback += RecordInitialize;
        }
    }

    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.V))
        {
            ResetChainPhysics();
        }
        if(CheckWeirdChain() && isInit)
        {
            ResetChainPhysics();
        }
    }
    private bool CheckWeirdChain()
    {
        foreach (Rigidbody rb in chainLinks)
        {
            if (rb.linearVelocity.magnitude > maxVelocity || rb.angularVelocity.magnitude > maxAngularVelocity)
            {
                return true;
            }
        }
        return false;
    }

    private void RecordInitialize()
    {
        Debug.Log("타란!");
        for (int i = 0; i < chainLinks.Count; i++)
        {
            // Rigidbody의 위치와 회전 초기화
            initialPositions[i] = chainLinks[i].transform.position;
            initialRotations[i] = chainLinks[i].transform.rotation;
        }
        isInit = true;
    }
    public void ResetChainPhysics()
    {
        Debug.Log("초기화 진입");
        foreach(Rigidbody rb in chainLinks)
        {
            XRGrabInteractable grab = null;
            if(grab = rb.GetComponent<XRGrabInteractable>())
            {
                grab.enabled = false;
                grab.enabled = true;
            }
            rb.isKinematic = true;
        }
        for (int i = 0; i < chainLinks.Count; i++)
        {
            // Rigidbody의 위치와 회전 초기화
            chainLinks[i].transform.position = initialPositions[i];
            chainLinks[i].transform.rotation = initialRotations[i];

            chainLinks[i].isKinematic = false;
            // 물리 상태 초기화
            chainLinks[i].linearVelocity = Vector3.zero;
            chainLinks[i].angularVelocity = Vector3.zero;
        }

    }
}