using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [Header("BGM")]
    public AudioClip bgmClip;
    public float bgmVolume;
    AudioSource bgmPlayer;

    [Header("SFX")]
    public AudioClip[] sfxClip;
    public float sfxVolume;
    public int channels;
    AudioSource[] sfxPlayers;
    int channelIndex;

    public enum sfx { boyscream, chain, door, glass, grap, inventory, keyboard, mannequin, picture, sendpicture, puzzle, succes, Noise, stab, oldDoor, chess, tape, drop, nareO, dropchain}


    void Awake()
    {
        instance = this;
        Init();
    }

    void Init()
    {
        //
        GameObject bgmObject = new GameObject("BgmPlayer");
        bgmObject.transform.parent = transform;
        bgmPlayer = bgmObject.AddComponent<AudioSource>();
        bgmPlayer.playOnAwake = false;
        bgmPlayer.loop = true;
        bgmPlayer.volume = bgmVolume;
        bgmPlayer.clip = bgmClip;


        //
        GameObject sfxObject = new GameObject("sfxPlayer");
        sfxObject.transform.parent = transform;
        sfxPlayers = new AudioSource[channels];
        for(int index = 0; index < sfxPlayers.Length; ++index)
        {
            sfxPlayers[index] = sfxObject.AddComponent<AudioSource>();
            sfxPlayers[index].playOnAwake = false;
            sfxPlayers[index].volume = sfxVolume;
            sfxPlayers[index].loop = false;
        }

    }

    public void PlaySfx(sfx sfx)
    {
        for (int index = 0; index < sfxPlayers.Length; ++index)
        {
            int loopIndex = (index + channelIndex) % sfxPlayers.Length;/*�� �������� �������ߴ� �÷��̾��� �ε���*/
            if (sfxPlayers[loopIndex].isPlaying)
            
                continue;

            //���� �̸��� ȿ������ ������ ���� �� �� �������� �Ҹ��� ���  ��Ű�� ������
            //���� ������ �ִ°� ������ ������ ����ġ ������ �����ָ� �ȴ�.
            //int ranIndex = 0;
            //if(sfx == sfx.Hit || sfx == sfx.Melee)
            //{
            //    ranIndex = Random.Range(0, 2);
            //}

            channelIndex = loopIndex;
            sfxPlayers[loopIndex].clip = sfxClip[(int)sfx];
            sfxPlayers[loopIndex].Play();
            break;
        }

        // channel �ϳ��� ����
        //sfxPlayers[0].clip = sfxClip[(int)sfx];
        //sfxPlayers[0].Play();
    }
}
