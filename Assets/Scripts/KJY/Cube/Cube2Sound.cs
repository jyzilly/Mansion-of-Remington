using UnityEngine;

//순서 맞추기 사운드 퍼즐

public class Cube2Sound : MonoBehaviour
{
    //동물 사운드
    public AudioClip DogSound;
    public AudioClip MouseSound;
    public AudioClip RabbitSound;
    public AudioClip MonkeySound;
    public AudioClip AnimalPlay;

    //성공 & 실패 효과음
    public AudioClip success;
    public AudioClip failure; 

    //정답순서
    public string[] Result = { "dog", "monkey", "dog", "mouse", "rabbit" };
    private string[] InputResult = new string[5];


    private GameObject[] Lights = new GameObject[5];
    [SerializeField] private Transform[] LightsPos;
    [SerializeField] private GameObject GreenLight;

   
    private int curNum = 0;
    private int correctNum = 0;

    //결과값
    public bool TheCube2Result = false;


    private void Update()
    {
        //테스트용
        //if (Input.GetKeyDown(KeyCode.A))
        //{
        //    ClickDog();
        //}

        //5개 입력이 들어오면 결과체크 시작
        if (curNum == 5)
        {
            CheckTheResult();
            curNum = 0;
        }

    }

    //개 이미지 클릭했을때 - OnClick 이벤트 사용해서
    public void ClickDog()
    {
        //Debug.Log("개 클릭됨");

        //불빛 추가
        AddLight();

        //배열에 해당동물 추가
        AddToArr("dog");

        //사운드 재생
        AudioClip AnimalSound = DogSound;
        GetComponent<AudioSource>().Stop();
        GetComponent<AudioSource>().PlayOneShot(AnimalSound, 0.8f);

        //현재 num추가
        curNum++;
    }

    //토끼 이미지 클릭했을때
    public void ClickRabbit()
    {
        //Debug.Log("토끼 클릭됨");

        //불빛 추가
        AddLight();

        //배열에 해당동물 추가
        AddToArr("rabbit");

        //사운드 재생
        AudioClip AnimalSound = RabbitSound;
        GetComponent<AudioSource>().Stop();
        GetComponent<AudioSource>().PlayOneShot(AnimalSound, 0.8f);

        //현재 num추가
        curNum++;
    }

    //플레이 버튼 클릭했을때 전체 사운드 출력
    public void ClickPlay()
    {
        AudioClip AnimalPlaySound = AnimalPlay;
        GetComponent<AudioSource>().Stop();
        GetComponent<AudioSource>().PlayOneShot(AnimalPlaySound, 0.8f);
    }

    //쥐 클릭을 콜백받았을때
    public void CallbackMouse()
    {
        //해당 불 출력
        AddLight();
        //배열에 맞는 동물 추가
        AddToArr("mouse");

        AudioClip AnimalSound = MouseSound;
        GetComponent<AudioSource>().Stop();
        GetComponent<AudioSource>().PlayOneShot(AnimalSound, 0.8f);

        curNum++;

    }

    //몽키 클릭을 콜백 받았을때
    public void CallbackMonkey()
    {
        //해당 불 출력
        AddLight();
        //배열에 맞는 동물 추가
        AddToArr("monkey");

        AudioClip AnimalSound = MonkeySound;
        GetComponent<AudioSource>().Stop();
        GetComponent<AudioSource>().PlayOneShot(AnimalSound, 0.8f);

        curNum++;
    }

    //입력받은 동물 배열에 추가
    private void AddToArr(string _animal)
    {
        for (int i = 0; i < InputResult.Length; ++i)
        {
            if (InputResult[i] == null)
            {
                //빈 칸에 값을 넣기
                InputResult[i] = _animal;
                //Debug.Log($"{_animal}이(가) 배열의 {i}번 칸에 추가되었습니다.");
                return;
            }
        }
    }

    //불빛 생성
    private void AddLight()
    {

        if (curNum < Lights.Length && Lights[curNum] == null)
        {
            Lights[curNum] = Instantiate(GreenLight, LightsPos[curNum].position, Quaternion.Euler(0f, 90f, 0f), transform);

        }
    }

    //결과를 확인
    private void CheckTheResult()
    {
        for (int i = 0; i < Result.Length; ++i)
        {
            if (Result[i] == InputResult[i])
            {
                correctNum++;
            }
        }

        Debug.Log(correctNum);

        if (correctNum == 5)
        {
            //성공 시 처리
            TheCube2Result = true;
            AudioClip SuccessSound = success;
            GetComponent<AudioSource>().Stop();
            GetComponent<AudioSource>().PlayOneShot(SuccessSound, 0.8f);
        }
        else
        {
            //실패 시 처리
            AudioClip FailureSound = failure;
            GetComponent<AudioSource>().Stop();
            GetComponent<AudioSource>().PlayOneShot(FailureSound, 0.8f);
            Debug.Log("실패");

            Destroy(Lights[0]);
            Destroy(Lights[1]);
            Destroy(Lights[2]);
            Destroy(Lights[3]);
            Destroy(Lights[4]);

            correctNum = 0;
            InputResult = new string[5];
        }
    }
}
