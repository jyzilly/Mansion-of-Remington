using UnityEngine;

public class PicSoundResult : MonoBehaviour
{
    private GResponse resTrigger = null;
    [SerializeField]
    private TheObjectSound objectSound = null;
    [SerializeField]
    private int audioClipNum = -1;

    private void Awake()
    {
        resTrigger = GetComponent<GResponse>();
        objectSound = GetComponentInParent<TheObjectSound>();
    }
    private void Start()
    {
        resTrigger.OnResponseCallback += PlaySound;
    }

    private void PlaySound(bool _State)
    {
        if (_State)
        {
            objectSound.PlaySound(audioClipNum);
        }
    }
}
