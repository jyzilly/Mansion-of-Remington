using UnityEngine;
using UnityEngine.Events;


//�� ���� �ſ� ��ư

public class MirrorInsideButton : MonoBehaviour
{

    public GameObject button;
    public UnityEvent onPress;
    public UnityEvent onRelease;
    private GameObject presser;
    private AudioSource sound;
    public bool isPressed;



    private void Start()
    {
        sound = GetComponent<AudioSource>();
        isPressed = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isPressed)
        {
            button.transform.localPosition = new Vector3(0f, -0.049f, 0f);
            //��ư�� ���� ������Ʈ�� ���
            presser = other.gameObject;

            //�ν����Ϳ��� ����� OnPress �̺�Ʈ ����
            onPress.Invoke();
            sound.Play();
            isPressed = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //������ ����
        if (other.gameObject == presser)
        {
            button.transform.localPosition = new Vector3(0f, 0f, 0f);

            //�ν����Ϳ��� ����� OnRelease �̺�Ʈ ����
            onRelease.Invoke();
            isPressed = false;
        }
    }


}
