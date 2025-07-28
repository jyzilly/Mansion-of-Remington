using UnityEngine;
using UnityEngine.Video;

public class TapePlayer : MonoBehaviour
{
    public Tape tape = null;
    public VideoClip[] clips;
    public VideoPlayer videoPlayer;
    public int repoterEndNum = 0;

    private void OnTapesVideoCallback(Tape tape)
    {
        Debug.Log(tape.TapeNum);

        videoPlayer.clip = clips[tape.TapeNum];
        videoPlayer.Play();
        videoPlayer.loopPointReached += OnVideoEnd;
        ++repoterEndNum;
    }

    private void OnTriggerEnter(Collider _collider)
    {
        tape = _collider.GetComponent<Tape>();
        AudioManager.instance.PlaySfx(AudioManager.sfx.tape);
        if (tape != null)
        {
            

            tape.OnVideoClickCallback = OnTapesVideoCallback;
        }
    }

    private void OnVideoEnd(VideoPlayer vd)
    {
        Destroy(tape.gameObject);
        PlayerPrefs.SetInt("REN",repoterEndNum);
    }

    
}
