using UnityEngine;

public class SetBoyInstance : MonoBehaviour
{
    private CrankController crank;
    [SerializeField]
    private Transform LeftHand;
    [SerializeField]
    private Transform RightHand;

    void Start()
    {
        crank = GameObject.FindAnyObjectByType<CrankController>();
        crank.LeftDeviceTr = LeftHand;
        crank.RightDeviceTr = RightHand;
    }
}
