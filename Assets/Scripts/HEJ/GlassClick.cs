using JetBrains.Annotations;
using UnityEngine;

public class GlassClick : MonoBehaviour
{
    public delegate void OnGlassClickDelegate();

    private OnGlassClickDelegate onGlassClickCallback = null;


    public OnGlassClickDelegate OnGlassClickCallback
    {
        set { onGlassClickCallback = value; }
    }


    public void OnClickProcess()
    {
        onGlassClickCallback?.Invoke();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Hammer")
        {
            Debug.Log("À¯¸® ±úÁü È£ÃâµÊ.");
            AudioManager.instance.PlaySfx(AudioManager.sfx.glass);
            OnClickProcess();
        }
    }
}
