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
        //중복 호출을 방지
        if (!isPressed)
        {
            button.transform.localPosition = new Vector3(3.196f, 0.4f, 3.486f);

            //버튼을 누른 오브젝트를 기억하고
            presser = other.gameObject;

            //인스펙터에 연결된 OnPress 이벤트들을 모두 실행
            onPress.Invoke();
            sound.Play();
            isPressed = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //스쳐 지나갈 때 버튼이 의도치 않게 올라오는 것을 방지
        if (other.gameObject == presser)
        {
            //원래 위치로
            button.transform.localPosition = new Vector3(3.196f, 0.449f, 3.486f);

            //인스펙터에 연결된 OnRelease 이벤트들을 모두 실행
            onRelease.Invoke();
            isPressed = false;
        }
    }

    //테스트용
    public void TheKeyAnswer()
    {
        //GameObject Sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        //Sphere.transform.localScale = new Vector3(0.5f,0.5f,0.5f);
        //Sphere.transform.localPosition = new Vector3(0, 1, 2);
        //Sphere.AddComponent<Rigidbody>();

        //InputTheKey.text += TheKey;


    }

}
