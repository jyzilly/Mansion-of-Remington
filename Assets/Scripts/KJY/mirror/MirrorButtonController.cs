using UnityEngine;

public class MirrorButtonController : MonoBehaviour
{
    //��ư ���� ����
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
