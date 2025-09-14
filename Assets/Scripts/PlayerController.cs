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

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveInput * moveSpeed * Time.fixedDeltaTime);
    }

    private void UpdateAnimation()
    {
        animator.SetInteger("HuongNhanVat", (int)_huongMatNhanVat);
        animator.SetBool("IsMoving", _isMoving);
    }
}
