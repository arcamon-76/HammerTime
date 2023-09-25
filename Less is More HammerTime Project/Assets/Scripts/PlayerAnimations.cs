using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    public Animator anim;
    public void Walk()
    {
        anim.Play("Walking");
    }
    public void HammerUp()
    {
        anim.Play("Hammering");
    }
    public void HammerDown()
    {
        anim.Play("Hammering Down");
    }
    public void Idle()
    {
        anim.Play("Idle");
    }
    public void Death() 
    {
        anim.Play("Player Death");
    }
}
