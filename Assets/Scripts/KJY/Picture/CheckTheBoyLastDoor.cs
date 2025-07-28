using System.Collections;
using UnityEngine;

//마지막 문 기믹
//여러 개의 퍼즐 조각(태그로 구분)이 특정 트리거 영역에 모두 들어왔는지 확인
//모든 조건이 충족되면, FireBurnOutShading 셰이더 효과를 사용하여 장면 전환을 포함한 엔딩 연출 코루틴을 실행

public class CheckTheBoyLastDoor : MonoBehaviour
{
    [Header("Doors Settings")]
    //3개 문의 조각
    [SerializeField] private GameObject DoorUp;
    [SerializeField] private GameObject DoorMiddle;
    [SerializeField] private GameObject DoorDown;

    [Header("DoorPictures Settings")]
    //3개 문의 사진
    [SerializeField] private GameObject DoorPicture1;
    [SerializeField] private GameObject DoorPicture2;
    [SerializeField] private GameObject DoorPicture3;

    //3개 조각 만족할 때 완전한 문
    [SerializeField] private GameObject RealDoor;

    //효과들
    private FireBurnOutShading realDoorEffect;
    private FireBurnOutShading canvasEffect;
    public FireBurnOutShadingUI[] canvasUIEffects;
    

    [SerializeField]//디버깅용
    private int CurIdx = 0;
    private bool Once = false;

    private void Awake()
    {
        RealDoor.SetActive(true);
        realDoorEffect = RealDoor.GetComponent<FireBurnOutShading>();
        canvasEffect = GetComponent<FireBurnOutShading>();
    }
    private void Start()
    {
        RealDoor.SetActive(false);
    }
    private void Update()
    {
        //조각 3개 만족하면 기믹완성하는 걸로 간주해서, 효과실행, 한 번 실행하고 다시 실행하는 걸 방지하는 bool변수 추가
        if(CurIdx == 3 && !Once)
        {
            TrueEndingEffect();
            Once = true;
        }
    }

    //구번해서 각각 사진비활성로 하고 문 조각 활성화 한다.
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Door1")
        {
            DoorUp.SetActive(true);
            DoorPicture1.SetActive(false);
            ++CurIdx;
        }
        if(other.gameObject.tag == "Door2")
        {
            DoorMiddle.SetActive(true);
            DoorPicture2.SetActive(false);
            ++CurIdx;
        }
        if(other.gameObject.tag == "Door3")
        {
            DoorDown.SetActive(true);
            DoorPicture3.SetActive(false);
            ++CurIdx;
        }
    }

    //진엔딩 효과 함수
    private void TrueEndingEffect()
    {
        StartCoroutine(TrueEndingEffectCoroutine());
    }
    private IEnumerator TrueEndingEffectCoroutine()
    {
        RealDoor.SetActive(true);
        yield return null;
        //전체 화면을 페이드 아웃(어두운 느낌) 한다.
        canvasEffect.FireFadeOut();
        //최종 문이 페이드 인(밝은 느낌) 나타난다.
        realDoorEffect.FireFadeIn();

        //모든 UI 요소들을 페이드 아웃
        foreach (FireBurnOutShadingUI fUI in canvasUIEffects)
        {
            fUI.FireFadeOut();
        }
    }
}
