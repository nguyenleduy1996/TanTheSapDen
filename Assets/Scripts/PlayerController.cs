using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 3f;

    private Rigidbody2D rb;
    private Animator animator;
    private Vector2 moveInput;

    private HuongNhanVat _huongMatNhanVat;
    private bool _isMoving;

    private InputAction moveAction;

    public enum HuongNhanVat { Trai = 1, Phai = 2, Len = 3, Xuong = 4 }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        moveAction = new InputAction("Move", InputActionType.Value);
        moveAction.AddCompositeBinding("2DVector")
            .With("Up", "<Keyboard>/w")
            .With("Down", "<Keyboard>/s")
            .With("Left", "<Keyboard>/a")
            .With("Right", "<Keyboard>/d");
    }

    private void OnEnable() => moveAction.Enable();
    private void OnDisable() => moveAction.Disable();

    private void Update()
    {
        moveInput = moveAction.ReadValue<Vector2>();
        _isMoving = moveInput != Vector2.zero;

        if (_isMoving) //Huong Nhan Vat
        {
            if (Mathf.Abs(moveInput.x) > Mathf.Abs(moveInput.y))
                _huongMatNhanVat = moveInput.x > 0 ? HuongNhanVat.Phai : HuongNhanVat.Trai;
            else
                _huongMatNhanVat = moveInput.y > 0 ? HuongNhanVat.Len : HuongNhanVat.Xuong;
        }

        UpdateAnimation();
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

    private void UpdateAnimation()
    {
        animator.SetInteger("HuongNhanVat", (int)_huongMatNhanVat);
        animator.SetBool("IsMoving", _isMoving);
    }
}
