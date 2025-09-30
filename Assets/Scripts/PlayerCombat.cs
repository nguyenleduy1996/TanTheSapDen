using UnityEngine;
using UnityEngine.InputSystem; // new Input System

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerCombat : MonoBehaviour
{

    public Animator anim;

    public float colldown = 2;
    private float timer;

    public void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }       
    }
    public void Attack()
    {
        if (timer <= 0)
        {
            anim.SetBool("IsAttacking", true);
            timer = colldown;
        }
    }
     public void End_Attack()
    {
        anim.SetBool("IsAttacking", false);
    }
}

