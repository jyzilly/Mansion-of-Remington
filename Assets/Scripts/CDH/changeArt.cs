using UnityEngine;

public class changeArt : MonoBehaviour
{
    [SerializeField]
    GameObject offImage;
    [SerializeField]
    GameObject onImage;
    [SerializeField]
    GameObject offBox;
    [SerializeField]
    GameObject onBox;

    [SerializeField]
    Animator anim;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "changeImage")
        {
            onImage.SetActive(true);
            AudioManager.instance.PlaySfx(AudioManager.sfx.stab);
            AudioManager.instance.PlaySfx(AudioManager.sfx.boyscream);
            offBox.SetActive(false);
            onBox.SetActive(true);
            offImage.SetActive(false);
            anim.SetBool("isOpen", true);
        }
    }

    
}
