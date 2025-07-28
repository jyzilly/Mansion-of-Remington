using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    void doorSound()
    {
        AudioManager.instance.PlaySfx(AudioManager.sfx.oldDoor);
    }
}
