using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Text.RegularExpressions;
using System.Collections;
using UnityEngine.Networking;


public class SignUpManager : MonoBehaviour
{
    private Button SignUp;

    [SerializeField] private GameObject SignUpBox;
    [SerializeField] private TMP_InputField Nick;
    [SerializeField] private TMP_InputField ID;
    [SerializeField] private TMP_InputField PW;
    [SerializeField] private TMP_InputField Email;

    // �����ϴ� ���̵�, ���� ����
    [SerializeField] private TextMeshProUGUI IDMessage;
    [SerializeField] private TextMeshProUGUI EmailMessage;

    // �Է¾��� ĭ �ִٰ� �˷��ִ� â
    [SerializeField] private GameObject EmptyBox;

    private void Awake()
    {
        // ��� �����ϰ� ���� �ڵ�
        ID.onValueChanged.AddListener((word) => ID.text = Regex.Replace(word, @"[^a-zA-Z]", ""));
    }
    private void Start()
    {
        SignUp = GetComponent<Button>();
        IDMessage.enabled = false;
        EmailMessage.enabled = false;
    }

    public void check()
    {
        // ��ĭ�� �ִٸ� �˾� ����
        if(Nick.text == "" || ID.text == "" || PW.text == "" || Email.text == "")
        {
           // Debug.Log("�����");
           StartCoroutine(Pop());

        }

        StartCoroutine(SignUpCoroutine(Nick.text, ID.text, PW.text, Email.text));

    }
    public void OpenPopUp()
    {
        SignUpBox.SetActive(true);
    }
    public void ClosePopUp()
    {
        SignUpBox.SetActive(false);
    }

    // ��ĭ�� �ִٰ� �˷��ִ� �˾� �ڷ�ƾ
    private IEnumerator Pop()
    {
        EmptyBox.SetActive(true);
        yield return new WaitForSeconds(1f);
        EmptyBox.SetActive(false);
    }

    private IEnumerator SignUpCoroutine(string _nick, string _id, string _pw, string _email)
    {
        string signUpUri = "http://34.47.102.147/gameSignUp.php";

        WWWForm form = new WWWForm();
        form.AddField("SignUpNick", _nick);
        form.AddField("SignUpID", _id);
        form.AddField("SignUpPW", _pw);
        form.AddField("SignUpEmail", _email);

        using (UnityWebRequest www = UnityWebRequest.Post(signUpUri, form))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.DataProcessingError)
            {
                Debug.Log(www.error);
            }
            else if (www.downloadHandler.text == "IDError")
            {
                // IDError�϶� �����.
                IDMessage.gameObject.SetActive(true);
                IDMessage.enabled = true;
                Debug.Log(www.downloadHandler.text);
            }
            else if (www.downloadHandler.text == "EmailError")
            {
                // IDError�϶� �����.
                EmailMessage.gameObject.SetActive(true);
                EmailMessage.enabled = true;
                Debug.Log(www.downloadHandler.text);
            }
            else
            {
                // �ƹ����� �ȳ��� �����.
                Debug.Log(www.downloadHandler.text);
                SignUpBox.SetActive(false);
            }
        }
    }
}
