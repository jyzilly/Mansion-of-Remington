using UnityEngine;

public class TestPlayer : MonoBehaviour
{
    public GameObject prefab1; // 橇府普 1

    public GameObject prefab2;

    void Start()
    {
        // 橇府普 1阑 积己
        Instantiate(prefab1, transform.position, Quaternion.identity);

        // 10檬 第俊 橇府普 2 积己
        Invoke("SpawnPrefab2", 10f);
    }

    void SpawnPrefab2()
    {
        // 橇府普 2甫 积己
        Instantiate(prefab2, transform.position + Vector3.right * 2, Quaternion.identity);
    }
}
