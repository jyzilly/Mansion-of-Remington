using UnityEngine;

public class MirroInsideButtonController : MonoBehaviour
{

    public bool TheButtonisPressed = false;

    public void SetButton()
    {
        TheButtonisPressed = true;
    }

    public void SetButtonExit()
    {
        TheButtonisPressed = false;
    }


}
