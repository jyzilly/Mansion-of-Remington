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

    // 이미지를 넣음
    private void OnTriggerStay(Collider other)
    {
        // 이미 슬롯안에 있다면 return
        if (alreadyIn) return;

        // itemInfo가 없는 객체면 안들어감. 
        if (other.GetComponent<ItemInfo>() == null) return;

        // 플레이어가 잡고 있는 상태라면 안들어감.
        if (other.gameObject.GetComponent<XRGrabInteractable>().isSelected == true) return;

        // 이미지 바꾸고
        gameObject.GetComponent<Image>().sprite = other.GetComponent<ItemInfo>().ItemImage;
        ItemName = other.GetComponent<ItemInfo>().ItemName;

        // 못들어가는 상태로 만듦.
        alreadyIn = true;

        // 현재 게임오브젝트 저장
        curInventoryGo = other.gameObject;

        // 해당 아이템을 비활성화
        other.gameObject.SetActive(false);
    }

    // 아이템 꺼내는 함수
    public void OutInventorySlot()
    {
        if (!alreadyIn) return;

        alreadyIn = false;
        gameObject.GetComponent<Image>().sprite = null;

        // 플레이어 위에 생성
        curInventoryGo.transform.position = playerTr.position + new Vector3(0f, 3f, 0f);
        curInventoryGo.SetActive(true);

        // 다시 null상태
        curInventoryGo = null;
    }
}

