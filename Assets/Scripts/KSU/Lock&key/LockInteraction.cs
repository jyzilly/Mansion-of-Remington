using UnityEngine;
using Photon.Pun;

[RequireComponent(typeof(GCondition))]
public class LockInteraction : MonoBehaviourPun
{
    public delegate void LockOpenDelegate();
    public LockOpenDelegate LockOpenCallback;

    // ÀÚ¹°¼è ¿­¸°°É ¾Ë·ÁÁÜ.
    private GCondition solve;
    private GResponse res;

    private void Awake()
    {
        solve = GetComponent<GCondition>();
        res = GetComponent<GResponse>();
    }

    private void Start()
    {
        res.OnResponseCallback += Interaction;
    }

    public void Interaction(bool _state)
    {
        // ÀÚ¹°¼è open
        Transform childTransform = transform.GetChild(0);
        childTransform.localPosition = childTransform.localPosition + new Vector3(0f, 0.02f, 0f);
        solve.OnSolvedCallback?.Invoke(true);
        LockOpenCallback?.Invoke();
    }
}
