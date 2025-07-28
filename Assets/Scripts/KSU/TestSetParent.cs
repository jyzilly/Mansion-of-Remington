using UnityEngine;

public class TestSetParent : MonoBehaviour
{
    public GameObject parent;
    public GameObject child;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            Debug.Log("trueÀÏ‹š");
            child.transform.SetParent(parent.transform, true);
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            Debug.Log("falseÀÏ‹š");
            child.transform.SetParent(parent.transform, false);
        }
    }
}
