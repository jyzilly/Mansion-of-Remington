using System.Threading;
using System.Timers;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class ShakeFunction : MonoBehaviour
{
    //��鸱 3D ������Ʈ
    public Transform objectToShake;

    [SerializeField] private XRGrabInteractable grab;

    //������ ������Ʈ �����յ�
    public GameObject[] toys;
    private int curnum = 0;
    

    //��鸲 ���� �Ӱ谪
    public float shakeThreshold = 1.0f;
    //��鸲 ���� �ð�
    public float shakeDuration = 1f; 
    //��� ����ȭ �ð� ��� ����
    public float syncThreshold = 0.2f; 
    
    //������ ��ġ �׼�
    public InputActionProperty rightHandPositionAction; 
    //�޼� ��ġ �׼�
    public InputActionProperty leftHandPositionAction;  

    //�������� ���� ��ġ
    private Vector3 lastRightPosition; 
    //�޼��� ���� ��ġ
    private Vector3 lastLeftPosition;  
    private float shakeTimer = 0f;

    private float TheTime = 0f;
    private float timeLimit = 0.5f;

    private bool Once = false;

    void Start()
    {
        lastRightPosition = Vector3.zero;
        lastLeftPosition = Vector3.zero;
    }

    void Update()
    {
        //������Ʈ�� ���� Up ���͸� ���� �������� ��ȯ
        Vector3 worldUp = transform.up;

        //worldUp ���Ͱ� -Y ����(�Ʒ���)�� �󸶳� ��ġ�ϴ��� ���
        float dotProduct = Vector3.Dot(worldUp, Vector3.down);

        if (curnum == toys.Length && !Once)
        {
            transform.gameObject.SetActive(false);
            Once = true;
        }

        if (curnum == toys.Length) return;

        if(grab.isSelected)
        {
            //transform.rotation = Quaternion.Euler(0f,0f,0f);
        }
        else
        {
            TheTime = 0f;
        }

        //��ġ ������ ��������
        Vector3 rightPosition = rightHandPositionAction.action.ReadValue<Vector3>();
        Vector3 leftPosition = leftHandPositionAction.action.ReadValue<Vector3>();

        //�ӵ� ���
        Vector3 rightVelocity = (rightPosition - lastRightPosition) / Time.deltaTime;
        Vector3 leftVelocity = (leftPosition - lastLeftPosition) / Time.deltaTime;

        //��� ��鸲 ����ȭ Ȯ��
        if (rightVelocity.magnitude > shakeThreshold && leftVelocity.magnitude > shakeThreshold && dotProduct <= -0.9f)
        {
            TheTime += Time.deltaTime;

            //�ӵ��� �ð� �� Ȯ��
            float timeDifference = Mathf.Abs(rightVelocity.magnitude - leftVelocity.magnitude);

            Debug.Log("timeDi :" + timeDifference);
            Debug.Log("sysmThre : " + syncThreshold);

            if(TheTime >= timeLimit)
            {
                if (timeDifference <= syncThreshold)
                {
                    shakeTimer = shakeDuration;
                    SetActive();
                    //MakePrefabs();
                    TheTime = 0f;
                }

            }

        }

        //���� ��ġ ������Ʈ
        lastRightPosition = rightPosition;
        lastLeftPosition = leftPosition;

        //��鸲 �ִϸ��̼�
        if (shakeTimer > 0)
        {
            ShakeObject();
            shakeTimer -= Time.deltaTime;
        }
    }

    void ShakeObject()
    {
        if (objectToShake != null)
        {
            //��鸲 ũ��
            float shakeAmount = Mathf.Sin(Time.time * 20) * 0.1f; 
            objectToShake.localPosition = new Vector3(shakeAmount, 0, 0);
        }
    }

    void SetActive()
    {
        //�峲�� ������� Ȱ��ȭ ��Ŵ
        toys[curnum].SetActive(true);
        toys[curnum].transform.parent = null;
        curnum++;
    }


}

