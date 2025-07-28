using UnityEngine;

public class DoorAnimation : MonoBehaviour
{
    public Animator animator;

    public void SartDoorAnimation()
    {
        animator.SetBool("Door",true);
    }

}
