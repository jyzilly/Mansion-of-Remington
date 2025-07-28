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
            int loopIndex = (index + channelIndex) % sfxPlayers.Length;/*맨 마지막에 실행을했던 플레이어의 인덱스*/
            if (sfxPlayers[loopIndex].isPlaying)
            
                continue;

            //같은 이름의 효과음이 여러개 존재 할 시 랜덤으로 소리를 재생  시키고 싶을때
            //만약 여러개 있는게 여러개 있을때 스위치 문으로 나눠주면 된다.
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

        // channel 하나만 쓸때
        //sfxPlayers[0].clip = sfxClip[(int)sfx];
        //sfxPlayers[0].Play();
    }
}
