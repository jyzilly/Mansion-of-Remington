using UnityEngine;

//�پ��� ���带 ����ϴ� ���� ���� �÷��̾�

public class TheObjectSound : MonoBehaviour
{
    //���ϴ� Ŭ���� �迭�� �ְ�
    [SerializeField] private AudioClip[] audioClips;

    //OnClick() �̺�Ʈ ��� ȣ�Ⱑ��
    public void PlaySound(int _audioClipNum)
    {
        //���ϴ� ������ Ŭ�� ��� 
        AudioClip soundClip = audioClips[_audioClipNum];
        GetComponent<AudioSource>().Stop();
        GetComponent<AudioSource>().PlayOneShot(soundClip, 0.8f);
    }

}
