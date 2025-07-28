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
    // ���� �޼��� �˾� ����
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

    // ��� ���� ������
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
        
        // ���� ���ѱ涧 ����ȭ ��Ű�鼭 �ѱ�� ���� ������ ���
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    private void Update()
    {
        RoomStartBtnOnOff();
    }

    #region �游��� ��ư����

    // �游��� �˾��� �ٿ�("�游���" �������� ����)
    public void OpenCreatePopUP()
    {
        createPopup.SetActive(true);
        codePopup.SetActive(false);
        findPopup.SetActive(false);
    }

    // ���� ������(Create��ư �������� ����)
    public void CreateRoom()
    {
        if(roomCode.text.Length < 5)
        {
            Debug.Log("NO");
        }
        else
        {
            // �����
            CreatePhotonRoom(roomCode.text);
        }
    }

    #endregion

    #region ��ã�� ��ư ����

    // JoinLobby â�� �ٿ�(��ã�� ��ư �������� ����)
    public void FindPopUP()
    {
        createPopup.SetActive(false);
        codePopup.SetActive(false);
        findPopup.SetActive(true);

        // �κ�� ����
        PhotonNetwork.JoinLobby();
    }

    // �κ� �濡 ����.(Join��ư �������� ����)
    private void ClickJoin()
    {
        if (clickedRoomInfo == "")
        {
            Debug.Log("���� ���� ����.");
        }
        else
        {
            PhotonNetwork.JoinRoom(clickedRoomInfo);
        }
    }

    #region �ڵ�� ��ã�� ����
    // �ڵ� �Է�â�� �ٿ�(RoomCode Ŭ���� ����)
    public void CodePopUp()
    {
        codePopup.SetActive(true);
    }

    // �ڵ� �Է�â�� ����(X Ŭ���� ����)
    public void CodePopUpClose()
    {
        codePopup.SetActive(false);
    }

    // Ȯ�� ��ư ������ ��
    public void CheckCode()
    {
        PhotonNetwork.JoinRoom(joinRoomCode.text);
    }
    #endregion

    #region ��ȿ��� �۵��ϴ� ��ư ����

    // ���� �׸� ��������
    // ���̻� ���ø��ϰ�(��Ȱ��ȭ), ������ �÷��̾��� �г����� ���İ��� �ٲ�.
    public void ClickWoman()
    {
        womanBtn.interactable = false;
        womanSelected = true;

        // ������ �÷��̾��� �г��� ���İ� ����
        SetAlphaUserNick(0.5f, PhotonNetwork.NickName);

        // text�̸� �ٲٱ�
        womanName.text = PhotonNetwork.NickName;

        // ����ȭ�� ���� RPC
        photonView.RPC("WomanClicked", RpcTarget.OthersBuffered, PhotonNetwork.NickName);
    }

    // �ҳ� �׸� ��������
    public void ClickBoy()
    {
        boyBtn.interactable = false;
        boySelected = true;

        // ������ �÷��̾��� �г��� ���İ� ����
        SetAlphaUserNick(0.5f, PhotonNetwork.NickName);

        // �̸� �ٲ����.
        boyName.text = PhotonNetwork.NickName;

        // ����ȭ�� ���� RPC
        photonView.RPC("BoyClicked", RpcTarget.OthersBuffered, PhotonNetwork.NickName);
    }

    // ��� ���ȭ�� ��������
    public void ClickWait()
    {
        if (boySelected && PhotonNetwork.NickName == boyName.text)
        {
            boySelected = false;
            boyBtn.interactable = true;
            boyName.text = "";

            // ������ �÷��̾��� �г��� ���İ� ����
            SetAlphaUserNick(1f, PhotonNetwork.NickName);

            // ����ȭ�� ���� RPC
            photonView.RPC("BoyWaitClicked", RpcTarget.OthersBuffered, PhotonNetwork.NickName);
        }
        else if (womanSelected && PhotonNetwork.NickName == womanName.text)
        {
            womanSelected = false;
            womanBtn.interactable = true;
            womanName.text = "";

            // ������ �÷��̾��� �г��� ���İ� ����
            SetAlphaUserNick(1f, PhotonNetwork.NickName);

            // ����ȭ�� ���� RPC
            photonView.RPC("WomanWaitClicked", RpcTarget.OthersBuffered, PhotonNetwork.NickName);
        }
    }

    // ���� ���� ��ư ��������
    public void ClickRoomStart()
    {
        Debug.Log(PhotonNetwork.NickName);

        photonView.RPC("ClickRoomStartRPC", RpcTarget.All);

        // ���������� �Ѿ����
        PhotonNetwork.LoadLevel("MainScene");
    }

    #endregion

    #endregion

    #region �α׾ƿ� ��ư ����
    // �α׾ƿ� ��ư ��������
    public void LoginOut()
    {
        // ���� ������ �������
        PhotonNetwork.Disconnect();

        // �ٽ� �κ������
        // ��ũ��Ʈ �̸��� SceneManager�� ȭ����
        UnityEngine.SceneManagement.SceneManager.LoadScene("HEJ_Scene");
    }

    #endregion

    #region ��Ʈ��ũ ���� �Լ���
    // ��Ʈ��ũ �󿡼� ���� �������.
    private void CreatePhotonRoom(string _roomname)
    {
        RoomOptions roomOptions = new RoomOptions();

        roomOptions.MaxPlayers = 2;
        PhotonNetwork.CreateRoom(_roomname, roomOptions, TypedLobby.Default);
    }

    #endregion

    #region ��Ʈ��ũ �ݹ� �Լ���

    // �� ���� �Լ�
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        // ���� �ִ� �������� �� ���ͼ�
        CheckClickRoom[] prefabs = contentRoom.transform.GetComponentsInChildren<CheckClickRoom>();

        // ó�� ������ ������ ���½�Ŵ
        if (firstEnter)
        {
            foreach (CheckClickRoom prefab in prefabs)
            {
                Destroy(roomPrefab.gameObject);
            }
            firstEnter = false;
        }

        // �����
        foreach (RoomInfo room in roomList)
        {
            // �� �濡 ���� ������ �ν��Ͻ�ȭ
            GameObject roomItem = Instantiate(roomPrefab, contentRoom.transform);

            // �������� Ŭ�������� ȣ��Ǵ� �Լ����
            roomItem.GetComponent<CheckClickRoom>().roomClickedCallback += SetRoomInfo;

            // 0��° �ڽĿ� �� �̸� �ֱ�
            TMP_Text roomNameText = roomItem.transform.GetChild(0).GetComponent<TMP_Text>();
            if (roomNameText != null)
            {
                roomNameText.text = "Room: " + room.Name;
            }

            // 1��° �ڽĿ� �÷��̾� �� �ֱ�
            TMP_Text playerNameText = roomItem.transform.GetChild(1).GetComponent<TMP_Text>();
            if (playerNameText != null)
            {
                playerNameText.text = "Players: " + room.PlayerCount + "/" + room.MaxPlayers;
            }
        }

        // ���� �ִ� �������� �� ���ͼ�
        prefabs = contentRoom.transform.GetComponentsInChildren<CheckClickRoom>();

        // �ǽð� �� ���� ������.
        if (!firstEnter && roomList.Count != 0)
        {
            // �÷��̾� ī��Ʈ0 (�泪�� ����)
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

    // �� ���� ���� �� ȣ��Ǵ� �ݹ�
    public override void OnCreatedRoom()
    {
        Debug.Log("���� ���������� �����Ǿ����ϴ�.");
    }

    // �� ���� ȣ��
    public override void OnJoinedRoom()
    {
        Debug.Log("�濡 ���������� ��");

        // ���� �ٿ�� �ɵ�
        roomScene.SetActive(true);
        lobbyCanvas.SetActive(false);

        // ���� �÷��̾� ������ ����ȭ ��Ű�� something�� �ʿ���.
        // �ʿ��� ���� : �÷��̾� �г���, ���ڵ�
        SetRoom();
    }

    // �� ���� ���н� ȣ��
    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        // �ڵ尡 �߸������� ȣ��
        if (returnCode == 32758)
        {
            errorText.text = dialogs[0];
            StartCoroutine(ErrorPopup());
        }

        // �ο��� �� á���� ȣ��
        if (returnCode == 32765)
        {
            errorText.text = dialogs[1];
            StartCoroutine(ErrorPopup());
        }

        // �� �ο� ��á���� ȣ��

        Debug.LogError("�� ���� ����! ���� �ڵ�: " + returnCode + ", �޽���: " + message);
    }

    // �� ���� ���� �� ȣ��Ǵ� �ݹ�
    public override void OnCreateRoomFailed(short errorCode, string errorMessage)
    {
        if (errorCode == 32766)
        {
            // ����â �ٿ��
            errorText.text = dialogs[2];
            StartCoroutine(ErrorPopup());
        }
        Debug.LogError("�� ���� ����! ���� �ڵ�: " + errorCode + ", �޽���: " + errorMessage);
    }

    // �κ� ���� ������ ȣ��
    public override void OnJoinedLobby()
    {
        firstEnter = true;
        Debug.Log("�κ����� ����!");
    }

    // �κ� ������ ȣ��
    public override void OnLeftLobby()
    {
        firstEnter = false;
        Debug.Log("�κ� ����!");
    }

    // �÷��̾ �濡 ���ö� ȣ��Ǵ� �Լ�
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        SetRoom();
    }

    // �÷��̾ �濡 ������ ȣ��Ǵ� �Լ�
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        SetRoom();
    }

    #endregion

    #region �ݹ�޴� rpc �Լ���

    [PunRPC]
    private void WomanClicked(string _name)
    {
        womanBtn.interactable = false;
        womanSelected = true;

        // ������ �÷��̾��� �г��� ���İ� ����
        SetAlphaUserNick(0.5f, _name);

        womanName.text = _name;
    }

    [PunRPC]
    private void BoyClicked(string _name)
    {
        boyBtn.interactable = false;
        boySelected = true;

        // ������ �÷��̾��� �г��� ���İ� ����
        SetAlphaUserNick(0.5f, _name);

        boyName.text = _name;
    }

    [PunRPC]
    private void BoyWaitClicked(string _name)
    {
        boySelected = false;
        boyBtn.interactable = true;
        boyName.text = "";

        // ������ �÷��̾��� �г��� ���İ� ����
        SetAlphaUserNick(1f, _name);
    }

    [PunRPC]
    private void WomanWaitClicked(string _name)
    {
        womanSelected = false;
        womanBtn.interactable = true;
        womanName.text = "";

        // ������ �÷��̾��� �г��� ���İ� ����
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

    // ������ ���� �˾�â �ڷ�ƾ
    private IEnumerator ErrorPopup()
    {
        errorPopup.SetActive(true);
        yield return new WaitForSeconds(1f);
        errorPopup.SetActive(false);
        errorText.text = "";
    }

    // �� ���� �漼��(���ڵ�, �÷��̾� nick)
    private void SetRoom()
    { 
        // ���̸� ����
        inRoomCode.text = PhotonNetwork.CurrentRoom.Name;

        // �̸� ����
        for (int i = 0; i < PhotonNetwork.CurrentRoom.PlayerCount; i++)
        {
            userNicks[i].text = PhotonNetwork.PlayerList[i].NickName;
        }
    }

    // �÷��̾��� �г��� ���İ� ���� (�� ���� �÷��̾� alpha���� ��������)
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

    // ��ȿ� ���۹�ư Ȱ��ȭ ��Ȱ��ȭ�� ������. (���常)
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

    // RoomInfo�� ����
    private void SetRoomInfo(string _roomName)
    {
        Debug.Log("�ݹ� ȣ��� : " + _roomName);

        clickedRoomInfo = _roomName;
    }
}
