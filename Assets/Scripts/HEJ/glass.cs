using UnityEngine;
using System.Collections;

public class glass : MonoBehaviour
{

    public delegate void GlassDelegate(glass _glass);
    public delegate void GlassSucessDelegate();

    public GlassSucessDelegate glassSucessCallback;
    private GlassDelegate glassCallback = null;

    public int glassNum;
    public Collider[] colliders;
    parents parents;

    public GlassDelegate GlassCallback
    {
        set { glassCallback = value; }
    }

    private void Awake()
    {
        glassNum = 0;

        parents = GetComponent<parents>();

        colliders = GetComponentsInChildren<Collider>();

        foreach(Collider col in colliders)
        {
            col.GetComponent<Renderer>().enabled = false;
            Rigidbody rb = col.gameObject.GetComponent<Rigidbody>();
            rb.isKinematic = true;
            // 전체 움직임 제한
            rb.constraints = (RigidbodyConstraints)126;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Hammer")
        {
            // 기존 상태의 유리창 예외처리
            GetComponent<Renderer>().enabled = false;

            foreach (Collider col in colliders)
            {
                // if (col.name == "12") continue;
                col.gameObject.GetComponent<Renderer>().enabled = true;

                Rigidbody rb = col.gameObject.GetComponent<Rigidbody>();

                // 전체 움직임 풀어줌
                rb.constraints = (RigidbodyConstraints)0;
            }
        }
    }

    private void Update()
    {
        if (glassNum == 4)
        {
            // 콜백
            glassSucessCallback?.Invoke();
            glassNum = 0;
        }
    }
}
