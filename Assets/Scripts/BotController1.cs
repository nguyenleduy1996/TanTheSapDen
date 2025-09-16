using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class BotController : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Rigidbody2D rb;
    private Animator animator;
    private bool isHurting = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f;
        rb.freezeRotation = true;
        animator = GetComponent<Animator>();
    }

    // Gọi hàm này khi Bot bị tấn công, truyền hướng tấn công (attackDir)
    public void PlayHurt(Vector2 attackDir)
    {
        if (isHurting)
            return;
        isHurting = true;
        string hurtAnim = "Swordsman_lvl1_Hurt_front_Clip";
        if (attackDir.x < -0.5f)
            hurtAnim = "Swordsman_lvl1_Hurt_side_left_Clip";
        else if (attackDir.x > 0.5f)
            hurtAnim = "Swordsman_lvl1_Hurt_side_right_Clip";
        else if (attackDir.y > 0.5f)
            hurtAnim = "Swordsman_lvl1_Hurt_back_Clip";
        else if (attackDir.y < -0.5f)
            hurtAnim = "Swordsman_lvl1_Hurt_front_Clip";
        animator.Play(hurtAnim);
    }

    // Gọi từ animation event cuối clip Hurt
    public void EndHurt()
    {
        isHurting = false;
    }
}
