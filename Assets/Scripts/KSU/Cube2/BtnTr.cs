using UnityEngine;

public class BtnTr : MonoBehaviour
{
    [SerializeField]
    private Transform btnBody; // ���� ��ư
    [SerializeField]
    private Vector3 offset; // ���� �Ÿ�?

    private void Update()
    {
        btnBody.position = transform.position - offset;
    }

}
