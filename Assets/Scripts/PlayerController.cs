using UnityEngine;
using UnityEngine.InputSystem; // new Input System

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Vector2 moveInput;
    public Rigidbody2D rb;
    public Animator animator;

    private InputAction moveAction;
    private InputAction attackAction;

    private PlayerCombat playerCombat;

    private void Awake()
    {
        // rb = GetComponent<Rigidbody2D>();
        playerCombat = GetComponent<PlayerCombat>();
        // định nghĩa input "Move" dùng WASD
        moveAction = new InputAction("Move", InputActionType.Value);
        moveAction
            .AddCompositeBinding("2DVector")
            .With("Up", "<Keyboard>/w")
            .With("Down", "<Keyboard>/s")
            .With("Left", "<Keyboard>/a")
            .With("Right", "<Keyboard>/d");

        // định nghĩa input "Attack" dùng phím J
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
        // đọc input mỗi frame
        moveInput = moveAction.ReadValue<Vector2>();

        // đánh khi bấm J
        if (attackAction.WasPressedThisFrame())
        {
            playerCombat.Attack();
        }

        // tự động trở lại Idle khi hết thời gian đánh
    }

    private void FixedUpdate()
    {
        // di chuyển nhân vật theo input
        CapNhatViTriNhanVat();
        CapNhatHuongMat2Chieu();
        CapNhatAniamtion();

        //  DebugGUIHelper.Log("moveInput: " + moveInput);
    }

    //
    private void CapNhatViTriNhanVat()
    {
        rb.MovePosition(rb.position + moveInput * moveSpeed * Time.fixedDeltaTime); // cập nhật vị trí Player
    }

    private void CapNhatAniamtion()
    {
        animator.SetFloat("horizontal", Mathf.Abs(moveInput.x));
        animator.SetFloat("vertical", Mathf.Abs(moveInput.y));
    }

    private void CapNhatHuongMat2Chieu()
    {
        Vector3 scale = transform.localScale;

        if (moveInput.x > 0.01f)
        {
            scale.x = Mathf.Abs(scale.x); // quay phải
        }
        else if (moveInput.x < -0.01f)
        {
            scale.x = -Mathf.Abs(scale.x); // quay trái
        }

        transform.localScale = scale;
    }
}
