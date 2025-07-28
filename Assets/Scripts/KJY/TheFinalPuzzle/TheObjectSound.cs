using UnityEngine;

//다양한 사운드를 재생하는 범용 사운드 플레이어

public class TheObjectSound : MonoBehaviour
{
    //원하는 클립들 배열에 넣고
    [SerializeField] private AudioClip[] audioClips;

    //OnClick() 이벤트 등에서 호출가능
    public void PlaySound(int _audioClipNum)
    {
        //원하는 순서의 클립 재생 
        AudioClip soundClip = audioClips[_audioClipNum];
        GetComponent<AudioSource>().Stop();
        GetComponent<AudioSource>().PlayOneShot(soundClip, 0.8f);
    }

}
