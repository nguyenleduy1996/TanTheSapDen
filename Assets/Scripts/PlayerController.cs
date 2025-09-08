using UnityEngine;
using UnityEngine.InputSystem; // để dùng Input System mới

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;         // tốc độ di chuyển
    private Vector2 moveInput;           // input từ bàn phím
    private Rigidbody2D rb;              // rigidbody để di chuyển

    private PlayerControls controls;     // class auto-gen từ PlayerControls.inputactions

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        controls = new PlayerControls(); // tạo instance
    }

    private void OnEnable()
    {
        // Enable input
        controls.Enable();

        // Lắng nghe action Move
        controls.Player.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        controls.Player.Move.canceled += ctx => moveInput = Vector2.zero;
    }

    private void OnDisable()
    {
        controls.Disable();
    }

    private void FixedUpdate()
    {
        // Di chuyển player theo input (RPG top-down nên bỏ gravity)
        rb.MovePosition(rb.position + moveInput * moveSpeed * Time.fixedDeltaTime);
    }
}
