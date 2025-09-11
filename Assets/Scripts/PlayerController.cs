using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Vector2 moveInput;
    private Vector2 lastMoveDir = Vector2.down; // Mặc định hướng xuống
    private Rigidbody2D rb;
    private PlayerControls controls;
    private Animator animator;
    private bool isAttacking = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f;
        rb.freezeRotation = true;
        controls = new PlayerControls();
        animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        controls.Player.Move.performed += OnMovePerformed;
        controls.Player.Move.canceled += OnMoveCanceled;
        controls.Player.Attack.performed += OnAttackPerformed;
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Player.Move.performed -= OnMovePerformed;
        controls.Player.Move.canceled -= OnMoveCanceled;
        controls.Player.Attack.performed -= OnAttackPerformed;
        controls.Disable();
    }

    private void OnMovePerformed(InputAction.CallbackContext ctx)
    {
        moveInput = ctx.ReadValue<Vector2>();
        if (moveInput.magnitude > 0.1f)
        {
            lastMoveDir = moveInput;
        }
    }

    private void OnMoveCanceled(InputAction.CallbackContext ctx)
    {
        moveInput = Vector2.zero;
    }

    private void OnAttackPerformed(InputAction.CallbackContext ctx)
    {
        if (!isAttacking)
        {
            isAttacking = true;
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
        }
    }

    // Gọi từ animation event khi kết thúc animation Attack
    public void EndAttack()
    {
        isAttacking = false;
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveInput * moveSpeed * Time.fixedDeltaTime);

        if (isAttacking)
        {
            return;
        }

        if (moveInput == Vector2.zero)
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

            Console.WriteLine("Idle Animation Played: " + idleAnim);

            animator.Play(idleAnim);
        }
        else if (moveInput.magnitude > 0.5f)
        {
            string runAnim = "Swordsman_lvl1_Run_front_Clip";
            if (moveInput.x < -0.5f)
                runAnim = "Swordsman_lvl1_Run_side_left_Clip";
            else if (moveInput.x > 0.5f)
                runAnim = "Swordsman_lvl1_Run_side_right_Clip";
            else if (moveInput.y > 0.5f)
                runAnim = "Swordsman_lvl1_Run_back_Clip";
            else if (moveInput.y < -0.5f)
                runAnim = "Swordsman_lvl1_Run_front_Clip";
            animator.Play(runAnim);
        }
        else
        {
            string walkAnim = "Swordsman_lvl1_Walk_front_Clip";
            if (moveInput.x < -0.5f)
                walkAnim = "Swordsman_lvl1_Walk_side_left_Clip";
            else if (moveInput.x > 0.5f)
                walkAnim = "Swordsman_lvl1_Walk_side_right_Clip";
            else if (moveInput.y > 0.5f)
                walkAnim = "Swordsman_lvl1_Walk_back_Clip";
            else if (moveInput.y < -0.5f)
                walkAnim = "Swordsman_lvl1_Walk_front_Clip";
            animator.Play(walkAnim);
        }
    }
}
