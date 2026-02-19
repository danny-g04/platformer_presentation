using UnityEngine;
using UnityEngine.InputSystem;

public class Char_Anim1 : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 5f;
    Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        HandleMovement();
        HandleJump();
    }

    void HandleMovement()
    {
        float horizontal = 0f;
        if (Keyboard.current.aKey.isPressed)
        {
            horizontal= -1f;
        } else if (Keyboard.current.dKey.isPressed)
        {
            horizontal= 1f;
        }
        rb.linearVelocity = new Vector2(horizontal * moveSpeed, rb.linearVelocity.y);
    }

    void HandleJump()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }
    }
}
