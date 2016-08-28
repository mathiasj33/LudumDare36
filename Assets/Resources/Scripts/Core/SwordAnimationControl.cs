using UnityEngine;
using System.Collections;

public class SwordAnimationControl : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void PlayAnim()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Kill")) return;
        animator.SetTrigger("Kill");
    }
}
