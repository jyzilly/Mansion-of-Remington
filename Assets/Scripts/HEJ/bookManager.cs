using UnityEngine;

public class bookManager : MonoBehaviour
{
    public bool isSuccess;
    private BookCheckPoint[] bookCheckPoints = null;

    public delegate void OnAnimationDelegate();
    public OnAnimationDelegate onAniamtionCallback = null;

    private void Awake()
    {
        bookCheckPoints = GetComponentsInChildren<BookCheckPoint>();
       
    }

    private void Start()
    {
        foreach (BookCheckPoint bookCheckPoint in bookCheckPoints)
            bookCheckPoint.onCheckedCallback += OnCheckedCallback;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            onAniamtionCallback?.Invoke();
        }
    }

    private void OnCheckedCallback()
    {
        Debug.Log("°á°ú ®G!");

        isSuccess = true;
        foreach (BookCheckPoint bookCheckPoint in bookCheckPoints)
        {
            if (bookCheckPoint.isChecked == false)
            {
                isSuccess = false;
                break;
            }
        }

        Debug.Log("°á°ú ®G!" + isSuccess);

        if (isSuccess)
        {
            // ¼º°ø
           // Debug.Log("¼º°ø");
            //Invoke("MovigBookSelf", 2f);
            onAniamtionCallback?.Invoke();
            Destroy(this.gameObject, 10f);

        }
        else
        {
            // ½ÇÆÐ
        }
    }
}
