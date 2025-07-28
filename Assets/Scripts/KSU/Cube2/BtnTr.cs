using UnityEngine;

public class BtnTr : MonoBehaviour
{
    [SerializeField]
    private Transform btnBody; // 실제 버튼
    [SerializeField]
    private Vector3 offset; // 차이 거리?

    private void Update()
    {
        btnBody.position = transform.position - offset;
    }

}
