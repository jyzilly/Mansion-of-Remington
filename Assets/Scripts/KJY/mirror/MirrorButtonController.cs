using UnityEngine;

public class MirrorButtonController : MonoBehaviour
{
    //버튼 상태 셋팅
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
