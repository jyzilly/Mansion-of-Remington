using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.Networking;
using System.Collections;
using Photon.Pun;
using Photon.Realtime;

public class LoginManager : MonoBehaviourPunCallbacks
{
    private Button Signin;
    private string[] dialogs;

    [SerializeField] private TMP_InputField Id;
    [SerializeField] private TMP_InputField Pw;
    [SerializeField] private GameObject popup;
    [SerializeField] private TextMeshProUGUI textBox;
    [SerializeField] private GameObject loginCanvas;
    [SerializeField] private GameObject lobbyCanvas;

    private string playerNick;

    private void Awake()
    {
        
        Signin = GetComponent<Button>();
    }

    private void Start()
    {
        textBox.text = "";
        dialogs = new string[] {
            "Check your ID",
            "Check your PW",
        };
        
    }

    public void Check()
    {
        if(Id.text == "")
        {
            popup.SetActive(true);
            textBox.text = dialogs[0];
        }
        else if (Pw.text == "")
        {
            popup.SetActive(true);
            textBox.text = dialogs[1];
        }

        // DB 비교
        StartCoroutine(LoginCoroutine(Id.text, Pw.text));
    }

    public void onClickXBtn() {

        popup.SetActive(false);
    }

    private IEnumerator LoginCoroutine(string _id, string _pw)
    {
        string loginUri = "http://34.47.102.147/gameLogin.php";

        WWWForm form = new WWWForm();
        form.AddField("LoginID", _id);
        form.AddField("LoginPW", _pw);

        using (UnityWebRequest www = UnityWebRequest.Post(loginUri, form))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.DataProcessingError)
            {
                Debug.Log(www.error);
            }
            // 아이디 불일치
            else if (www.downloadHandler.text == "IDError")
            {
                popup.SetActive(true);
                textBox.text = dialogs[0];
            }
            // PW 불일치
            else if (www.downloadHandler.text == "PWError")
            {
                popup.SetActive(true);
                textBox.text = dialogs[1];
            }
            // 로그인 됨.
            else
            {
                // echo한 nick을 playerNick에 저장
                playerNick = www.downloadHandler.text;

                // 포톤 서버와 연결을 함.
                PhotonNetwork.ConnectUsingSettings();

                // 포톤 연결이 됬다면 다음 씬으로 넘어가고 안됬으면 그냥 오류뛰우기
                Debug.Log("DB와 ID,PW에는 문제없이 로그인됨.");
            }
        }
    }

    // 서버와 연결이 성공시
    public override void OnConnectedToMaster()
    {
        // 다음 씬으로 넘어가도록 하면 될듯.
        Debug.Log("서버 연결 성공");

        // 닉네임 설정
        PhotonNetwork.NickName = playerNick;

        // Login Scene을 false하고, LobbyScene을 On
        loginCanvas.SetActive(false);
        lobbyCanvas.SetActive(true);
    }

    // 서버와 연결이 실패시
    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log("서버 연결 실패! 원인: " + cause.ToString());
    }

}
