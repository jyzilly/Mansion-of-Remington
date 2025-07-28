using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;



//플레이어가 9개의 패드 중 올바른 순서("L", "A", "S", "T")로 4개를 누르는 퍼즐

public class PadButtonController : MonoBehaviour
{
    [Header("Default Settings")]
    [SerializeField] private GameObject LastDoor;
    [SerializeField] private GameObject PadPuzzle;
    [SerializeField] private AudioClip audioClips;

    //셰이더 효과
    private FireBurnOutShading lastDoorEffect;
    private FireBurnOutShadingChain padPuzzleEffect;
    //비활성화할 UI 캔버스들
    private Canvas[] canvases;

    //디버그
    public bool debugST;

    //정답순서
    List<string> FinalResult = new List<string> { "L", "A", "S", "T" };
    //현재입력 순서 저장하는 리스트
    [SerializeField] private List<string> CurResult1 = new List<string>();

    //최종결과
    public bool TheResult = false;

    //각 패드 머티리얼 배열 -> 9개
    public Material[] mats;

    private void Awake()
    {
        lastDoorEffect = LastDoor.GetComponent<FireBurnOutShading>();
        padPuzzleEffect = PadPuzzle.GetComponent<FireBurnOutShadingChain>();
        canvases = GetComponentsInChildren<Canvas>();
    }
    private void Start()
    {
        //초기상태
        ResetColor();
        
    }


    private void Update()
    {
        //입력이 4개가 되는 순간 CheckTheResult()를 4번 호출하게 됨

        if (CurResult1.Count == 4)
        {
            foreach (string Cur1 in CurResult1)
            {
                //Debug.Log(Cur1);
                CheckTheResult();
            }
        }

        //디버그용 -> 강제 성공
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
            //성공 시퀀스 호출
            IfCan();
            //Debug.Log("성공");
            //문열리는 소리 나오고 문이 조금 열린다. -> 엔딩
        }

        //성공 여부와 관계없이, 입력이 끝나면 플레이어의 입력 리스트를 초기화하고
        CurResult1 = new List<string>();
        //2초 후에 패드 색상을 리셋
        Invoke("ResetColor", 2f);

    }

    private void IfCan()
    {
        StartCoroutine(ReporterTrueEndingCoroutine());
    }

    //UI OnClick 이벤트
    public void FirstBTN()
    {
        CurResult1.Add("H");
        mats[0].DisableKeyword("_EffectOn");
        mats[0].EnableKeyword("_EMISSION");
    }

    //UI OnClick 이벤트
    public void SecondBTN()
    {
        CurResult1.Add("A");
        mats[1].DisableKeyword("_EffectOn");
        mats[1].EnableKeyword("_EMISSION");
    }

    //UI OnClick 이벤트
    public void ThirdBTN()
    {

        CurResult1.Add("D");
        mats[2].DisableKeyword("_EffectOn");
        mats[2].EnableKeyword("_EMISSION");
    }

    //UI OnClick 이벤트
    public void FourthBTN()
    {

        CurResult1.Add("M");
        mats[3].DisableKeyword("_EffectOn");
        mats[3].EnableKeyword("_EMISSION");
    }
    //HADMTYLQS

    //UI OnClick 이벤트
    public void FifthBTN()
    {

        CurResult1.Add("T");
        mats[4].DisableKeyword("_EffectOn");
        mats[4].EnableKeyword("_EMISSION");
    }

    //UI OnClick 이벤트
    public void SixthBTN()
    {

        CurResult1.Add("Y");
        mats[5].DisableKeyword("_EffectOn");
        mats[5].EnableKeyword("_EMISSION");

    }

    //UI OnClick 이벤트
    public void SeventhBTN()
    {
        CurResult1.Add("L");
        mats[6].DisableKeyword("_EffectOn");
        mats[6].EnableKeyword("_EMISSION");
    }

    //UI OnClick 이벤트
    public void EighthBTN()
    {

        CurResult1.Add("Q");
        mats[7].DisableKeyword("_EffectOn");
        mats[7].EnableKeyword("_EMISSION");
    }

    //UI OnClick 이벤트
    public void NinthBTN()
    {

        CurResult1.Add("S");
        mats[8].DisableKeyword("_EffectOn");
        mats[8].EnableKeyword("_EMISSION");
    }

    //UI OnClick 이벤트
    private void ResetColor()
    {
        foreach(Material mat in mats)
        {
            mat.DisableKeyword("_EffectOn");
            mat.DisableKeyword("_EMISSION");
        }
    }

    //최종 엔딩 시퀀스를 연출하는 코루틴
    private IEnumerator ReporterTrueEndingCoroutine()
    {
        //탈출문 활성화
        LastDoor.SetActive(true);

        //퍼즐 UI들을 모두 비활성화
        foreach (Canvas can in canvases)
        {
            can.gameObject.SetActive(false);
        }

        //한 프레임 대기하여 SetActive 변경사항이 완전히 적용되도록 보장
        yield return null;

        //셰이더 효과와 사운드를 재생하여 장면을 연출
        lastDoorEffect.FireFadeIn();
        padPuzzleEffect.FireFadeOut();

        AudioClip Door = audioClips;
        GetComponent<AudioSource>().Stop();
        GetComponent<AudioSource>().PlayOneShot(Door, 0.8f);

        //성공 후에는 이 스크립트가 더 이상 작동할 필요가 없으므로 비활성화하여 성능을 최적화함
        this.enabled = false;
    }
}
