using UnityEngine;

public class AttachHand : MonoBehaviour
{
    [SerializeField]
    [Tooltip("손을 붙일 위치")]
    private Transform attachPos;
    [SerializeField]
    [Tooltip("손을 붙일 객체")]
    private GameObject attachGo;

    [Tooltip("손")]
    public GameObject hand = null;

    public bool grapping = false;

    private void Update()
    {
        if (grapping)
        {
            hand.transform.position = attachPos.position;
        }
    }

    // hand를 자식으로 붙임.
    public void SetChildHand()
    {
        Debug.Log(new Vector3(1 / transform.root.localScale.x, 1 / transform.root.localScale.y, 1 / transform.root.localScale.z));
        Debug.Log("hand의 local 스케일" + hand.transform.localScale);
        Debug.Log("hand의 world 스케일" + hand.transform.lossyScale);
        hand.transform.SetParent(attachGo.transform, true);

        //hand.transform.localScale = new Vector3(1 / transform.root.localScale.x, 1 / transform.root.localScale.y, 1 / transform.root.localScale.z);
        Debug.Log("그랩후 hand의 local 스케일" + hand.transform.localScale);
        Debug.Log("그랩 후hand의 world 스케일" + hand.transform.lossyScale);
    }

}
