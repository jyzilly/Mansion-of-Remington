using UnityEngine;

public class Cubeparticle : MonoBehaviour
{
    //������ ������ ��ƼŬ ����
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

    //������ ������ �ִϸ��̼� ����
    public void StartAnimation()
    {
        animator.SetBool("Start", true);
    }
}
