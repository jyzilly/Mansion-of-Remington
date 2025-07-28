using System.Collections;
using Photon.Pun;
using Photon.Voice.Unity;
using UnityEngine;
using UnityEngine.Video;
using static NetworkManager;

public class NetworkManager : MonoBehaviourPun
{
    [Header("�÷��̾� �ݹ� �Ѹ��� ��")]
    [SerializeField]
    [Tooltip("ó�� �ڹ��� Ǯ������ �ݹ�")]
    private LockInteraction openLock;
    [SerializeField]
    [Tooltip("���ڰ� ���� ������ư �ݹ�")]
    private AnimalBoard wAnimalboard;
    [SerializeField]
    [Tooltip("�ſ� ��� ���� �ݹ�")]
    private MirrorP mirror;
    [SerializeField]
    [Tooltip("����ŷ ���� ���� �ݹ�")]
    private manneManager mane;
    [SerializeField]
    [Tooltip("���� �Ѽ��� �ݹ�")]
    private glass glass1;


    [Header("�÷��̾� �ݹ� �޴� ��")]
    [SerializeField]
    [Tooltip("������ư �ݹ��� �޴� �ҳ�ť��2��")]
    private Cube2Sound bAnimalboard;
    [SerializeField]
    [Tooltip("�ҳ�")]
    private GameObject boy;
    [SerializeField]
    [Tooltip("����")]
    private GameObject woman;
    [SerializeField]
    [Tooltip("���溸�̽�")]
    private Recorder recorder;
    [SerializeField]
    [Tooltip("ť�� ������")]
    private GResponse cubeResult;
    [SerializeField]
    [Tooltip("��罽 ������")]
    private GResponse ropeResult;
    [SerializeField]
    [Tooltip("å1 ������")]
    private GResponse book1Result;
    [SerializeField]
    [Tooltip("å2 ������")]
    private GResponse book2Result;
    [SerializeField]
    [Tooltip("ǻ�� ������")]
    private GResponse puseResult;

    [Header("�÷��̾� ���� ����")]
    [SerializeField]
    [Tooltip("�ҳ� ������")]
    private GameObject boyPrefab;
    [SerializeField]
    [Tooltip("���� ������")]
    private GameObject womanPrefab;
    [SerializeField]
    [Tooltip("�ҳ� ���� ��ġ")]
    private Vector3 boyTr;
    [SerializeField]
    [Tooltip("���� ���� ��ġ")]
    private Vector3 womanTr;


    [Header("�������� �����͵�(�ҳ��� ������Ʈ)")]
    [SerializeField]
    [Tooltip("ť��")]
    private GameObject cube;
    [SerializeField]
    [Tooltip("��罽")]
    private GameObject chain;
    [SerializeField]
    [Tooltip("�� Ȱ��ȭ")]
    private HookAttach hook;
    [SerializeField]
    [Tooltip("å 2���� ù��°å(book ��������)")]
    private GameObject book1;
    [SerializeField]
    [Tooltip("å 2���� �ι�°å(book ����)")]
    private GameObject book2;
    [SerializeField]
    [Tooltip("ǻ��")]
    private GameObject fuse;

    [Header("���� �� ����°͵�")]
    [SerializeField]
    [Tooltip("������ ��罽")]
    private GameObject wChain;
    [SerializeField]
    [Tooltip("������ ��ġ")]
    private GameObject wHammer;
    [SerializeField]
    [Tooltip("������ ����ŷ�� å")]
    private GameObject womanTBook;


    [Header("å 4�� ���� ����")]
    [SerializeField]
    [Tooltip("�ҳ�� å1")]
    private GameObject boyBook1;
    [SerializeField]
    [Tooltip("�ҳ�� å2")]
    private GameObject boyBook2;
    [SerializeField]
    [Tooltip("�ҳ�� å3")]
    private GameObject boyBook3;
    [SerializeField]
    [Tooltip("�ҳ�� å4")]
    private GameObject boyBook4;
    [SerializeField]
    [Tooltip("�ҳ�� å5")]
    private GameObject boyBook5;
    [SerializeField]
    [Tooltip("�ҳ� ��Ʈ 1")]
    private GameObject boyHint1;
    [SerializeField]
    [Tooltip("���� å1")]
    private GameObject womanBook1;
    [SerializeField]
    [Tooltip("���� ��Ʈ 2")]
    private GameObject womanHint2;
    [SerializeField]
    [Tooltip("�ҳ�� �Ƿ翧���� collider")]
    private Collider[] planes = new Collider[4];


    [Header("Ű���� ���ÿ� ������")]
    [SerializeField]
    [Tooltip("���� Ű����1 ����")]
    private KeyboardRPC wKeyBoard1;
    [SerializeField]
    [Tooltip("���� Ű����2 ����")]
    private KeyboardRPC wKeyBoard2;
    [SerializeField]
    [Tooltip("�ҳ� Ű����1 ����")]
    private KeyboardRPC bKeyBoard1;
    [SerializeField]
    [Tooltip("�ҳ� Ű����2 ����")]
    private KeyboardRPC bKeyBoard2;

    [Header("���� �Ѽ����� �ҳ����Ͻǿ� ����°͵�")]
    [SerializeField]
    [Tooltip("�����Ǵ� ������Ʈ��")]
    private GameObject[] somethings;
    [SerializeField]
    [Tooltip("������ ����(������ ����)")]
    private GameObject keypad;
    [SerializeField]
    [Tooltip("�ҳ�� ���� �ִ� boxcollder")]
    private BoxCollider wBoyRoom;
    [SerializeField]
    [Tooltip("�ҳ�� ��� ���ʹ�")]
    private ChangeDoorY Left;
    [SerializeField]
    [Tooltip("�ҳ�� ��� �����ʹ�")]
    private ChangeDoorY Right;

    [Header("�÷��̾� ������ �� ���� ī�޶�")]
    [SerializeField]
    [Tooltip("�ҳ� ī�޶�")]
    private GameObject boyCam;
    [SerializeField]
    [Tooltip("���� ī�޶�")]
    private GameObject womanCam;

    [Header("���� Ŭ����")]
    [SerializeField]
    [Tooltip("�ҳ��� ���� ��� VideoPlayer")]
    private VideoPlayer boyVideoPlayer;
    [SerializeField]
    [Tooltip("������ ���� ��� VideoPlayer")]
    private VideoPlayer womanVideoPlayer;
    [SerializeField]
    [Tooltip("�ҳ� ������ Ŭ��")]
    private VideoClip boyVideo1;
    [SerializeField]
    [Tooltip("�ҳ� ���ǿ��� Ŭ��")]
    private VideoClip boyVideo2;
    [SerializeField]
    [Tooltip("�ҳ� ��忣�� Ŭ��")]
    private VideoClip boyVideo3;
    [SerializeField]
    [Tooltip("���� ������ Ŭ��")]
    private VideoClip womanVideo1;
    [SerializeField]
    [Tooltip("���� ������ 1������ Ŭ��")]
    private VideoClip womanVideo2;
    [SerializeField]
    [Tooltip("���� ������ 2�� Ŭ��")]
    private VideoClip womanVideo3;
    [SerializeField]
    [Tooltip("���� ������ 3�� Ŭ��")]
    private VideoClip womanVideo4;
    [SerializeField]
    [Tooltip("������ Ŭ�� ���� For ���ڿ���")]
    private TapePlayer tapePlayer;

    [Header("�ҳ� ���� bool��")]
    public bool boyBadEnding = false;
    public bool boyHappyEnding = false;

    // ���� �ɶ� networkManager�� ��������Ʈ�� ȣ���ϸ��.
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
        // �ݹ� �Լ� ���
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

        // �÷��̾� ���� �������� ȣ��
        boyVideoPlayer.loopPointReached += OnVideoEnd;
        womanVideoPlayer.loopPointReached += OnVideoEnd;

        endingDelegate += EndingCallback;


        // �Ѵ� ���Դ��� check
        CheckEnterScene();

        // �ڷ�ƾ ����(�Ѵ� ������ �����׾� ����ǵ���)
        StartCoroutine(EnterTwoPlayer());
    }

    private void Update()
    {
        // Ű���� 4���� ���������� ����!
        if (wKeyBoard1 != null && wKeyBoard2 != null && bKeyBoard1 != null && bKeyBoard2 != null)
        {
            if (!keyboardSucess && wKeyBoard1.TheButtonisPressed && wKeyBoard2.TheButtonisPressed && bKeyBoard1.TheButtonisPressed && bKeyBoard2.TheButtonisPressed)
            {
                keyboardSucess = true;
                KeyboardSucess();
            }
        }

        // boy�� woman�� �Ѵ� ��Ʈ��ũ�󿡼� ����������
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

    #region �ݹ� �޴� �ʿ��� ����Ǵ� �Լ���
    private void BoyMove()
    {
        Debug.Log("boymove ���� ȣ��");

        // �ҳ��� �����ϼ� �ֵ��� ����
        photonView.RPC("BoyMoveRPC", RpcTarget.Others);
    }

    private void WCallbackAnimal(string _animal)
    {
        // �ҳ⿡�� ���ڰ� ���� ��ư�� ������ �ѱ�.
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

    #region PunRPC �Լ�
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
        Debug.LogError("Boy�� ������ ���� rpc ȣ���.");

        Debug.LogError(boy.transform.GetChild(0).GetChild(0) + " : Locomotion�̿�����");
        // boy�� �����ϼ� �ְ�
        boy.transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
    }

    [PunRPC]
    private void MirrorSucessRPC()
    {
        // ��罽 ����
        if (wChain != null)
        {
            wChain.SetActive(true);
            AudioManager.instance.PlaySfx(AudioManager.sfx.dropchain);

        }
    }

    [PunRPC]
    private void ManeSucessRPC()
    {
        // ���� �Ҵ� �������� ����ȵ�.
        // if (boyBook1 == null || boyBook2 == null || boyBook3 == null || boyBook4 == null || boyBook5 == null || womanHint1 == null || womanHint2 == null) return;

        if (PhotonNetwork.LocalPlayer.CustomProperties["Role"].ToString() == "Boy")
        {
            // �ҳ��϶� -> å5�� Ȱ��ȭ + ��Ʈ 1�� Ȱ��ȭ
            boyBook1.SetActive(true);
            boyBook1.transform.parent = null;
            boyBook2.SetActive(true);
            boyBook3.SetActive(true);
            boyBook4.SetActive(true);
            boyBook5.SetActive(true);
            // boyHint1.SetActive(true);

            // ���� collider ��Ȱ��ȭ
            foreach(Collider col in planes)
            {
                col.enabled = false;
            }
        }
        else if (PhotonNetwork.LocalPlayer.CustomProperties["Role"].ToString() == "Woman")
        {
            // �����϶� -> ��Ʈ 2���� ���� ��ġ��
            womanTBook.SetActive(true);
            // womanHint2.SetActive(true);
        }
    }

    [PunRPC]
    private void GlassSucessRPC()
    {
        // �ҳ� ���Ͻǿ� �����ؾ� �ϴ°͵�
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



        // ���� ���̽� ����
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
            // �ҳ��϶�

        }
        else if (PhotonNetwork.LocalPlayer.CustomProperties["Role"].ToString() == "Woman")
        {
            // �����϶�
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
        // �� Ȱ��ȭ
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

    // �÷��̾ ��ȯ�ϴ� �Լ�
    private void InstantiatePlayer()
    {
        if (PhotonNetwork.LocalPlayer.CustomProperties.ContainsKey("Role") && PhotonNetwork.LocalPlayer.CustomProperties["Role"].ToString() == "Boy")
        {
            Debug.Log("�ҳ� ����");

            // boy ����
            boy = PhotonNetwork.Instantiate(boyPrefab.name, boyTr, Quaternion.Euler(0f, 180f, 0f));

            // ismine Ű��
            boy.transform.GetChild(0).gameObject.SetActive(true);

            // boy �������̰� locomotion ��Ȱ��ȭ
            boy.transform.GetChild(0).GetChild(0).gameObject.SetActive(false);

            // boy ����
            photonView.RPC("SetBoy", RpcTarget.AllBuffered, boy.GetComponent<PhotonView>().ViewID);

            playerSpwanCnt++;
        }
        else if(PhotonNetwork.LocalPlayer.CustomProperties.ContainsKey("Role") && PhotonNetwork.LocalPlayer.CustomProperties["Role"].ToString() == "Woman")
        {
            Debug.Log("���� ����");

            // ���� ����
            woman = PhotonNetwork.Instantiate(womanPrefab.name, womanTr, Quaternion.identity);

            // ismine Ű��
            woman.transform.GetChild(0).gameObject.SetActive(true);

            // woman ����
            photonView.RPC("SetWoman", RpcTarget.AllBuffered, woman.GetComponent<PhotonView>().ViewID);

            playerSpwanCnt++;
        }
    }

    // �ٷ� �����ϸ� ���ұ� �����ϴ� �ð������� ������ ���� �������� ���Դ��� Ȯ���Ŀ� ����
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

    // �ش� ���� �÷��̾ 2�� �Դ��� Ȯ���ϴ� �Լ�
    private void CheckEnterScene()
    {
        photonView.RPC("CheckEnterSceneRPC", RpcTarget.AllBuffered);
    }

    // ���ҿ� ���� ������ ķ�� On
    private void SetOpeningCam()
    {
        if (PhotonNetwork.LocalPlayer.CustomProperties["Role"].ToString() == "Boy")
        {
            // �ҳ��� ī�޶� On ��Ŵ
            boyCam.SetActive(true);
        }
        else if (PhotonNetwork.LocalPlayer.CustomProperties["Role"].ToString() == "Woman")
        {
            // ������ ī�޶� On ��Ŵ
            womanCam.SetActive(true);
        }

        StartCoroutine(StartOpeningVideoCoroutine());
    }

    // 2��� �������� ������ �����Ű�� �Լ�
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

    // ���� �����ϴ� �Լ�
    private void StartOpeningVideo()
    {
        if (PhotonNetwork.LocalPlayer.CustomProperties["Role"].ToString() == "Boy")
        {
            // �ҳ��� ���� ���
            boyVideoPlayer.clip = boyVideo1;
            boyVideoPlayer.Play();
        }
        else if (PhotonNetwork.LocalPlayer.CustomProperties["Role"].ToString() == "Woman")
        {
            // ������ ���� ���
            womanVideoPlayer.clip = womanVideo1;
            womanVideoPlayer.Play();
        }
    }

    // ������ �������� ȣ��Ǵ� �ݹ��Լ�
    private void OnVideoEnd(VideoPlayer vd)
    {
        if (PhotonNetwork.LocalPlayer.CustomProperties["Role"].ToString() == "Boy")
        {
            // ������ �����̶��
            if (boyVideoPlayer.clip == boyVideo1)
            {
                // ������ ������ �����ٰ� ������ �ݹ�
                photonView.RPC("EndOpeningVideoRPC", RpcTarget.AllBuffered);
            }

            // �ҳ��� ���� �������
            boyVideoPlayer.Stop();
        }
        else if (PhotonNetwork.LocalPlayer.CustomProperties["Role"].ToString() == "Woman")
        {
            if (womanVideoPlayer.clip == womanVideo1)
            {
                // ������ ������ �����ٰ� ������ �ݹ�
                photonView.RPC("EndOpeningVideoRPC", RpcTarget.AllBuffered);
            }

            // ������ ���� �������
            womanVideoPlayer.Stop();
        }

        StartCoroutine(EndVideoCoroutine());
    }

    // ������ �Ѵ� ���������� ����Ǵ� �Լ�
    private IEnumerator EndVideoCoroutine()
    {
        while (true)
        {
            if (endOpeningVideoNum == 2)
            {
                // �ѳ��� ķ�� ����
                if (PhotonNetwork.LocalPlayer.CustomProperties["Role"].ToString() == "Boy")
                {
                    boyCam.SetActive(false);
                }
                else if (PhotonNetwork.LocalPlayer.CustomProperties["Role"].ToString() == "Woman")
                {
                    womanCam.SetActive(false);
                }

                // �÷��̾ ����
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

            // �ҳ� ���ǿ��� ����
            if (boyBadEnding)
            {
                boyVideoPlayer.clip = boyVideo3;
                boyVideoPlayer.Play();
            }  // �ҳ� ��忣�� ����
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
