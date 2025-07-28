using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;


//XR ȯ�濡�� Ư�� ��ü�� ���grab���¸� �����ϰ� �ܺο� �˸��� ��Ʈ�ѷ� -> XR Interaction Toolkit �� XRGrabInteractable, �����Ǹ� true�� �ƴϸ� false

public class BongController : MonoBehaviour
{
    //����� �˸������� public����
    public bool TheBongOnTouch = false;
    private XRGrabInteractable grab;

    private void Start()
    {
        grab = GetComponent<XRGrabInteractable>();
    }

    private void Update()
    {
        //isSelected�� ��Ҵٴ� ���̰�, true��, isSelected false�� �� ��Ҵٴ� ��
        if (grab.isSelected == true)
        {
            TheBongOnTouch = true;
        }
        else
        {
            TheBongOnTouch = false;
        }
    }
}
