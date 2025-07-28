using UnityEngine;
using UnityEngine.Video;

public class EndingScene : MonoBehaviour
{

    private VideoPlayer videoPlayer;
    public VideoClip[] clips;
    private int num;

    /*
        클립 1 - 오프닝씬
        클립 2 - 소년 베드 엔딩
        클립 3 - 소년 진엔딩
     
     
    */

    private void Start()
    {
        videoPlayer = GetComponent<VideoPlayer>();
    }

    private void Update()
    {

        if (Input.GetKeyUp(KeyCode.Q))
        {
            num = 1;
        }
        if (Input.GetKeyUp(KeyCode.W))
        {
            num = 2;
        }
        if (Input.GetKeyUp(KeyCode.E))
        {
            num = 3;
        }

        if (num == 1)
        {
            videoPlayer.clip = clips[0];
            videoPlayer.Play();
            videoPlayer.loopPointReached += OnVideoEnd;
        } 
        if(num == 2)
        {
            videoPlayer.clip = clips[1];
            videoPlayer.Play();
            videoPlayer.loopPointReached += OnVideoEnd;
        }
        if (num == 3)
        {
            videoPlayer.clip = clips[2];
            videoPlayer.Play();
            videoPlayer.loopPointReached += OnVideoEnd;
        }
    }

    public void test1()
    {
        num = 1;
    }
    public void test2()
    {
        num = 2;
    }
    public void test3()
    {
        num = 3;
    }

    private void OnVideoEnd(VideoPlayer vd)
    {
        videoPlayer.Stop();
    }

}
