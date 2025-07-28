using System.Collections;
using UnityEngine;

//������ �� ���
//���� ���� ���� ����(�±׷� ����)�� Ư�� Ʈ���� ������ ��� ���Դ��� Ȯ��
//��� ������ �����Ǹ�, FireBurnOutShading ���̴� ȿ���� ����Ͽ� ��� ��ȯ�� ������ ���� ���� �ڷ�ƾ�� ����

public class CheckTheBoyLastDoor : MonoBehaviour
{
    [Header("Doors Settings")]
    //3�� ���� ����
    [SerializeField] private GameObject DoorUp;
    [SerializeField] private GameObject DoorMiddle;
    [SerializeField] private GameObject DoorDown;

    [Header("DoorPictures Settings")]
    //3�� ���� ����
    [SerializeField] private GameObject DoorPicture1;
    [SerializeField] private GameObject DoorPicture2;
    [SerializeField] private GameObject DoorPicture3;

    //3�� ���� ������ �� ������ ��
    [SerializeField] private GameObject RealDoor;

    //ȿ����
    private FireBurnOutShading realDoorEffect;
    private FireBurnOutShading canvasEffect;
    public FireBurnOutShadingUI[] canvasUIEffects;
    

    [SerializeField]//������
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
        //���� 3�� �����ϸ� ��Ϳϼ��ϴ� �ɷ� �����ؼ�, ȿ������, �� �� �����ϰ� �ٽ� �����ϴ� �� �����ϴ� bool���� �߰�
        if(CurIdx == 3 && !Once)
        {
            TrueEndingEffect();
            Once = true;
        }
    }

    //�����ؼ� ���� ������Ȱ���� �ϰ� �� ���� Ȱ��ȭ �Ѵ�.
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

    //������ ȿ�� �Լ�
    private void TrueEndingEffect()
    {
        StartCoroutine(TrueEndingEffectCoroutine());
    }
    private IEnumerator TrueEndingEffectCoroutine()
    {
        RealDoor.SetActive(true);
        yield return null;
        //��ü ȭ���� ���̵� �ƿ�(��ο� ����) �Ѵ�.
        canvasEffect.FireFadeOut();
        //���� ���� ���̵� ��(���� ����) ��Ÿ����.
        realDoorEffect.FireFadeIn();

        //��� UI ��ҵ��� ���̵� �ƿ�
        foreach (FireBurnOutShadingUI fUI in canvasUIEffects)
        {
            fUI.FireFadeOut();
        }
    }
}
