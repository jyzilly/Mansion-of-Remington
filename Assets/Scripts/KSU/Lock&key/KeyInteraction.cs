using UnityEngine;

[RequireComponent(typeof(GCondition))]
public class KeyInteraction : MonoBehaviour
{
    [SerializeField]
    [Tooltip("회전할 key의 Transform")]
    private Transform keyTr;

    private GCondition solve;


    public bool inserted = false; // 키가 꽂혀 있는지 여부
    private bool opend = false; // 

    private void Start()
    {
        solve = GetComponent<GCondition>();
    }

    private void Update()
    {
        if (inserted)
        {
            Debug.Log(keyTr.rotation.y);
            if (keyTr.rotation.y >= 0 && !opend)
            {
                opend = true;
                Debug.Log("열쇠 열림");
                solve.OnSolvedCallback?.Invoke(true);
            }
        }
    }
}
