using UnityEngine;
using UnityEngine.Video;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UIElements;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class Tape : MonoBehaviour
{
    Animator animator;

    public delegate void OnVideoClickDelegate(Tape tapes);

    private OnVideoClickDelegate onVideoClickCallback = null;
    private Collider tapeCollider = null;
    public Vector3 startPoint;

    [SerializeField] private int tapeNum;

    public int TapeNum { get { return tapeNum; } }


    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        //startPoint = GetComponentInChildren<Transform>();
        
    }

    public OnVideoClickDelegate OnVideoClickCallback
    {
        set { onVideoClickCallback = value; }
    }

   

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.name == "DVDPlayer")
        {
            transform.gameObject.GetComponent<XRGrabInteractable>().enabled = false;
            this.transform.position = startPoint;
            animator.SetBool("IsTouch", true);
            //animator.enabled = true;
            //tapeCollider.enabled = false;

            Invoke("IsFinish", animator.GetCurrentAnimatorStateInfo(0).length);
        }
    }

    public void IsFinish()
    {
        onVideoClickCallback?.Invoke(this);
    }
}
