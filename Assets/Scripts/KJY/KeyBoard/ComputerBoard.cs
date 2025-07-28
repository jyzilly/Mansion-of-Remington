using UnityEngine;
using UnityEngine.UI;

//��ǻ�� ���
//��ǻ�� ȭ�� ����, Ű�� �Է��ϸ� ��ǻ�� ȭ�� ����, CCTV, Mail �� ȭ��

public class ComputerBoard : MonoBehaviour
{
    [SerializeField] private GameObject passwordScreen;
    [SerializeField] private Image LoginImg;
    [SerializeField] private Image CCTVIMg;
    [SerializeField] private Image MailIMg;
    [SerializeField] private GameObject mainScreen;
    [SerializeField] private GameObject mailScreen;
    [SerializeField] private GameObject cctvScreen;


    [SerializeField] private Button CCTVButton;
    [SerializeField] private Button MailButton;
    [SerializeField] private Button MemoButton;

    private bool Result = false;


    private void Start()
    {
        CCTVButton.onClick.AddListener(CCTVButtonOnClick);
    }

    //�ʱ� �α��� ���� �� ȣ��Ǿ� ���� ȭ������ ��ȯ
    private void LoginEvent()
    {
        if(Result == true)
        {
            //��й�ȣ â�� �α��� �������� ����� ���� ȭ���� ǥ��
            LoginImg.enabled = false;
            passwordScreen.SetActive(false);
            mainScreen.SetActive(true);

            //���� �÷��׸� �ٽ� false�� �����Ͽ� ���� ��й�ȣ �Է��� �غ�
            Result = false;
        }
    }

    //CCTV ��ư Ŭ�� �� ȣ��Ǿ� ��й�ȣ Ȯ�� ������ ����
    private void CCTVButtonOnClick()
    {
        mainScreen.SetActive(false);
        passwordScreen.SetActive(true);
        CCTVIMg.enabled = true;
    }

    //CCTV ��й�ȣ �Է� ���� �� ȣ��Ǿ� CCTV ȭ������ ��ȯ
    private void CCTVEvent()
    {
        if(Result == true)
        {
            CCTVIMg.enabled = false;
            passwordScreen.SetActive(false);
            cctvScreen.SetActive(true);
            Result = false;
        }
    }

}
