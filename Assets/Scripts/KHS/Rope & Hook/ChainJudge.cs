using UnityEngine;

public class ChainJudge : MonoBehaviour
{
    #region
    public delegate void ChainJudgeInDelegate(Transform _tr, Color _color);
    public delegate void ChainJudgeOutDelegate(Transform _tr, Color _color);

    private ChainJudgeInDelegate chainJudgeInCallback;
    private ChainJudgeOutDelegate chainJudgeOutCallback;

    public ChainJudgeInDelegate ChainJudgeInCallback
    {
        get { return chainJudgeInCallback; }
        set { chainJudgeInCallback = value; }
    }
    public ChainJudgeOutDelegate ChainJudgeOutCallback
    {
        get { return chainJudgeOutCallback; }
        set { chainJudgeOutCallback = value; }
    }
    #endregion

    private Vector3 pathVec = Vector3.zero;
    private float threshold = 0.9f;

    private void OnTriggerEnter(Collider _chrCollider)
    {
        if(LightJudgeFunc(_chrCollider))
        {
            Debug.Log("character Index : " + _chrCollider.name);
            ChainJudgeInCallback?.Invoke(_chrCollider.transform, Color.red);
        }
    }
    private void OnTriggerExit(Collider _chrCollider)
    {
        if (_chrCollider.GetComponent<ChangeTextColor>())
        {
            Debug.Log("Off character Index : " + _chrCollider.name);
            ChainJudgeOutCallback?.Invoke(_chrCollider.transform, Color.white);
        }
    }

    private bool LightJudgeFunc(Collider _chrCollider)
    {
        ChangeTextColor tempCHWA = null;
        if (tempCHWA = _chrCollider.GetComponent<ChangeTextColor>())
        {
            float dotProduct = Vector3.Dot(tempCHWA.transform.up, transform.up);

            if (Mathf.Abs(dotProduct) >= threshold)
            {
                //Debug.Log("On dotProduct : "+ dotProduct);
                return true;
            }
            else
            {
                //Debug.Log("OFF dotProduct : "+ dotProduct);
                return false;
            }
        }
        else
        {
            return false;
        }
    }
    private void OnDrawGizmos()
    {
        // 로컬 X축 (빨강)
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + transform.right);

        // 로컬 Y축 (초록)
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + transform.up);

        // 로컬 Z축 (파랑)
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, transform.position + transform.forward);
    }
}
