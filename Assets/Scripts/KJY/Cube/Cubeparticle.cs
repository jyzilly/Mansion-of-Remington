using UnityEngine;

public class Cubeparticle : MonoBehaviour
{
    //마지막 성공시 파티클 실행
    public ParticleSystem particle;

    private Animator animator;

    private void Start()
    {
        particle.Stop();
        animator = this.GetComponent<Animator>();
    }

    private void congration()
    {
        particle.Play();

    }

    //마지막 성공시 애니메이션 실행
    public void StartAnimation()
    {
        animator.SetBool("Start", true);
    }
}
