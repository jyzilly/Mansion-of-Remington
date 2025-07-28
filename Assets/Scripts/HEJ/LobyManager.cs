using System.Collections;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;


public class LobyManager : MonoBehaviourPunCallbacks
{
    // 오류 메세지 팝업 변수
    private string[] dialogs;
    public Button[] buttonList;

    [SerializeField] private GameObject createPopup;
    [SerializeField] private GameObject findPopup;
    [SerializeField] private GameObject codePopup;
    [SerializeField] private GameObject errorPopup;
    [SerializeField] private GameObject roomPrefab;
    [SerializeField] private GameObject contentRoom;
    [SerializeField] private Button joinBtn;
    [SerializeField] private Button codeBtn;
    [SerializeField] private TextMeshProUGUI errorText;
    [SerializeField] private TMP_InputField roomCode;
    [SerializeField] private GameObject roomScene;
    [SerializeField] private TMP_InputField joinRoomCode;
    [SerializeField] private GameObject lobbyCanvas;
    private bool firstEnter = false;

    // 방안 관련 변수들
    [SerializeField] private TMP_Text inRoomCode;
    [SerializeField] private TMP_Text[] userNicks = new TMP_Text[2];
    [SerializeField] private TMP_Text womanName;
    [SerializeField] private TMP_Text boyName;
    [SerializeField] private Button boyBtn;
    [SerializeField] private Button womanBtn;
    [SerializeField] private Button roomStart;
    private bool boySelected = false;
    private bool womanSelected = false;
    private bool callOneTime = false;
    private string clickedRoomInfo = "";

    private void Start()
    {
        dialogs = new string[] {
            "Room Code Not Found!",
            "This Room is Full!",
            "This Room is Exist"
        };
        joinBtn.onClick.AddListener(ClickJoin);
        
        // 같이 씬넘길때 동기화 시키면서 넘기기 위해 포톤기능 사용
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    private void Update()
    {
        RoomStartBtnOnOff();
    }

    #region 방만들기 버튼관련

    // 방만들기 팝업을 뛰움("방만들기" 눌렀을때 실행)
    public void OpenCreatePopUP()
    {
        createPopup.SetActive(true);
        codePopup.SetActive(false);
        findPopup.SetActive(false);
    }

    // 방을 생성함(Create버튼 눌렀을때 실행)
    public void CreateRoom()
    {
        if(roomCode.text.Length < 5)
        {
            Debug.Log("NO");
        }
        else
        {
            // 방생성
            CreatePhotonRoom(roomCode.text);
        }
    }

    #endregion

    #region 방찾기 버튼 관련

    // JoinLobby 창을 뛰움(방찾기 버튼 눌렀을때 실행)
    public void FindPopUP()
    {
        createPopup.SetActive(false);
        codePopup.SetActive(false);
        findPopup.SetActive(true);

        // 로비로 들어가기
        PhotonNetwork.JoinLobby();
    }

    // 로비 방에 참가.(Join버튼 눌렀을때 실행)
    private void ClickJoin()
    {
        if (clickedRoomInfo == "")
        {
            Debug.Log("방을 선택 안함.");
        }
        else
        {
            PhotonNetwork.JoinRoom(clickedRoomInfo);
        }
    }

    #region 코드로 방찾기 관련
    // 코드 입력창을 뛰움(RoomCode 클릭시 실행)
    public void CodePopUp()
    {
        codePopup.SetActive(true);
    }

    // 코드 입력창을 닫음(X 클릭시 실행)
    public void CodePopUpClose()
    {
        codePopup.SetActive(false);
    }

    // 확인 버튼 눌렀을 때
    public void CheckCode()
    {
        PhotonNetwork.JoinRoom(joinRoomCode.text);
    }
    #endregion

    #region 방안에서 작동하는 버튼 관련

    // 기자 그림 눌렀을때
    // 더이상 선택못하고(비활성화), 선택한 플레이어의 닉네임의 알파값을 바꿈.
    public void ClickWoman()
    {
        womanBtn.interactable = false;
        womanSelected = true;

        // 선택한 플레이어의 닉네임 알파값 조정
        SetAlphaUserNick(0.5f, PhotonNetwork.NickName);

        // text이름 바꾸기
        womanName.text = PhotonNetwork.NickName;

        // 동기화를 위한 RPC
        photonView.RPC("WomanClicked", RpcTarget.OthersBuffered, PhotonNetwork.NickName);
    }

    // 소년 그림 눌렀을때
    public void ClickBoy()
    {
        boyBtn.interactable = false;
        boySelected = true;

        // 선택한 플레이어의 닉네임 알파값 조정
        SetAlphaUserNick(0.5f, PhotonNetwork.NickName);

        // 이름 바꿔야함.
        boyName.text = PhotonNetwork.NickName;

        // 동기화를 위한 RPC
        photonView.RPC("BoyClicked", RpcTarget.OthersBuffered, PhotonNetwork.NickName);
    }

    // 가운데 대기화면 눌렀을때
    public void ClickWait()
    {
        if (boySelected && PhotonNetwork.NickName == boyName.text)
        {
            boySelected = false;
            boyBtn.interactable = true;
            boyName.text = "";

            // 선택한 플레이어의 닉네임 알파값 조정
            SetAlphaUserNick(1f, PhotonNetwork.NickName);

            // 동기화를 위한 RPC
            photonView.RPC("BoyWaitClicked", RpcTarget.OthersBuffered, PhotonNetwork.NickName);
        }
        else if (womanSelected && PhotonNetwork.NickName == womanName.text)
        {
            womanSelected = false;
            womanBtn.interactable = true;
            womanName.text = "";

            // 선택한 플레이어의 닉네임 알파값 조정
            SetAlphaUserNick(1f, PhotonNetwork.NickName);

            // 동기화를 위한 RPC
            photonView.RPC("WomanWaitClicked", RpcTarget.OthersBuffered, PhotonNetwork.NickName);
        }
    }

    // 게임 시작 버튼 눌렀을때
    public void ClickRoomStart()
    {
        Debug.Log(PhotonNetwork.NickName);

        photonView.RPC("ClickRoomStartRPC", RpcTarget.All);

        // 다음씬으로 넘어가도록
        PhotonNetwork.LoadLevel("MainScene");
    }

    #endregion

    #endregion

    #region 로그아웃 버튼 관련
    // 로그아웃 버튼 눌렀을때
    public void LoginOut()
    {
        // 포톤 서버와 연결끊기
        PhotonNetwork.Disconnect();

        // 다시 로비씬으로
        // 스크립트 이름이 SceneManager면 화나요
        UnityEngine.SceneManagement.SceneManager.LoadScene("HEJ_Scene");
    }

    #endregion

    #region 네트워크 관련 함수들
    // 네트워크 상에서 방을 만들어줌.
    private void CreatePhotonRoom(string _roomname)
    {
        RoomOptions roomOptions = new RoomOptions();

        roomOptions.MaxPlayers = 2;
        PhotonNetwork.CreateRoom(_roomname, roomOptions, TypedLobby.Default);
    }

    #endregion

    #region 네트워크 콜백 함수들

    // 룸 갱신 함수
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        // 현재 있는 프리펩을 다 들고와서
        CheckClickRoom[] prefabs = contentRoom.transform.GetComponentsInChildren<CheckClickRoom>();

        // 처음 들어갔을때 방정보 리셋시킴
        if (firstEnter)
        {
            foreach (CheckClickRoom prefab in prefabs)
            {
                Destroy(roomPrefab.gameObject);
            }
            firstEnter = false;
        }

        // 방생성
        foreach (RoomInfo room in roomList)
        {
            // 각 방에 대해 프리팹 인스턴스화
            GameObject roomItem = Instantiate(roomPrefab, contentRoom.transform);

            // 프리펩이 클릭됬을때 호출되는 함수등록
            roomItem.GetComponent<CheckClickRoom>().roomClickedCallback += SetRoomInfo;

            // 0번째 자식에 룸 이름 넣기
            TMP_Text roomNameText = roomItem.transform.GetChild(0).GetComponent<TMP_Text>();
            if (roomNameText != null)
            {
                roomNameText.text = "Room: " + room.Name;
            }

            // 1번째 자식에 플레이어 수 넣기
            TMP_Text playerNameText = roomItem.transform.GetChild(1).GetComponent<TMP_Text>();
            if (playerNameText != null)
            {
                playerNameText.text = "Players: " + room.PlayerCount + "/" + room.MaxPlayers;
            }
        }

        // 현재 있는 프리펩을 다 들고와서
        prefabs = contentRoom.transform.GetComponentsInChildren<CheckClickRoom>();

        // 실시간 방 상태 전달함.
        if (!firstEnter && roomList.Count != 0)
        {
            // 플레이어 카운트0 (방나간 상태)
            if (roomList[0].PlayerCount == 0)
            {
                foreach (CheckClickRoom prefab in prefabs)
                {
                    if ("Room: " + roomList[0].Name == prefab.transform.GetChild(0).GetComponent<TMP_Text>().text)
                    {
                        Destroy(prefab.gameObject);
                    }

                }
            }
        }
    }

    // 방 생성 성공 시 호출되는 콜백
    public override void OnCreatedRoom()
    {
        Debug.Log("방이 성공적으로 생성되었습니다.");
    }

    // 룸 들어가면 호출
    public override void OnJoinedRoom()
    {
        Debug.Log("방에 성공적으로 들어감");

        // 씬을 뛰우면 될듯
        roomScene.SetActive(true);
        lobbyCanvas.SetActive(false);

        // 씬을 플레이어 정보와 동기화 시키는 something이 필요함.
        // 필요한 정보 : 플레이어 닉네임, 룸코드
        SetRoom();
    }

    // 룸 들어가기 실패시 호출
    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        // 코드가 잘못됬을때 호출
        if (returnCode == 32758)
        {
            errorText.text = dialogs[0];
            StartCoroutine(ErrorPopup());
        }

        // 인원이 다 찼을때 호출
        if (returnCode == 32765)
        {
            errorText.text = dialogs[1];
            StartCoroutine(ErrorPopup());
        }

        // 방 인원 꽉찼을때 호출

        Debug.LogError("방 입장 실패! 에러 코드: " + returnCode + ", 메시지: " + message);
    }

    // 방 생성 실패 시 호출되는 콜백
    public override void OnCreateRoomFailed(short errorCode, string errorMessage)
    {
        if (errorCode == 32766)
        {
            // 에러창 뛰우기
            errorText.text = dialogs[2];
            StartCoroutine(ErrorPopup());
        }
        Debug.LogError("방 생성 실패! 에러 코드: " + errorCode + ", 메시지: " + errorMessage);
    }

    // 로비 입장 성공시 호출
    public override void OnJoinedLobby()
    {
        firstEnter = true;
        Debug.Log("로비입장 성공!");
    }

    // 로비 나가면 호출
    public override void OnLeftLobby()
    {
        firstEnter = false;
        Debug.Log("로비 나감!");
    }

    // 플레이어가 방에 들어올때 호출되는 함수
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        SetRoom();
    }

    // 플레이어가 방에 나갈때 호출되는 함수
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        SetRoom();
    }

    #endregion

    #region 콜백받는 rpc 함수들

    [PunRPC]
    private void WomanClicked(string _name)
    {
        womanBtn.interactable = false;
        womanSelected = true;

        // 선택한 플레이어의 닉네임 알파값 조정
        SetAlphaUserNick(0.5f, _name);

        womanName.text = _name;
    }

    [PunRPC]
    private void BoyClicked(string _name)
    {
        boyBtn.interactable = false;
        boySelected = true;

        // 선택한 플레이어의 닉네임 알파값 조정
        SetAlphaUserNick(0.5f, _name);

        boyName.text = _name;
    }

    [PunRPC]
    private void BoyWaitClicked(string _name)
    {
        boySelected = false;
        boyBtn.interactable = true;
        boyName.text = "";

        // 선택한 플레이어의 닉네임 알파값 조정
        SetAlphaUserNick(1f, _name);
    }

    [PunRPC]
    private void WomanWaitClicked(string _name)
    {
        womanSelected = false;
        womanBtn.interactable = true;
        womanName.text = "";

        // 선택한 플레이어의 닉네임 알파값 조정
        SetAlphaUserNick(1f, _name);
    }

    [PunRPC]
    private void ClickRoomStartRPC()
    {
        if (boyName.text == PhotonNetwork.NickName)
        {
            ExitGames.Client.Photon.Hashtable playerProperties = new ExitGames.Client.Photon.Hashtable();
            playerProperties.Add("Role", "Boy");
            PhotonNetwork.LocalPlayer.SetCustomProperties(playerProperties);
        }
        else
        {
            ExitGames.Client.Photon.Hashtable playerProperties = new ExitGames.Client.Photon.Hashtable();
            playerProperties.Add("Role", "Woman");
            PhotonNetwork.LocalPlayer.SetCustomProperties(playerProperties);
        }
    }
    #endregion

    // 방입장 에러 팝업창 코루틴
    private IEnumerator ErrorPopup()
    {
        errorPopup.SetActive(true);
        yield return new WaitForSeconds(1f);
        errorPopup.SetActive(false);
        errorText.text = "";
    }

    // 방 들어가면 방세팅(룸코드, 플레이어 nick)
    private void SetRoom()
    { 
        // 방이름 설정
        inRoomCode.text = PhotonNetwork.CurrentRoom.Name;

        // 이름 설정
        for (int i = 0; i < PhotonNetwork.CurrentRoom.PlayerCount; i++)
        {
            userNicks[i].text = PhotonNetwork.PlayerList[i].NickName;
        }
    }

    // 플레이어의 닉네임 알파값 조정 (방 안의 플레이어 alpha값만 조정가능)
    private void SetAlphaUserNick(float _alpha, string _name)
    {
        for (int i = 0; i < PhotonNetwork.CurrentRoom.PlayerCount; i++)
        {
            if (userNicks[i].text == _name)
            {
                Color currentColor = userNicks[i].color;
                currentColor.a = _alpha;
                userNicks[i].color = currentColor;
            }
        }
    }

    // 방안에 시작버튼 활성화 비활성화를 결정함. (방장만)
    private void RoomStartBtnOnOff()
    {
        if ((boySelected && womanSelected) && !callOneTime)
        {
            if (PhotonNetwork.IsMasterClient)
            {
                roomStart.interactable = true;
                callOneTime = true;

                Color currentColor = roomStart.image.color;
                currentColor.a = 1f;
                roomStart.image.color = currentColor;
            }
        }
        else if ((!boySelected || !womanSelected) && callOneTime)
        {
            roomStart.interactable = false;
            callOneTime = false;

            Color currentColor = roomStart.image.color;
            currentColor.a = 0.5f;
            roomStart.image.color = currentColor;
        }
    }

    // RoomInfo를 설정
    private void SetRoomInfo(string _roomName)
    {
        Debug.Log("콜백 호출됨 : " + _roomName);

        clickedRoomInfo = _roomName;
    }
}
