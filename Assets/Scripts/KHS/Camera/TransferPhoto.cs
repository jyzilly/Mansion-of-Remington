using UnityEngine;

public class TransferPhoto : MonoBehaviour
{
    public int gimmickPriority = 0;
    private GCondition conTrigger = null;

    private void Awake()
    {
        conTrigger = GetComponent<GCondition>();
    }
    public void OnTransfer()
    {
        Debug.Log("OnTransfer : " + gameObject.name);
        conTrigger.OnSolved(true);
    }
}
