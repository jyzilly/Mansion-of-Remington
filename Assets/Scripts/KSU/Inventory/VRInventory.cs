using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.UI;
public class VRInventory : MonoBehaviour
{
    public Transform playerTr;

    private bool alreadyIn = false;
    private bool isHovering = false;
    private XRGrabInteractable grabInteractable;

    private string ItemName;
    private GameObject curInventoryGo;

    private void Start()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();
    }

    // �̹����� ����
    private void OnTriggerStay(Collider other)
    {
        // �̹� ���Ծȿ� �ִٸ� return
        if (alreadyIn) return;

        // itemInfo�� ���� ��ü�� �ȵ�. 
        if (other.GetComponent<ItemInfo>() == null) return;

        // �÷��̾ ��� �ִ� ���¶�� �ȵ�.
        if (other.gameObject.GetComponent<XRGrabInteractable>().isSelected == true) return;

        // �̹��� �ٲٰ�
        gameObject.GetComponent<Image>().sprite = other.GetComponent<ItemInfo>().ItemImage;
        ItemName = other.GetComponent<ItemInfo>().ItemName;

        // ������ ���·� ����.
        alreadyIn = true;

        // ���� ���ӿ�����Ʈ ����
        curInventoryGo = other.gameObject;

        // �ش� �������� ��Ȱ��ȭ
        other.gameObject.SetActive(false);
    }

    // ������ ������ �Լ�
    public void OutInventorySlot()
    {
        if (!alreadyIn) return;

        alreadyIn = false;
        gameObject.GetComponent<Image>().sprite = null;

        // �÷��̾� ���� ����
        curInventoryGo.transform.position = playerTr.position + new Vector3(0f, 3f, 0f);
        curInventoryGo.SetActive(true);

        // �ٽ� null����
        curInventoryGo = null;
    }
}

