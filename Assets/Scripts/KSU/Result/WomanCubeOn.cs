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


    // ���� �濡 ť�갡 �����ǰ� �ϴ°�
    private void SetCubeOn(bool _state)
    {
        CubeInWomanRoom.SetActive(true);
    }
}
