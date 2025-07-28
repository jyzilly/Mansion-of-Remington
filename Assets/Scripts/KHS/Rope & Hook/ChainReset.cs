using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class ChainReset : MonoBehaviour
{
    public float maxVelocity = 20f;
    public float maxAngularVelocity = 50f;

    public List<Rigidbody> chainLinks; // ü���� �����ϴ� Rigidbody �迭
    public Vector3[] initialPositions; // �ʱ� ��ġ ����
    public Quaternion[] initialRotations; // �ʱ� ȸ�� ����
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
        // �ʱ� ��ġ�� ȸ�� �� ����
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
        Debug.Log("Ÿ��!");
        for (int i = 0; i < chainLinks.Count; i++)
        {
            // Rigidbody�� ��ġ�� ȸ�� �ʱ�ȭ
            initialPositions[i] = chainLinks[i].transform.position;
            initialRotations[i] = chainLinks[i].transform.rotation;
        }
        isInit = true;
    }
    public void ResetChainPhysics()
    {
        Debug.Log("�ʱ�ȭ ����");
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
            // Rigidbody�� ��ġ�� ȸ�� �ʱ�ȭ
            chainLinks[i].transform.position = initialPositions[i];
            chainLinks[i].transform.rotation = initialRotations[i];

            chainLinks[i].isKinematic = false;
            // ���� ���� �ʱ�ȭ
            chainLinks[i].linearVelocity = Vector3.zero;
            chainLinks[i].angularVelocity = Vector3.zero;
        }

    }
}