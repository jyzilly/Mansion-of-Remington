using UnityEngine;


//��ġ�� �ν��ؼ� �´� ��ġ�� ��ġ���� �� �ٸ� ���� ����� �ǰ� �� �ȿ� ��ư �ְ� 
//���� ������ �������� ó�� 
//��ġ�� �� 1�� 2���� �ð� ������ ��� �� 
//��ġ �ν� - ��Ī�ϴ� ��  - Ÿ�� & Trigger   
//�´� ��ġ�� �θ� mirror & backPan ��Ȱ��ȭ �ȴ�.

public class MirrorP : MonoBehaviour
{
    //������ �������� �� ȣ��� �ݹ� �Լ��� ����
    public delegate void MirroDelegate();
    public MirroDelegate mirroSucessCallback;

    [Header("Default Settings")]
    [SerializeField] private GameObject Mirror;
    [SerializeField] private GameObject MirrorBackpan;
    [SerializeField] private GameObject Wall;

    [SerializeField] private MirrorButtonController mirrorbutton;
    [SerializeField] private MirroInsideButtonController mirrorinsidebutton;

    public bool TheResult = false;

    private void Update()
    {
        //�񱳿����ڷ� ���� �������� Ȯ��
        if (mirrorbutton.TheButtonisPressed == true && mirrorinsidebutton.TheButtonisPressed == true && !TheResult)
        {
            //Debug.Log("���� ������");
            TheResult = true;
            //������ �ܺ� �ý��ۿ� ������ �Ϸ�Ǿ����� �˸�
            mirroSucessCallback?.Invoke();
        }
        
    }

    //�ſ� ��ġ�� �±�Ʋ ����, �ſ� ������ ������ ����
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "TheMirrorPlace")
        {
            //Debug.Log("�۵�");

            Mirror.SetActive(false);
            MirrorBackpan.SetActive(false);
            Wall.SetActive(false);

            //Invoke("Mirror.SetActive(false)", 1f);
            //Invoke("Mirrorbackpan.SetActive(false)", 1f);
            //Invoke("Wall.SetActive(false)", 1f);

        }
    }


}
