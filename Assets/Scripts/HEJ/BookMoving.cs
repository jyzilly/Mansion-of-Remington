using UnityEngine;

public class BookMoving : MonoBehaviour
{
    public Animator animator;
    public bookManager bookManager;


    private void Awake()
    {
        
    }

    private void Start()
    {
            bookManager.onAniamtionCallback = ()=>Invoke("OnAnimation",5f);
    }

    private void Update()
    {
       
        
    }

    private void OnAnimation()
    {
        animator.SetBool("IsOpen", true);
    }
}
