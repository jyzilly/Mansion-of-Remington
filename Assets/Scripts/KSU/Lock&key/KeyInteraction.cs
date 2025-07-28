using UnityEngine;

[RequireComponent(typeof(GCondition))]
public class KeyInteraction : MonoBehaviour
{
    [SerializeField]
    [Tooltip("ȸ���� key�� Transform")]
    private Transform keyTr;

    private GCondition solve;


    public bool inserted = false; // Ű�� ���� �ִ��� ����
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
                Debug.Log("���� ����");
                solve.OnSolvedCallback?.Invoke(true);
            }
        }
    }
}
