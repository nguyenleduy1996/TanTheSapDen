using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class PlayerController : MonoBehaviour
{
    [Header("Player Settings")]
    public float moveSpeed = 1f;
    public float attackSpeed = 1f;

    private Rigidbody2D rb;
    private Animator animator;
    private Vector2 moveInput;

    private HuongNhanVat _huongMatNhanVat;
    private bool _isMoving;
    private bool _isAttack;

    private InputAction moveAction;
    private InputAction attackAction;

    public enum HuongNhanVat { Trai = 1, Phai = 2, Len = 3, Xuong = 4 }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        // Move action với WASD
        moveAction = new InputAction("Move", InputActionType.Value);
        moveAction.AddCompositeBinding("2DVector")
            .With("Up", "<Keyboard>/w")
            .With("Down", "<Keyboard>/s")
            .With("Left", "<Keyboard>/a")
            .With("Right", "<Keyboard>/d");

        // Attack action với J
        attackAction = new InputAction("Attack", InputActionType.Button, "<Keyboard>/j");
    }

    private void OnEnable()
    {
        moveAction.Enable();
        attackAction.Enable();
    }

    private void OnDisable()
    {
        moveAction.Disable();
        attackAction.Disable();
    }

    private void Update()
    {
        // --- Movement ---
        moveInput = moveAction.ReadValue<Vector2>();
        _isMoving = moveInput != Vector2.zero;

        if (_isMoving)
        {
            if (Mathf.Abs(moveInput.x) > Mathf.Abs(moveInput.y))
                _huongMatNhanVat = moveInput.x > 0 ? HuongNhanVat.Phai : HuongNhanVat.Trai;
            else
                _huongMatNhanVat = moveInput.y > 0 ? HuongNhanVat.Len : HuongNhanVat.Xuong;
        }

        // --- Attack ---
        if (attackAction.WasPressedThisFrame())
        {
            _isAttack = true;
        }
        else if (attackAction.WasReleasedThisFrame())
        {
            _isAttack = false;
        }

        UpdateAnimation();
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveInput * moveSpeed * Time.fixedDeltaTime);
    }

    private void UpdateAnimation()
    {
        animator.SetInteger("HuongNhanVat", (int)_huongMatNhanVat);
        animator.SetBool("IsMoving", _isMoving);
        animator.SetBool("IsAttack", _isAttack);

        // Debug cho dễ check
        Debug.Log($"Hướng mặt: {_huongMatNhanVat}, Moving: {_isMoving}, Attack: {_isAttack}");
    }
}
