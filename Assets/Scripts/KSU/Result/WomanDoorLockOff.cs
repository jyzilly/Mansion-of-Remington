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
        Debug.Log("������");

        // layer ���� (���̶� �浹�ȵǴ� layer��)
        int layerToSet = LayerMask.NameToLayer("Interaction");
        Transform[] allTransforms = WomanTutoriialDoor.GetComponentsInChildren<Transform>(true);
        foreach (Transform t in allTransforms)
        {
            Debug.Log(layerToSet);
            Debug.Log(t.gameObject);
            t.gameObject.layer = layerToSet;
        }

        // ��乮 ����
        WomanTutoriialDoor.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        WomanTutoriialDoor.GetComponent<Rigidbody>().isKinematic = false;
    }
}
