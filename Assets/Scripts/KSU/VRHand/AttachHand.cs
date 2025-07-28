using UnityEngine;

public class AttachHand : MonoBehaviour
{
    [SerializeField]
    [Tooltip("���� ���� ��ġ")]
    private Transform attachPos;
    [SerializeField]
    [Tooltip("���� ���� ��ü")]
    private GameObject attachGo;

    [Tooltip("��")]
    public GameObject hand = null;

    public bool grapping = false;

    private void Update()
    {
        if (grapping)
        {
            hand.transform.position = attachPos.position;
        }
    }

    // hand�� �ڽ����� ����.
    public void SetChildHand()
    {
        Debug.Log(new Vector3(1 / transform.root.localScale.x, 1 / transform.root.localScale.y, 1 / transform.root.localScale.z));
        Debug.Log("hand�� local ������" + hand.transform.localScale);
        Debug.Log("hand�� world ������" + hand.transform.lossyScale);
        hand.transform.SetParent(attachGo.transform, true);

        //hand.transform.localScale = new Vector3(1 / transform.root.localScale.x, 1 / transform.root.localScale.y, 1 / transform.root.localScale.z);
        Debug.Log("�׷��� hand�� local ������" + hand.transform.localScale);
        Debug.Log("�׷� ��hand�� world ������" + hand.transform.lossyScale);
    }

}
