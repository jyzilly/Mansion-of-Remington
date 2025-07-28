using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;



//�÷��̾ 9���� �е� �� �ùٸ� ����("L", "A", "S", "T")�� 4���� ������ ����

public class PadButtonController : MonoBehaviour
{
    [Header("Default Settings")]
    [SerializeField] private GameObject LastDoor;
    [SerializeField] private GameObject PadPuzzle;
    [SerializeField] private AudioClip audioClips;

    //���̴� ȿ��
    private FireBurnOutShading lastDoorEffect;
    private FireBurnOutShadingChain padPuzzleEffect;
    //��Ȱ��ȭ�� UI ĵ������
    private Canvas[] canvases;

    //�����
    public bool debugST;

    //�������
    List<string> FinalResult = new List<string> { "L", "A", "S", "T" };
    //�����Է� ���� �����ϴ� ����Ʈ
    [SerializeField] private List<string> CurResult1 = new List<string>();

    //�������
    public bool TheResult = false;

    //�� �е� ��Ƽ���� �迭 -> 9��
    public Material[] mats;

    private void Awake()
    {
        lastDoorEffect = LastDoor.GetComponent<FireBurnOutShading>();
        padPuzzleEffect = PadPuzzle.GetComponent<FireBurnOutShadingChain>();
        canvases = GetComponentsInChildren<Canvas>();
    }
    private void Start()
    {
        //�ʱ����
        ResetColor();
        
    }


    private void Update()
    {
        //�Է��� 4���� �Ǵ� ���� CheckTheResult()�� 4�� ȣ���ϰ� ��

        if (CurResult1.Count == 4)
        {
            foreach (string Cur1 in CurResult1)
            {
                //Debug.Log(Cur1);
                CheckTheResult();
            }
        }

        //����׿� -> ���� ����
        if (debugST && Input.GetKeyDown(KeyCode.L))
        {
            IfCan();
        }

    }


    private void CheckTheResult()
    {

        if (CurResult1.SequenceEqual(FinalResult))
        {
            TheResult = true;
            //���� ������ ȣ��
            IfCan();
            //Debug.Log("����");
            //�������� �Ҹ� ������ ���� ���� ������. -> ����
        }

        //���� ���ο� �������, �Է��� ������ �÷��̾��� �Է� ����Ʈ�� �ʱ�ȭ�ϰ�
        CurResult1 = new List<string>();
        //2�� �Ŀ� �е� ������ ����
        Invoke("ResetColor", 2f);

    }

    private void IfCan()
    {
        StartCoroutine(ReporterTrueEndingCoroutine());
    }

    //UI OnClick �̺�Ʈ
    public void FirstBTN()
    {
        CurResult1.Add("H");
        mats[0].DisableKeyword("_EffectOn");
        mats[0].EnableKeyword("_EMISSION");
    }

    //UI OnClick �̺�Ʈ
    public void SecondBTN()
    {
        CurResult1.Add("A");
        mats[1].DisableKeyword("_EffectOn");
        mats[1].EnableKeyword("_EMISSION");
    }

    //UI OnClick �̺�Ʈ
    public void ThirdBTN()
    {

        CurResult1.Add("D");
        mats[2].DisableKeyword("_EffectOn");
        mats[2].EnableKeyword("_EMISSION");
    }

    //UI OnClick �̺�Ʈ
    public void FourthBTN()
    {

        CurResult1.Add("M");
        mats[3].DisableKeyword("_EffectOn");
        mats[3].EnableKeyword("_EMISSION");
    }
    //HADMTYLQS

    //UI OnClick �̺�Ʈ
    public void FifthBTN()
    {

        CurResult1.Add("T");
        mats[4].DisableKeyword("_EffectOn");
        mats[4].EnableKeyword("_EMISSION");
    }

    //UI OnClick �̺�Ʈ
    public void SixthBTN()
    {

        CurResult1.Add("Y");
        mats[5].DisableKeyword("_EffectOn");
        mats[5].EnableKeyword("_EMISSION");

    }

    //UI OnClick �̺�Ʈ
    public void SeventhBTN()
    {
        CurResult1.Add("L");
        mats[6].DisableKeyword("_EffectOn");
        mats[6].EnableKeyword("_EMISSION");
    }

    //UI OnClick �̺�Ʈ
    public void EighthBTN()
    {

        CurResult1.Add("Q");
        mats[7].DisableKeyword("_EffectOn");
        mats[7].EnableKeyword("_EMISSION");
    }

    //UI OnClick �̺�Ʈ
    public void NinthBTN()
    {

        CurResult1.Add("S");
        mats[8].DisableKeyword("_EffectOn");
        mats[8].EnableKeyword("_EMISSION");
    }

    //UI OnClick �̺�Ʈ
    private void ResetColor()
    {
        foreach(Material mat in mats)
        {
            mat.DisableKeyword("_EffectOn");
            mat.DisableKeyword("_EMISSION");
        }
    }

    //���� ���� �������� �����ϴ� �ڷ�ƾ
    private IEnumerator ReporterTrueEndingCoroutine()
    {
        //Ż�⹮ Ȱ��ȭ
        LastDoor.SetActive(true);

        //���� UI���� ��� ��Ȱ��ȭ
        foreach (Canvas can in canvases)
        {
            can.gameObject.SetActive(false);
        }

        //�� ������ ����Ͽ� SetActive ��������� ������ ����ǵ��� ����
        yield return null;

        //���̴� ȿ���� ���带 ����Ͽ� ����� ����
        lastDoorEffect.FireFadeIn();
        padPuzzleEffect.FireFadeOut();

        AudioClip Door = audioClips;
        GetComponent<AudioSource>().Stop();
        GetComponent<AudioSource>().PlayOneShot(Door, 0.8f);

        //���� �Ŀ��� �� ��ũ��Ʈ�� �� �̻� �۵��� �ʿ䰡 �����Ƿ� ��Ȱ��ȭ�Ͽ� ������ ����ȭ��
        this.enabled = false;
    }
}
