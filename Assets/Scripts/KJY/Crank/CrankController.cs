using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;


//������ ������ ������ ���� ũ��ũ�� ���� ȸ�� ���� ����Ͽ�, Ư�� Ƚ��(��: 3����)�� �����ϸ� ��� �ϼ��ϴ� �ɷ� ����


public class CrankController : XRBaseInteractable
{

    //�� ��ġ ��ġ
    public Transform LeftDeviceTr;
    public Transform RightDeviceTr;

    public XRGrabInteractable grab;

    //������ ��ġ
    public Transform originTr;

    //Door
    [SerializeField] private GameObject Door;
    //Fuse
    //[SerializeField] private GameObject Fuse;

    //�� ��Ʈ�ѷ��� ��ġ�� �����ϱ� ���� ����
    [SerializeField] private Vector3 DeviceLTrs;
    [SerializeField] private Vector3 DeviceRTrs;

    //�ʱ� ����
    private float OriginAngle = 0f;
    //���� ����
    private float CurAngle = 0f;
    //�� ����
    private float TotalAngle = 0f;
    //�� ���� ��
    private int curCnt = 0;

    //���� ��ġ�� �̸�
    private string DeviceName;
    [SerializeField] private DoorAnimation doorAni;
    
    
    private void Start()
    {
        //grab(XRGrabInteractable) ������Ʈ�� 'selectEntered' �̺�Ʈ�� OnSelectEntered �޼��带 ����, ����ڰ� ������Ʈ�� ����� �� OnSelectEntered�� �ڵ����� ȣ��
        grab.selectEntered.AddListener(OnSelectEntered);
    }
    private void Update()
    {
        //��� �ִ� ���¿���, �Լ� ����
        if (grab.isSelected == true)
        {
            //��Ʈ�ѷ��� ũ��ũ �������� ������� Ȯ���ϴ� �Լ�
            TheRange();
            //��Ʈ�ѷ� ��ġ�� ���� ũ��ũ�� ȸ����Ű�� �Լ�
            TheRotate();
        }

        //3�� ������ ���� ȣ��
        if(curCnt < 3)
        {
            //���� ȸ�� ���� ����ϴ� �Լ�
            TheRotateCnt();
        }
        //3���̸� �������� ����
        else if(curCnt == 3 )
        {
            //Debug.Log("3���� ���ȴ�.");

            //�� Ȱ��ȭ
            Door.SetActive(true);
            //�� �ִϸ��̼� Ȱ��ȭ
            doorAni.SartDoorAnimation();
        }
    }

    //��Ʈ�ѷ�(��ġ)�� ��ġ�� ���� ũ��ũ �ڵ��� ������ �ǽð����� ����
    private void TheRotate()
    {
        DeviceLTrs = LeftDeviceTr.transform.position;
        DeviceRTrs = RightDeviceTr.transform.position;
        Vector3 CrankDir = new Vector3();

        //��ġ ����
        if(DeviceName == "Left Controller (UnityEngine.Transform)")
        {
            CrankDir = DeviceLTrs - transform.position;
        }
        else if(DeviceName == "Right Controller (UnityEngine.Transform)")
        {
            CrankDir = DeviceRTrs - transform.position;
        }

        //Atan2�� ����Ͽ� X, Y ��ǥ�� ����(����)�� ����ϰ�, Rad2Deg�� ��ȯ
        float z = Mathf.Atan2(CrankDir.y, CrankDir.x) * Mathf.Rad2Deg;
        //���� ������ 90���� ����(������Ʈ�� �ʱ� ȸ������ ���� �����ϴ� ���� 90��) ũ��ũ�� Z�� ȸ�������� ����
        transform.rotation = Quaternion.Euler(0f,0f,z + 90f);
    }


    private void TheRotateCnt()
    {
        CurAngle = transform.rotation.eulerAngles.z;

        //���� ���̸� ���
        float angle = OriginAngle - CurAngle;

        //ũ��ũ�� �ݴ� �������� �� �� ���� ���� ������ ���� �����ϰ�, �� �������θ� ī��Ʈ
        if (angle < 0)
        {
            angle = 0;
        }
        
        //���� Z�� ȸ�� ���� ������Ʈ
        TotalAngle += angle;

        //��ü ȸ�� ���� �� ���
        curCnt = Mathf.FloorToInt(TotalAngle / 360f);

        //���� ������ ���� ������ ������Ʈ
        OriginAngle = CurAngle;
    }

    //ũ��ũ�� ��ȿ ����(0.25f)�� ������� Ȯ���ϰ�, ����ٸ� ��⸦ ������ ����
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

    //ũ��ũ�� ������ ��(OnSelectEntered �̺�Ʈ �߻� ��) ȣ��ȴ�. �׽�Ʈ��
    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        //XRBaseInteractable �⺻ ���� ����
        base.OnSelectEntered(args); 
        //��ġ ������ �� �ְ� �ð�ȭ
        DeviceName = args.interactorObject.transform.parent.ToString();
        Debug.Log("DeviceName : " + DeviceName);
        //Debug.Log($"Object grabbed by: {args.interactorObject.transform.parent}");
    }



}


