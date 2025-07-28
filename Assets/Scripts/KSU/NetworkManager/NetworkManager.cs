using System.Collections;
using Photon.Pun;
using Photon.Voice.Unity;
using UnityEngine;
using UnityEngine.Video;
using static NetworkManager;

public class NetworkManager : MonoBehaviourPun
{
    [Header("플레이어 콜백 뿌리는 쪽")]
    [SerializeField]
    [Tooltip("처음 자물쇠 풀렸을때 콜백")]
    private LockInteraction openLock;
    [SerializeField]
    [Tooltip("기자가 누른 동물버튼 콜백")]
    private AnimalBoard wAnimalboard;
    [SerializeField]
    [Tooltip("거울 기믹 성공 콜백")]
    private MirrorP mirror;
    [SerializeField]
    [Tooltip("마네킹 퍼즐 성공 콜백")]
    private manneManager mane;
    [SerializeField]
    [Tooltip("유리 뿌서짐 콜백")]
    private glass glass1;


    [Header("플레이어 콜백 받는 쪽")]
    [SerializeField]
    [Tooltip("동물버튼 콜백을 받는 소년큐브2면")]
    private Cube2Sound bAnimalboard;
    [SerializeField]
    [Tooltip("소년")]
    private GameObject boy;
    [SerializeField]
    [Tooltip("기자")]
    private GameObject woman;
    [SerializeField]
    [Tooltip("포톤보이스")]
    private Recorder recorder;
    [SerializeField]
    [Tooltip("큐브 보내기")]
    private GResponse cubeResult;
    [SerializeField]
    [Tooltip("쇠사슬 보내기")]
    private GResponse ropeResult;
    [SerializeField]
    [Tooltip("책1 보내기")]
    private GResponse book1Result;
    [SerializeField]
    [Tooltip("책2 보내기")]
    private GResponse book2Result;
    [SerializeField]
    [Tooltip("퓨즈 보내기")]
    private GResponse puseResult;

    [Header("플레이어 생성 관련")]
    [SerializeField]
    [Tooltip("소년 프리팹")]
    private GameObject boyPrefab;
    [SerializeField]
    [Tooltip("기자 프리팹")]
    private GameObject womanPrefab;
    [SerializeField]
    [Tooltip("소년 생성 위치")]
    private Vector3 boyTr;
    [SerializeField]
    [Tooltip("기자 생성 위치")]
    private Vector3 womanTr;


    [Header("사진으로 보낼것들(소년쪽 오브젝트)")]
    [SerializeField]
    [Tooltip("큐브")]
    private GameObject cube;
    [SerializeField]
    [Tooltip("쇠사슬")]
    private GameObject chain;
    [SerializeField]
    [Tooltip("훅 활성화")]
    private HookAttach hook;
    [SerializeField]
    [Tooltip("책 2권중 첫번째책(book 착시퍼즐)")]
    private GameObject book1;
    [SerializeField]
    [Tooltip("책 2권중 두번째책(book 퍼즐)")]
    private GameObject book2;
    [SerializeField]
    [Tooltip("퓨즈")]
    private GameObject fuse;

    [Header("기자 쪽 생기는것들")]
    [SerializeField]
    [Tooltip("기자쪽 쇠사슬")]
    private GameObject wChain;
    [SerializeField]
    [Tooltip("기자쪽 망치")]
    private GameObject wHammer;
    [SerializeField]
    [Tooltip("기자쪽 마네킹후 책")]
    private GameObject womanTBook;


    [Header("책 4권 생성 관련")]
    [SerializeField]
    [Tooltip("소년방 책1")]
    private GameObject boyBook1;
    [SerializeField]
    [Tooltip("소년방 책2")]
    private GameObject boyBook2;
    [SerializeField]
    [Tooltip("소년방 책3")]
    private GameObject boyBook3;
    [SerializeField]
    [Tooltip("소년방 책4")]
    private GameObject boyBook4;
    [SerializeField]
    [Tooltip("소년방 책5")]
    private GameObject boyBook5;
    [SerializeField]
    [Tooltip("소년 힌트 1")]
    private GameObject boyHint1;
    [SerializeField]
    [Tooltip("기자 책1")]
    private GameObject womanBook1;
    [SerializeField]
    [Tooltip("기자 힌트 2")]
    private GameObject womanHint2;
    [SerializeField]
    [Tooltip("소년방 실루엣액자 collider")]
    private Collider[] planes = new Collider[4];


    [Header("키보드 동시에 누르기")]
    [SerializeField]
    [Tooltip("기자 키보드1 상태")]
    private KeyboardRPC wKeyBoard1;
    [SerializeField]
    [Tooltip("기자 키보드2 상태")]
    private KeyboardRPC wKeyBoard2;
    [SerializeField]
    [Tooltip("소년 키보드1 상태")]
    private KeyboardRPC bKeyBoard1;
    [SerializeField]
    [Tooltip("소년 키보드2 상태")]
    private KeyboardRPC bKeyBoard2;

    [Header("유리 뿌셨을때 소년지하실에 생기는것들")]
    [SerializeField]
    [Tooltip("생성되는 오브젝트들")]
    private GameObject[] somethings;
    [SerializeField]
    [Tooltip("기자쪽 생성(마지막 퍼즐)")]
    private GameObject keypad;
    [SerializeField]
    [Tooltip("소년방 들어갈수 있는 boxcollder")]
    private BoxCollider wBoyRoom;
    [SerializeField]
    [Tooltip("소년방 잠긴 왼쪽문")]
    private ChangeDoorY Left;
    [SerializeField]
    [Tooltip("소년방 잠긴 오른쪽문")]
    private ChangeDoorY Right;

    [Header("플레이어 오프닝 및 엔딩 카메라")]
    [SerializeField]
    [Tooltip("소년 카메라")]
    private GameObject boyCam;
    [SerializeField]
    [Tooltip("기자 카메라")]
    private GameObject womanCam;

    [Header("비디오 클립들")]
    [SerializeField]
    [Tooltip("소년쪽 영상 재생 VideoPlayer")]
    private VideoPlayer boyVideoPlayer;
    [SerializeField]
    [Tooltip("기자쪽 영상 재생 VideoPlayer")]
    private VideoPlayer womanVideoPlayer;
    [SerializeField]
    [Tooltip("소년 오프닝 클립")]
    private VideoClip boyVideo1;
    [SerializeField]
    [Tooltip("소년 해피엔딩 클립")]
    private VideoClip boyVideo2;
    [SerializeField]
    [Tooltip("소년 배드엔딩 클립")]
    private VideoClip boyVideo3;
    [SerializeField]
    [Tooltip("기자 오프닝 클립")]
    private VideoClip womanVideo1;
    [SerializeField]
    [Tooltip("기자 테이프 1개이하 클립")]
    private VideoClip womanVideo2;
    [SerializeField]
    [Tooltip("기자 테이프 2개 클립")]
    private VideoClip womanVideo3;
    [SerializeField]
    [Tooltip("기자 테이프 3개 클립")]
    private VideoClip womanVideo4;
    [SerializeField]
    [Tooltip("테이프 클립 갯수 For 기자엔딩")]
    private TapePlayer tapePlayer;

    [Header("소년 엔딩 bool값")]
    public bool boyBadEnding = false;
    public bool boyHappyEnding = false;

    // 엔딩 될때 networkManager의 딜리게이트를 호출하면됨.
    public delegate void EndingDelegate();
    public EndingDelegate endingDelegate;


    private bool keyboardSucess = false;
    private bool checkIsMine = false;
    private bool recorderOn = true;
    private int enterPlayerNum;
    private int endOpeningVideoNum;
    private int playerSpwanCnt;

    private void Awake()
    {
        enterPlayerNum = 0;
        endOpeningVideoNum = 0;
        playerSpwanCnt = 0;
    }

    private void Start()
    {
        // 콜백 함수 등록
        if (openLock != null)
        {
            openLock.LockOpenCallback += BoyMove;
        }

        if (wAnimalboard != null)
        {
            wAnimalboard.animalBtnCallback += WCallbackAnimal;
        }

        if (glass1 != null)
        {
            glass1.glassSucessCallback += GlassSucess;
        }

        if (mirror != null)
        {
            mirror.mirroSucessCallback += MirrorSucess;
        }

        if (mane != null)
        {
            mane.manneSucessCallback += ManeSucess;
        }

        cubeResult.OnResponseCallback += CubeTransport;
        ropeResult.OnResponseCallback += RopeTransport;
        book1Result.OnResponseCallback += Book1Transport;
        book2Result.OnResponseCallback += Book2Transport;
        puseResult.OnResponseCallback += FuseTransport;

        // 플레이어 비디오 끝났을때 호출
        boyVideoPlayer.loopPointReached += OnVideoEnd;
        womanVideoPlayer.loopPointReached += OnVideoEnd;

        endingDelegate += EndingCallback;


        // 둘다 들어왔는지 check
        CheckEnterScene();

        // 코루틴 실행(둘다 들어오면 오프닝씬 재생되도록)
        StartCoroutine(EnterTwoPlayer());
    }

    private void Update()
    {
        // 키보드 4개다 눌려졌을때 성공!
        if (wKeyBoard1 != null && wKeyBoard2 != null && bKeyBoard1 != null && bKeyBoard2 != null)
        {
            if (!keyboardSucess && wKeyBoard1.TheButtonisPressed && wKeyBoard2.TheButtonisPressed && bKeyBoard1.TheButtonisPressed && bKeyBoard2.TheButtonisPressed)
            {
                keyboardSucess = true;
                KeyboardSucess();
            }
        }

        // boy와 woman이 둘다 네트워크상에서 생성됬을때
        if (boy != null && woman != null && !checkIsMine)
        {
            if (boy.GetComponent<PhotonView>().IsMine)
            {
                woman.transform.GetChild(0).gameObject.SetActive(false);
            }
            else if (woman.GetComponent<PhotonView>().IsMine)
            {
                boy.transform.GetChild(0).gameObject.SetActive(false);
            }

            checkIsMine = true;
        }
    }

    #region 콜백 받는 쪽에서 실행되는 함수들
    private void BoyMove()
    {
        Debug.Log("boymove 해제 호출");

        // 소년이 움직일수 있도록 설정
        photonView.RPC("BoyMoveRPC", RpcTarget.Others);
    }

    private void WCallbackAnimal(string _animal)
    {
        // 소년에게 기자가 누른 버튼의 정보를 넘김.
        if (_animal == "Monkey")
        {
            bAnimalboard.CallbackMonkey();
        }

        if (_animal == "Mouse")
        {
            bAnimalboard.CallbackMouse();
        }
    }

    private void MirrorSucess()
    {
        photonView.RPC("MirrorSucessRPC", RpcTarget.Others);
    }

    private void ManeSucess()
    {
        photonView.RPC("ManeSucessRPC", RpcTarget.All);
    }

    private void GlassSucess()
    {
        photonView.RPC("GlassSucessRPC", RpcTarget.AllBuffered);
    }

    private void KeyboardSucess()
    {
        photonView.RPC("KeyboardSucessRPC", RpcTarget.All);
    }

    private void CubeTransport(bool _state)
    {
        photonView.RPC("CubeTransportRPC", RpcTarget.All);
    }

    private void RopeTransport(bool _state)
    {
        photonView.RPC("RopeTransportRPC", RpcTarget.Others);
    }

    private void Book1Transport(bool _state)
    {
        photonView.RPC("Book1TransportRPC", RpcTarget.Others);
    }

    private void Book2Transport(bool _state)
    {
        photonView.RPC("Book2TransportRPC", RpcTarget.Others);
    }

    private void FuseTransport(bool _state)
    {
        photonView.RPC("FuseTransportRPC", RpcTarget.Others);
    }
    #endregion

    #region PunRPC 함수
    [PunRPC]
    private void SetBoy(int _viewID)
    {
        boy = PhotonNetwork.GetPhotonView(_viewID).gameObject;
    }

    [PunRPC]
    private void SetWoman(int _viewID)
    {
        woman = PhotonNetwork.GetPhotonView(_viewID).gameObject;
    }

    [PunRPC]
    private void BoyMoveRPC()
    {
        Debug.LogError("Boy의 움직임 해제 rpc 호출됨.");

        Debug.LogError(boy.transform.GetChild(0).GetChild(0) + " : Locomotion이여야함");
        // boy가 움직일수 있게
        boy.transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
    }

    [PunRPC]
    private void MirrorSucessRPC()
    {
        // 쇠사슬 생성
        if (wChain != null)
        {
            wChain.SetActive(true);
            AudioManager.instance.PlaySfx(AudioManager.sfx.dropchain);

        }
    }

    [PunRPC]
    private void ManeSucessRPC()
    {
        // 아직 할당 안했으면 실행안됨.
        // if (boyBook1 == null || boyBook2 == null || boyBook3 == null || boyBook4 == null || boyBook5 == null || womanHint1 == null || womanHint2 == null) return;

        if (PhotonNetwork.LocalPlayer.CustomProperties["Role"].ToString() == "Boy")
        {
            // 소년일때 -> 책5권 활성화 + 힌트 1개 활성화
            boyBook1.SetActive(true);
            boyBook1.transform.parent = null;
            boyBook2.SetActive(true);
            boyBook3.SetActive(true);
            boyBook4.SetActive(true);
            boyBook5.SetActive(true);
            // boyHint1.SetActive(true);

            // 액자 collider 비활성화
            foreach(Collider col in planes)
            {
                col.enabled = false;
            }
        }
        else if (PhotonNetwork.LocalPlayer.CustomProperties["Role"].ToString() == "Woman")
        {
            // 기자일때 -> 힌트 2개를 기자 위치에
            womanTBook.SetActive(true);
            // womanHint2.SetActive(true);
        }
    }

    [PunRPC]
    private void GlassSucessRPC()
    {
        // 소년 지하실에 생성해야 하는것들
        if (PhotonNetwork.LocalPlayer.CustomProperties["Role"].ToString() == "Boy")
        {
            foreach(GameObject go in somethings)
            {
                go.SetActive(true);
            }

            Left.enabled = true;
            Right.enabled = true;
        } 
        else if (PhotonNetwork.LocalPlayer.CustomProperties["Role"].ToString() == "Woman")
        {
            keypad.SetActive(true);
            wBoyRoom.enabled = false;
        }



        // 서로 보이스 끊김
        //if (recorderOn)
        //{
        //    recorder.RecordingEnabled = false;
        //    recorderOn = false;
        //}
        //else
        //{
        //    recorder.RecordingEnabled = true;
        //    recorderOn = true;
        //}

    }

    [PunRPC]
    private void KeyboardSucessRPC()
    {
        if (PhotonNetwork.LocalPlayer.CustomProperties["Role"].ToString() == "Boy")
        {
            // 소년일때

        }
        else if (PhotonNetwork.LocalPlayer.CustomProperties["Role"].ToString() == "Woman")
        {
            // 기자일때
            wHammer.SetActive(true);
        }
    }

    [PunRPC]
    private void CubeTransportRPC()
    {
        if (cube != null) cube.SetActive(true);
    }

    [PunRPC]
    private void RopeTransportRPC()
    {
        if (chain != null) chain.SetActive(true);
        // 훅 활성화
        if (hook != null) hook.activeTrigger = true;
    }

    [PunRPC]
    private void Book1TransportRPC()
    {
        if (book1 != null) book1.SetActive(true);
    }

    [PunRPC]
    private void Book2TransportRPC()
    {
        if (book1 != null) book2.SetActive(true);
    }

    [PunRPC]
    private void FuseTransportRPC()
    {
        if (fuse != null) fuse.SetActive(true);
    }

    [PunRPC]
    private void CheckEnterSceneRPC()
    {
        enterPlayerNum++;
    }

    [PunRPC]
    private void EndOpeningVideoRPC()
    {
        endOpeningVideoNum++;
    }
    #endregion

    // 플레이어를 소환하는 함수
    private void InstantiatePlayer()
    {
        if (PhotonNetwork.LocalPlayer.CustomProperties.ContainsKey("Role") && PhotonNetwork.LocalPlayer.CustomProperties["Role"].ToString() == "Boy")
        {
            Debug.Log("소년 생성");

            // boy 생성
            boy = PhotonNetwork.Instantiate(boyPrefab.name, boyTr, Quaternion.Euler(0f, 180f, 0f));

            // ismine 키기
            boy.transform.GetChild(0).gameObject.SetActive(true);

            // boy 못움직이게 locomotion 비활성화
            boy.transform.GetChild(0).GetChild(0).gameObject.SetActive(false);

            // boy 설정
            photonView.RPC("SetBoy", RpcTarget.AllBuffered, boy.GetComponent<PhotonView>().ViewID);

            playerSpwanCnt++;
        }
        else if(PhotonNetwork.LocalPlayer.CustomProperties.ContainsKey("Role") && PhotonNetwork.LocalPlayer.CustomProperties["Role"].ToString() == "Woman")
        {
            Debug.Log("기자 생성");

            // 기자 생성
            woman = PhotonNetwork.Instantiate(womanPrefab.name, womanTr, Quaternion.identity);

            // ismine 키기
            woman.transform.GetChild(0).gameObject.SetActive(true);

            // woman 설정
            photonView.RPC("SetWoman", RpcTarget.AllBuffered, woman.GetComponent<PhotonView>().ViewID);

            playerSpwanCnt++;
        }
    }

    // 바로 생성하면 역할군 설정하는 시간때문에 오류가 나서 매프레임 들어왔는지 확인후에 생성
    private IEnumerator EnterTwoPlayer()
    {
        while (true)
        {
            if (PhotonNetwork.LocalPlayer.CustomProperties.ContainsKey("Role") == true)
            {
                SetOpeningCam();
                break;
            }

            yield return null;
        }
    }

    // 해당 씬에 플레이어가 2명 왔는지 확인하는 함수
    private void CheckEnterScene()
    {
        photonView.RPC("CheckEnterSceneRPC", RpcTarget.AllBuffered);
    }

    // 역할에 따른 오프닝 캠을 On
    private void SetOpeningCam()
    {
        if (PhotonNetwork.LocalPlayer.CustomProperties["Role"].ToString() == "Boy")
        {
            // 소년쪽 카메라를 On 시킴
            boyCam.SetActive(true);
        }
        else if (PhotonNetwork.LocalPlayer.CustomProperties["Role"].ToString() == "Woman")
        {
            // 기자쪽 카메라를 On 시킴
            womanCam.SetActive(true);
        }

        StartCoroutine(StartOpeningVideoCoroutine());
    }

    // 2명다 들어왔을때 비디오를 실행시키는 함수
    private IEnumerator StartOpeningVideoCoroutine()
    {
        while (true)
        {
            if (enterPlayerNum == 2)
            {
                yield return new WaitForSeconds(1f);

                StartOpeningVideo();

                break;
            }

            yield return null;
        }
    }

    // 비디오 시작하는 함수
    private void StartOpeningVideo()
    {
        if (PhotonNetwork.LocalPlayer.CustomProperties["Role"].ToString() == "Boy")
        {
            // 소년쪽 비디오 재생
            boyVideoPlayer.clip = boyVideo1;
            boyVideoPlayer.Play();
        }
        else if (PhotonNetwork.LocalPlayer.CustomProperties["Role"].ToString() == "Woman")
        {
            // 기자쪽 비디오 재생
            womanVideoPlayer.clip = womanVideo1;
            womanVideoPlayer.Play();
        }
    }

    // 비디오가 끝났을때 호출되는 콜백함수
    private void OnVideoEnd(VideoPlayer vd)
    {
        if (PhotonNetwork.LocalPlayer.CustomProperties["Role"].ToString() == "Boy")
        {
            // 오프닝 영상이라면
            if (boyVideoPlayer.clip == boyVideo1)
            {
                // 오프닝 영상이 끝났다고 서버에 콜백
                photonView.RPC("EndOpeningVideoRPC", RpcTarget.AllBuffered);
            }

            // 소년쪽 비디오 재생멈춤
            boyVideoPlayer.Stop();
        }
        else if (PhotonNetwork.LocalPlayer.CustomProperties["Role"].ToString() == "Woman")
        {
            if (womanVideoPlayer.clip == womanVideo1)
            {
                // 오프닝 영상이 끝났다고 서버에 콜백
                photonView.RPC("EndOpeningVideoRPC", RpcTarget.AllBuffered);
            }

            // 기자쪽 비디오 재생멈춤
            womanVideoPlayer.Stop();
        }

        StartCoroutine(EndVideoCoroutine());
    }

    // 비디오가 둘다 끝났을때를 실행되는 함수
    private IEnumerator EndVideoCoroutine()
    {
        while (true)
        {
            if (endOpeningVideoNum == 2)
            {
                // 켜놨던 캠을 끄고
                if (PhotonNetwork.LocalPlayer.CustomProperties["Role"].ToString() == "Boy")
                {
                    boyCam.SetActive(false);
                }
                else if (PhotonNetwork.LocalPlayer.CustomProperties["Role"].ToString() == "Woman")
                {
                    womanCam.SetActive(false);
                }

                // 플레이어를 생성
                if (playerSpwanCnt == 0)
                {
                    InstantiatePlayer();
                }

                break;
            }

            yield return null;
        }
    }

    private void EndingCallback()
    {
        if (PhotonNetwork.LocalPlayer.CustomProperties["Role"].ToString() == "Boy")
        {
            boy.SetActive(false);
            boyCam.SetActive(true); 

            // 소년 해피엔딩 세팅
            if (boyBadEnding)
            {
                boyVideoPlayer.clip = boyVideo3;
                boyVideoPlayer.Play();
            }  // 소년 배드엔딩 세팅
            else if (boyHappyEnding)
            {
                boyVideoPlayer.clip = boyVideo2;
                boyVideoPlayer.Play();
            }
        }
        else if (PhotonNetwork.LocalPlayer.CustomProperties["Role"].ToString() == "Woman")
        {
            woman.SetActive(false);
            womanCam.SetActive(true);

            if (tapePlayer.repoterEndNum <= 1)
            {
                womanVideoPlayer.clip = womanVideo2;
                womanVideoPlayer.Play();
            }
            else if (tapePlayer.repoterEndNum == 2)
            {
                womanVideoPlayer.clip = womanVideo3;
                womanVideoPlayer.Play();
            }
            else if (tapePlayer.repoterEndNum == 3)
            {
                womanVideoPlayer.clip = womanVideo4;
                womanVideoPlayer.Play();
            }
        }
    }
}
