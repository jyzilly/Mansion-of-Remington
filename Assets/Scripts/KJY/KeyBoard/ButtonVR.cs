using TMPro;
using UnityEngine;
using UnityEngine.Events;


public class ButtonVR : MonoBehaviour
{

    public GameObject button;
    public UnityEvent onPress;
    public UnityEvent onRelease;
    private GameObject presser;
    private AudioSource sound;
    private bool isPressed;

    private string[] InputAnswers;


    private void Start()
    {
        sound = GetComponent<AudioSource>();
        isPressed = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        //�ߺ� ȣ���� ����
        if (!isPressed)
        {
            button.transform.localPosition = new Vector3(3.196f, 0.4f, 3.486f);

            //��ư�� ���� ������Ʈ�� ����ϰ�
            presser = other.gameObject;

            //�ν����Ϳ� ����� OnPress �̺�Ʈ���� ��� ����
            onPress.Invoke();
            sound.Play();
            isPressed = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //���� ������ �� ��ư�� �ǵ�ġ �ʰ� �ö���� ���� ����
        if (other.gameObject == presser)
        {
            //���� ��ġ��
            button.transform.localPosition = new Vector3(3.196f, 0.449f, 3.486f);

            //�ν����Ϳ� ����� OnRelease �̺�Ʈ���� ��� ����
            onRelease.Invoke();
            isPressed = false;
        }
    }

    //�׽�Ʈ��
    public void TheKeyAnswer()
    {
        //GameObject Sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        //Sphere.transform.localScale = new Vector3(0.5f,0.5f,0.5f);
        //Sphere.transform.localPosition = new Vector3(0, 1, 2);
        //Sphere.AddComponent<Rigidbody>();

        //InputTheKey.text += TheKey;


    }

}
