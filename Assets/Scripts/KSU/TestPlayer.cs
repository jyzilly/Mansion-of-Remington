using UnityEngine;

public class TestPlayer : MonoBehaviour
{
    public GameObject prefab1; // ������ 1

    public GameObject prefab2;

    void Start()
    {
        // ������ 1�� ����
        Instantiate(prefab1, transform.position, Quaternion.identity);

        // 10�� �ڿ� ������ 2 ����
        Invoke("SpawnPrefab2", 10f);
    }

    void SpawnPrefab2()
    {
        // ������ 2�� ����
        Instantiate(prefab2, transform.position + Vector3.right * 2, Quaternion.identity);
    }
}
