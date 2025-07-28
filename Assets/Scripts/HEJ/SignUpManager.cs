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

    // 존재하는 아이디, 메일 문구
    [SerializeField] private TextMeshProUGUI IDMessage;
    [SerializeField] private TextMeshProUGUI EmailMessage;

    // 입력안한 칸 있다고 알려주는 창
    [SerializeField] private GameObject EmptyBox;

    private void Awake()
    {
        // 영어만 가능하게 막는 코드
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
        // 빈칸이 있다면 팝업 실행
        if(Nick.text == "" || ID.text == "" || PW.text == "" || Email.text == "")
        {
           // Debug.Log("비었음");
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

    // 빈칸이 있다고 알려주는 팝업 코루틴
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
                // IDError일때 실행됨.
                IDMessage.gameObject.SetActive(true);
                IDMessage.enabled = true;
                Debug.Log(www.downloadHandler.text);
            }
            else if (www.downloadHandler.text == "EmailError")
            {
                // IDError일때 실행됨.
                EmailMessage.gameObject.SetActive(true);
                EmailMessage.enabled = true;
                Debug.Log(www.downloadHandler.text);
            }
            else
            {
                // 아무오류 안나면 실행됨.
                Debug.Log(www.downloadHandler.text);
                SignUpBox.SetActive(false);
            }
        }
    }
}
