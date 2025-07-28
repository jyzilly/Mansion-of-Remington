using UnityEngine;

public class PhysicHand : MonoBehaviour
{
    public Transform target;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        // ��ġ
        rb.linearVelocity = (target.position - transform.position) / Time.deltaTime;

        // ȸ��
        Quaternion rotationDifference = target.rotation * Quaternion.Inverse(transform.rotation);
        rotationDifference.ToAngleAxis(out float angleInDegree, out Vector3 rotationAixs);

        Vector3 rotationDifferenceInDegree = angleInDegree * rotationAixs;

        rb.angularVelocity = (rotationDifferenceInDegree * Mathf.Deg2Rad / Time.fixedDeltaTime);
    }
}
