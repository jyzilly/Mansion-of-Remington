using UnityEngine;

public class RenderHand : MonoBehaviour
{
    public Transform trackController;
    public Transform physicHand;
    public GameObject nonePhysicHand;
    public float activeDis = 0.05f;

    private void Update()
    {
        float dis = Vector3.Distance(physicHand.position, trackController.position);

        if (dis > activeDis)
        {
            nonePhysicHand.SetActive(true);
        }
        else
        {
            nonePhysicHand.SetActive(false);
        }
    }

}
