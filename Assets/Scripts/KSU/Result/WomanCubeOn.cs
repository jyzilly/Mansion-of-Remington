using UnityEngine;

[RequireComponent(typeof(GResponse))]
public class WomanCubeOn : MonoBehaviour
{
    public GameObject CubeInWomanRoom;
    private GResponse result;

    private void Awake()
    {
        result = GetComponent<GResponse>();
    }

    private void Start()
    {
        result.OnResponseCallback += SetCubeOn;
    }


    // 기자 방에 큐브가 생성되게 하는거
    private void SetCubeOn(bool _state)
    {
        CubeInWomanRoom.SetActive(true);
    }
}
