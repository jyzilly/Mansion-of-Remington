using UnityEngine;

public class TestSetParent : MonoBehaviour
{
    public GameObject parent;
    public GameObject child;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            Debug.Log("true�ϋ�");
            child.transform.SetParent(parent.transform, true);
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            Debug.Log("false�ϋ�");
            child.transform.SetParent(parent.transform, false);
        }
    }
}
