using UnityEngine;

public class TempResponse : MonoBehaviour
{
    [SerializeField]
    private Material mat = null;
    [SerializeField]
    private GResponse resTrigger = null;

    [SerializeField]
    private bool isRun = false;

    private void Awake()
    {
        mat = GetComponentInChildren<MeshRenderer>().material;
        resTrigger = GetComponent<GResponse>();
    }
    private void Start()
    {
        isRun = false;
        resTrigger.OnResponseCallback = BlinkDoor;
    }

    private void BlinkDoor(bool _State)
    {
        if (_State)
        {
            isRun = true;
            mat.EnableKeyword("_EMISSION");
        }
        else
        {
            isRun = false;
            mat.DisableKeyword("_EMISSION");
        }
    }

}
