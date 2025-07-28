using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class HookAttach : MonoBehaviour
{
    public delegate void HookArrivedDelegate();

    private HookArrivedDelegate hookArrivedCallback = null;
    
    public HookArrivedDelegate HookArrivedCallback
    {
        get { return hookArrivedCallback; }
        set { hookArrivedCallback = value; }
    }

    [SerializeField]
    private Vector3 noRopePos = Vector3.zero;
    [SerializeField]
    private Vector3 onRopePos = Vector3.zero;
    [SerializeField]
    private float lerpratio = 0.02f;
    public GameObject ropeGo = null;

    [SerializeField]
    public bool activeTrigger = false;
    private bool isActive = false;
    private bool isArrived = false;

    private Transform parentTr = null;
    private Transform chainTr = null;

    public delegate void HookAttachDelegate();
    private HookAttachDelegate hookAttachCallback = null;

    public HookAttachDelegate HookAttachCallback
    {
        get { return hookAttachCallback; }
        set { hookAttachCallback = value; }
    }

    private void Start()
    {
        parentTr = transform.parent.transform;
        chainTr = ropeGo.transform;
        parentTr.localPosition = onRopePos;
        chainTr.localPosition = Vector3.zero;
        isActive = false;
    }

    private void FixedUpdate()
    {
        if(activeTrigger && !isActive)
        {
            isActive = true;
            StartCoroutine(FirstPositionMove());
        }
    }

    private void OnTriggerEnter(Collider _collider)
    {
        if(isActive && _collider.name == "ChainLink")
        {
            Debug.Log("Rope Attact!");
            hookAttachCallback?.Invoke();
            ropeGo.SetActive(true);
            StartCoroutine(StayPositionMove());
        }
    }

    private IEnumerator StayPositionMove()
    {
        yield return new WaitForSeconds(0.5f);
        while(!isArrived)
        { 
            if((parentTr.localPosition - onRopePos).magnitude <= 0.1f)
            {
                isArrived = true;
            }
            else
            {
                parentTr.localPosition = Vector3.Lerp(parentTr.localPosition, onRopePos, lerpratio);
                yield return new WaitForSeconds(0.01f);
            }
        }
        Debug.Log("Arrived!");
        HookArrivedCallback?.Invoke();
        isArrived = false;
    }
    private IEnumerator FirstPositionMove()
    {
        yield return new WaitForSeconds(0.5f);
        while (!isArrived)
        {
            if ((parentTr.localPosition - noRopePos).magnitude <= 0.1f)
            {
                isArrived = true;
            }
            else
            {
                parentTr.localPosition = Vector3.Lerp(parentTr.localPosition, noRopePos, lerpratio);
                yield return null;
            }
        }
        Debug.Log("Arrived!");
        isArrived = false;
    }
}
