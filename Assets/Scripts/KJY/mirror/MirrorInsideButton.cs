using UnityEngine;
using UnityEngine.Events;


//벽 안쪽 거울 버튼

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
            //버튼을 누른 오브젝트를 기억
            presser = other.gameObject;

            //인스펙터에서 연결된 OnPress 이벤트 실행
            onPress.Invoke();
            sound.Play();
            isPressed = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //오동작 방지
        if (other.gameObject == presser)
        {
            button.transform.localPosition = new Vector3(0f, 0f, 0f);

            //인스펙터에서 연결된 OnRelease 이벤트 실행
            onRelease.Invoke();
            isPressed = false;
        }
    }


}
