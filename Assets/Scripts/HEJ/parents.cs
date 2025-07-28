using UnityEngine;
using UnityEngine.Assertions.Must;

public class parents : MonoBehaviour
{
    private glass glass;

    private Rigidbody[] rbs = null;
    private GlassClick glassClick = null;

    private void Awake()
    {
        glass = GetComponentInParent<glass>();

        rbs = GetComponentsInChildren<Rigidbody>();
        glassClick = GetComponentInChildren<GlassClick>();
        glassClick.OnGlassClickCallback = OnGlassClickCallback;
    }
    private void Start()
    {
        GetComponent<Renderer>().enabled = false;
    }

   
    private void OnKinematicAll(bool _isOn)
    {
        foreach (Rigidbody rb in rbs)
            rb.isKinematic = _isOn;
        glass.glassNum++;
    }

    private void OnGlassClickCallback()
    {
        OnKinematicAll(false);
    }
}
