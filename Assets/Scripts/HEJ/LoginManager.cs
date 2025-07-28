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

        // DB ��
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
            // ���̵� ����ġ
            else if (www.downloadHandler.text == "IDError")
            {
                popup.SetActive(true);
                textBox.text = dialogs[0];
            }
            // PW ����ġ
            else if (www.downloadHandler.text == "PWError")
            {
                popup.SetActive(true);
                textBox.text = dialogs[1];
            }
            // �α��� ��.
            else
            {
                // echo�� nick�� playerNick�� ����
                playerNick = www.downloadHandler.text;

                // ���� ������ ������ ��.
                PhotonNetwork.ConnectUsingSettings();

                // ���� ������ ��ٸ� ���� ������ �Ѿ�� �ȉ����� �׳� �����ٿ��
                Debug.Log("DB�� ID,PW���� �������� �α��ε�.");
            }
        }
    }

    // ������ ������ ������
    public override void OnConnectedToMaster()
    {
        // ���� ������ �Ѿ���� �ϸ� �ɵ�.
        Debug.Log("���� ���� ����");

        // �г��� ����
        PhotonNetwork.NickName = playerNick;

        // Login Scene�� false�ϰ�, LobbyScene�� On
        loginCanvas.SetActive(false);
        lobbyCanvas.SetActive(true);
    }

    // ������ ������ ���н�
    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log("���� ���� ����! ����: " + cause.ToString());
    }

}
