using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class BotController : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Rigidbody2D rb;
    private Animator animator;
    private bool isHurting = false;
    private Vector2 moveInput;
    private Vector2 lastMoveDir = Vector2.down;

    // Tham chiếu tới player
    public Transform player;
    public float attackRange = 1.2f;
    public float detectRange = 5f;
    private bool isAttacking = false;
    private float attackCooldown = 0.5f; // giây
    private float attackTimer = 0f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f;
        rb.freezeRotation = true;
        animator = GetComponent<Animator>();

        // Khi bắt đầu game, cho bot đứng Idle đúng hướng
        string idleAnim = "Swordsman_lvl1_Idle_front_Clip";
        if (lastMoveDir.x < -0.5f)
            idleAnim = "Swordsman_lvl1_Idle_side_left_Clip";
        else if (lastMoveDir.x > 0.5f)
            idleAnim = "Swordsman_lvl1_Idle_side_right_Clip";
        else if (lastMoveDir.y > 0.5f)
            idleAnim = "Swordsman_lvl1_Idle_back_Clip";
        else if (lastMoveDir.y < -0.5f)
            idleAnim = "Swordsman_lvl1_Idle_front_Clip";
        animator.Play(idleAnim);
    }

    // Gọi hàm này khi Bot bị tấn công, truyền hướng tấn công (attackDir)
    public void PlayHurt(Vector2 attackDir)
    {
        if (isHurting)
            return;
        isHurting = true;
        lastMoveDir = attackDir;
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
        // Chuyển về Idle đúng hướng
        string idleAnim = "Swordsman_lvl1_Idle_front_Clip";
        if (lastMoveDir.x < -0.5f)
            idleAnim = "Swordsman_lvl1_Idle_side_left_Clip";
        else if (lastMoveDir.x > 0.5f)
            idleAnim = "Swordsman_lvl1_Idle_side_right_Clip";
        else if (lastMoveDir.y > 0.5f)
            idleAnim = "Swordsman_lvl1_Idle_back_Clip";
        else if (lastMoveDir.y < -0.5f)
            idleAnim = "Swordsman_lvl1_Idle_front_Clip";
        animator.Play(idleAnim);
    }
    // Gọi từ animation event cuối clip attack
    public void EndAttack()
    {
        isAttacking = false;
        attackTimer = 0f;
    }

    // Hàm FixedUpdate cho Bot
    private void FixedUpdate()
    {
        if (isHurting)
        {
            return;
        }

        // Cập nhật cooldown tấn công
        if (isAttacking)
        {
            attackTimer += Time.fixedDeltaTime;
        }

        if (player != null)
        {
            Vector2 dirToPlayer = (player.position - transform.position);
            float distance = dirToPlayer.magnitude;
            if (distance <= detectRange)
            {
                if (distance > attackRange)
                {
                    moveInput = dirToPlayer.normalized;
                    lastMoveDir = moveInput;
                    rb.MovePosition(rb.position + moveInput * moveSpeed * Time.fixedDeltaTime);
                }
                else
                {
                    moveInput = Vector2.zero;
                    if (!isAttacking || attackTimer >= attackCooldown)
                    {
                        isAttacking = true;
                        attackTimer = 0f;
                        // Chọn animation attack đúng hướng
                        string attackAnim = "Swordsman_lvl1_attack_front_Clip";
                        if (lastMoveDir.x < -0.5f)
                            attackAnim = "Swordsman_lvl1_attack_side_left_Clip";
                        else if (lastMoveDir.x > 0.5f)
                            attackAnim = "Swordsman_lvl1_attack_side_right_Clip";
                        else if (lastMoveDir.y > 0.5f)
                            attackAnim = "Swordsman_lvl1_attack_back_Clip";
                        else if (lastMoveDir.y < -0.5f)
                            attackAnim = "Swordsman_lvl1_attack_front_Clip";
                        animator.Play(attackAnim);

                        // Gọi player bị thương
                        Vector2 attackDir = (player.position - transform.position).normalized;
                    }
                }
            }
            else
            {
                moveInput = Vector2.zero;
            }
        }
        else
        {
            rb.MovePosition(rb.position + moveInput * moveSpeed * Time.fixedDeltaTime);
        }

        // Idle khi không di chuyển
        if (moveInput == Vector2.zero && !isAttacking && !isHurting)
        {
            string idleAnim = "Swordsman_lvl1_Idle_front_Clip";
            if (lastMoveDir.x < -0.5f)
                idleAnim = "Swordsman_lvl1_Idle_side_left_Clip";
            else if (lastMoveDir.x > 0.5f)
                idleAnim = "Swordsman_lvl1_Idle_side_right_Clip";
            else if (lastMoveDir.y > 0.5f)
                idleAnim = "Swordsman_lvl1_Idle_back_Clip";
            else if (lastMoveDir.y < -0.5f)
                idleAnim = "Swordsman_lvl1_Idle_front_Clip";
            animator.Play(idleAnim);
        }
    }
}
