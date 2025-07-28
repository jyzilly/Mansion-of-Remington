using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem.XR;

// �׷� ���� ���� �Լ�(�׳� ������, ���� ������ ���)
public class CheckEvent : MonoBehaviour
{
    [SerializeField]
    private GameObject leftHandVisable;
    [SerializeField]
    private GameObject rightHandVisable;
    [SerializeField]
    private GameObject player;

    // �׷������� ȣ���.
    public void GrabOn(SelectEnterEventArgs args)
    {
        AudioManager.instance.PlaySfx(AudioManager.sfx.grap);

        Debug.Log("GrabOn ȣ��� : " + args.interactableObject.transform.name);
        string hand = args.interactorObject.handedness.ToString();

        CheckHand(hand).GetComponent<Rigidbody>().isKinematic = true;

        // ���� ���� ���� �ش� ������Ʈ�� �پ�� �ϴ� ���
        if (args.interactableObject.transform.gameObject.tag == "NotMove")
        {
            Debug.Log(args.interactableObject.transform.gameObject.name);
            // �������� ����ٴϴ� track�� ��Ȱ��ȭ
            CheckHand(hand).GetComponent<PhysicHand>().enabled = false;

            // �ش� ��ġ�� hand�� �ٿ���.
            args.interactableObject.transform.gameObject.GetComponent<AttachHand>().hand = CheckHand(hand);

            // �ڽ����� �ٿ���.
            args.interactableObject.transform.gameObject.GetComponent<AttachHand>().SetChildHand();

            // �׷� �� ����
            args.interactableObject.transform.gameObject.GetComponent<AttachHand>().grapping = true;
        }
        else
        {
            // �׷��� �� �Ⱥ��̰� ��.
            CheckHand(hand).SetActive(false);
        }
    }

    // �׷� �������� ȣ���.
    public void GrabOff(SelectExitEventArgs args)
    {
        AudioManager.instance.PlaySfx(AudioManager.sfx.drop);

        Debug.Log("GrabOff ȣ���" + args.interactorObject.handedness.ToString());
        string hand = args.interactorObject.handedness.ToString();

        CheckHand(hand).GetComponent<Rigidbody>().isKinematic = false;

        // ���� ���� ���� �ش� ������Ʈ�� �پ�� �ϴ� ���
        if (args.interactableObject.transform.gameObject.tag == "NotMove")
        {
            // �׷� ����
            args.interactableObject.transform.gameObject.GetComponent<AttachHand>().grapping = false;

            // �������� ����ٴϴ� track�� Ȱ��ȭ
            CheckHand(hand).GetComponent<PhysicHand>().enabled = true;

            // �ش� ��ġ�� hand�� �ʱ�ȭ
            args.interactableObject.transform.gameObject.GetComponent<AttachHand>().hand = null;

            // �ٽ� �÷��̾� �ڽ����� �����
            CheckHand(hand).transform.SetParent(player.transform);

            // �����·� ����
            CheckHand(hand).transform.localScale = Vector3.one;
        }
        else
        {
            // �׷��� �� �Ⱥ��̰� ��.
            CheckHand(hand).SetActive(true);
        }
    }

    // � ������ ��Ҵ��� Ȯ��
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

    // �������, �����Ÿ� �̻��̸� �ڵ����� ������.
}
