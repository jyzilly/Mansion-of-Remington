using UnityEngine;

public class SetActivePlane : MonoBehaviour
{
    public Renderer render;
    private Renderer rd;

    private void Start()
    {
        rd = GetComponent<Renderer>();
    }

    private void Update()
    {
        if (render.enabled == true)
        {
            rd.enabled = true;
        }
        else
        {
            rd.enabled = false;
        }
    }
}
