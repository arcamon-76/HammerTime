using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackScreenManager : MonoBehaviour
{
    [SerializeField] Animator animator;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void Start()
    {
        animator.Play("ShadeToWhite");
    }
    public void FadeToBlack() 
    {
        animator.Play("ShadeToBlack");
    }
    public void FadeToWhite() 
    {
        animator.Play("ShadeToWhite");
    }


}
