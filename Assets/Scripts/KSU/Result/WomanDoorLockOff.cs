using UnityEngine;

[RequireComponent(typeof(GResponse))]
public class WomanDoorLockOff : MonoBehaviour
{
    public GameObject WomanTutoriialDoor;
    private GResponse result;

    private void Awake()
    {
       result = GetComponent<GResponse>();
    }

    private void Start()
    {
        result.OnResponseCallback += DoorLockOff;
    }

    private void DoorLockOff(bool _state)
    {
        Debug.Log("문열림");

        // layer 변경 (벽이랑 충돌안되는 layer로)
        int layerToSet = LayerMask.NameToLayer("Interaction");
        Transform[] allTransforms = WomanTutoriialDoor.GetComponentsInChildren<Transform>(true);
        foreach (Transform t in allTransforms)
        {
            Debug.Log(layerToSet);
            Debug.Log(t.gameObject);
            t.gameObject.layer = layerToSet;
        }

        // 잠긴문 해제
        WomanTutoriialDoor.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        WomanTutoriialDoor.GetComponent<Rigidbody>().isKinematic = false;
    }
}
