using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;


//실제로 돌리는 동작을 구현 크랭크의 누적 회전 수를 계산하여, 특정 횟수(예: 3바퀴)에 도달하면 기믹 완성하는 걸로 간주


public class CrankController : XRBaseInteractable
{

    //두 장치 위치
    public Transform LeftDeviceTr;
    public Transform RightDeviceTr;

    public XRGrabInteractable grab;

    //손잡이 위치
    public Transform originTr;

    //Door
    [SerializeField] private GameObject Door;
    //Fuse
    //[SerializeField] private GameObject Fuse;

    //두 컨트롤러의 위치를 저장하기 위한 변수
    [SerializeField] private Vector3 DeviceLTrs;
    [SerializeField] private Vector3 DeviceRTrs;

    //초기 각도
    private float OriginAngle = 0f;
    //현재 각도
    private float CurAngle = 0f;
    //총 각도
    private float TotalAngle = 0f;
    //총 바퀴 수
    private int curCnt = 0;

    //현재 장치의 이름
    private string DeviceName;
    [SerializeField] private DoorAnimation doorAni;
    
    
    private void Start()
    {
        //grab(XRGrabInteractable) 컴포넌트의 'selectEntered' 이벤트에 OnSelectEntered 메서드를 연결, 사용자가 오브젝트를 잡았을 때 OnSelectEntered가 자동으로 호출
        grab.selectEntered.AddListener(OnSelectEntered);
    }
    private void Update()
    {
        //잡고 있는 상태에서, 함수 실행
        if (grab.isSelected == true)
        {
            //컨트롤러가 크랭크 범위에서 벗어나는지 확인하는 함수
            TheRange();
            //컨트롤러 위치에 따라 크랭크를 회전시키는 함수
            TheRotate();
        }

        //3번 이하일 때만 호출
        if(curCnt < 3)
        {
            //누적 회전 수를 계산하는 함수
            TheRotateCnt();
        }
        //3번이면 성공으로 판정
        else if(curCnt == 3 )
        {
            //Debug.Log("3바퀴 돌렸다.");

            //문 활성화
            Door.SetActive(true);
            //문 애니메이션 활성화
            doorAni.SartDoorAnimation();
        }
    }

    //컨트롤러(장치)의 위치에 따라 크랭크 핸들의 각도를 실시간으로 변경
    private void TheRotate()
    {
        DeviceLTrs = LeftDeviceTr.transform.position;
        DeviceRTrs = RightDeviceTr.transform.position;
        Vector3 CrankDir = new Vector3();

        //장치 구분
        if(DeviceName == "Left Controller (UnityEngine.Transform)")
        {
            CrankDir = DeviceLTrs - transform.position;
        }
        else if(DeviceName == "Right Controller (UnityEngine.Transform)")
        {
            CrankDir = DeviceRTrs - transform.position;
        }

        //Atan2를 사용하여 X, Y 좌표로 각도(라디안)를 계산하고, Rad2Deg로 변환
        float z = Mathf.Atan2(CrankDir.y, CrankDir.x) * Mathf.Rad2Deg;
        //계산된 각도에 90도를 더해(오브젝트의 초기 회전값에 따라 보정하는 값이 90도) 크랭크의 Z축 회전값으로 적용
        transform.rotation = Quaternion.Euler(0f,0f,z + 90f);
    }


    private void TheRotateCnt()
    {
        CurAngle = transform.rotation.eulerAngles.z;

        //각도 차이를 계산
        float angle = OriginAngle - CurAngle;

        //크랭크가 반대 방향으로 돌 때 음수 값이 나오는 것을 방지하고, 한 방향으로만 카운트
        if (angle < 0)
        {
            angle = 0;
        }
        
        //누적 Z축 회전 각도 업데이트
        TotalAngle += angle;

        //전체 회전 바퀴 수 계산
        curCnt = Mathf.FloorToInt(TotalAngle / 360f);

        //현재 각도를 이전 각도로 업데이트
        OriginAngle = CurAngle;
    }

    //크랭크의 유효 범위(0.25f)를 벗어났는지 확인하고, 벗어났다면 잡기를 강제로 해제
    private void TheRange()
    {
        if (DeviceName == "Left Controller (UnityEngine.Transform)")
        {
            if ((LeftDeviceTr.position - originTr.position).magnitude >= 0.25f)
            {
                grab.enabled = false;
                grab.enabled = true;
            }
        }
        else if(DeviceName == "Right Controller (UnityEngine.Transform)")
        {
            if ((RightDeviceTr.position - originTr.position).magnitude >= 0.25f)
            {
                grab.enabled = false;
                grab.enabled = true;
            }
        }
    }

    //크랭크가 잡혔을 때(OnSelectEntered 이벤트 발생 시) 호출된다. 테스트용
    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        //XRBaseInteractable 기본 동작 유지
        base.OnSelectEntered(args); 
        //장치 구분할 수 있게 시각화
        DeviceName = args.interactorObject.transform.parent.ToString();
        Debug.Log("DeviceName : " + DeviceName);
        //Debug.Log($"Object grabbed by: {args.interactorObject.transform.parent}");
    }



}


