using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem.XR;

// 그랩 동작 관련 함수(그냥 잡을때, 레버 잡을때 등등)
public class CheckEvent : MonoBehaviour
{
    [SerializeField]
    private GameObject leftHandVisable;
    [SerializeField]
    private GameObject rightHandVisable;
    [SerializeField]
    private GameObject player;

    // 그랩했을때 호출됨.
    public void GrabOn(SelectEnterEventArgs args)
    {
        AudioManager.instance.PlaySfx(AudioManager.sfx.grap);

        Debug.Log("GrabOn 호출됨 : " + args.interactableObject.transform.name);
        string hand = args.interactorObject.handedness.ToString();

        CheckHand(hand).GetComponent<Rigidbody>().isKinematic = true;

        // 레버 같이 손이 해당 오브젝트에 붙어야 하는 경우
        if (args.interactableObject.transform.gameObject.tag == "NotMove")
        {
            Debug.Log(args.interactableObject.transform.gameObject.name);
            // 움직임을 따라다니는 track을 비활성화
            CheckHand(hand).GetComponent<PhysicHand>().enabled = false;

            // 해당 위치에 hand를 붙여줌.
            args.interactableObject.transform.gameObject.GetComponent<AttachHand>().hand = CheckHand(hand);

            // 자식으로 붙여줌.
            args.interactableObject.transform.gameObject.GetComponent<AttachHand>().SetChildHand();

            // 그랩 된 상태
            args.interactableObject.transform.gameObject.GetComponent<AttachHand>().grapping = true;
        }
        else
        {
            // 그랩한 손 안보이게 함.
            CheckHand(hand).SetActive(false);
        }
    }

    // 그랩 놓았을때 호출됨.
    public void GrabOff(SelectExitEventArgs args)
    {
        AudioManager.instance.PlaySfx(AudioManager.sfx.drop);

        Debug.Log("GrabOff 호출됨" + args.interactorObject.handedness.ToString());
        string hand = args.interactorObject.handedness.ToString();

        CheckHand(hand).GetComponent<Rigidbody>().isKinematic = false;

        // 레버 같이 손이 해당 오브젝트에 붙어야 하는 경우
        if (args.interactableObject.transform.gameObject.tag == "NotMove")
        {
            // 그랩 해제
            args.interactableObject.transform.gameObject.GetComponent<AttachHand>().grapping = false;

            // 움직임을 따라다니는 track을 활성화
            CheckHand(hand).GetComponent<PhysicHand>().enabled = true;

            // 해당 위치에 hand를 초기화
            args.interactableObject.transform.gameObject.GetComponent<AttachHand>().hand = null;

            // 다시 플레이어 자식으로 만들기
            CheckHand(hand).transform.SetParent(player.transform);

            // 원상태로 복귀
            CheckHand(hand).transform.localScale = Vector3.one;
        }
        else
        {
            // 그랩한 손 안보이게 함.
            CheckHand(hand).SetActive(true);
        }
    }

    // 어떤 손으로 잡았는지 확인
    private GameObject CheckHand(string _name)
    {
        if (_name == "Left")
        {
            return leftHandVisable;
        }
        else if (_name == "Right")
        {
            return rightHandVisable;
        }

        return null;
    }

    // 잡았을때, 일정거리 이상이면 자동으로 놓아짐.
}
