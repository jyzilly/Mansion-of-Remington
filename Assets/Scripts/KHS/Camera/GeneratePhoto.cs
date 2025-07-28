using UnityEngine;

public class GeneratePhoto : MonoBehaviour
{
    public int gimmickPriority = 0;
    private GCondition conTrigger = null;

    private void Awake()
    {
        conTrigger = GetComponent<GCondition>();
    }
    public void OnPhoto()
    {
        Debug.Log("OnPhoto : " + gameObject.name);
        conTrigger.OnSolved(true);
    }
}
