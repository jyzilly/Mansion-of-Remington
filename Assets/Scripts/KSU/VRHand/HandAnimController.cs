using UnityEngine;
using UnityEngine.InputSystem;

public class HandAnimController : MonoBehaviour
{
    public InputActionReference gripInputActioinReference;
    public InputActionReference triggerInputActionReference;

    private Animator handAnimator;
    private float gripValue;
    private float triggerValue;

    private void Start()
    {
        handAnimator = GetComponent<Animator>();
    }

    private void Update()
    {
        AnimateGrip();
        AnimateTrigger();
    }

    private void AnimateGrip()
    {
        gripValue = gripInputActioinReference.action.ReadValue<float>();
        handAnimator.SetFloat("Grip", gripValue);
    }

    private void AnimateTrigger()
    {
        triggerValue = triggerInputActionReference.action.ReadValue<float>();
        handAnimator.SetFloat("Trigger", triggerValue);
    }
}
