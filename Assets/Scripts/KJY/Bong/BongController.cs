using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;


//XR 환경에서 특정 객체의 잡기grab상태를 감지하고 외부에 알리는 컨트롤러 -> XR Interaction Toolkit 의 XRGrabInteractable, 감지되면 true로 아니면 false

public class BongController : MonoBehaviour
{
    //결과를 알리기위해 public으로
    public bool TheBongOnTouch = false;
    private XRGrabInteractable grab;

    private void Start()
    {
        grab = GetComponent<XRGrabInteractable>();
    }

    private void Update()
    {
        //isSelected면 잡았다는 뜻이고, true로, isSelected false면 못 잡았다는 뜻
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
