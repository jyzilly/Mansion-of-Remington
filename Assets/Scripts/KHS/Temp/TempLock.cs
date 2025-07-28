using UnityEngine;

public class TempLock : MonoBehaviour
{
    [SerializeField]
    private bool isLock = false;
    [SerializeField]
    private Material mat = null;
    [SerializeField]
    private GCondition conTrigger = null;

    public bool isSolve = false;
    

    private void Awake()
    {
        mat = GetComponentInChildren<MeshRenderer>().material;
        conTrigger = GetComponent<GCondition>();
    }

    private void Start()
    {
        isSolve = false;
        isLock = true;
    }

    private void OnTriggerEnter(Collider _collider)
    {
        if(_collider.CompareTag("TempKey"))
        {
            UnLockEffect();
        }
    }
    private void OnTriggerExit(Collider _collider)
    {
        if (_collider.CompareTag("TempKey"))
        {
            LockEffect();
        }
    }

    private void UnLockEffect()
    {
        isLock = false;
        isSolve = true;
        conTrigger.OnSolved(isSolve);

        mat.EnableKeyword("_EMISSION");

    }
    private void LockEffect()
    {
        isLock = true;
        isSolve = false;
        conTrigger.OnSolved(isSolve);

        mat.DisableKeyword("_EMISSION");

    }
}
