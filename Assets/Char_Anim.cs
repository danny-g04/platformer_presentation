using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using System.Collections.Generic;

public class Char_Anim : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 5f;
    public float dashSpeed = 10f;

    public float mult = 2f;
    public float dur = 4f;

    private bool moving = false;
    private bool facingLeft = false;

    public AudioSource sfxSource;
    public AudioClip jumpClip;
    public AudioClip powerupClip;
    public AudioClip coinClip;
    public AudioClip enemyKillClip;
    public AudioClip levelFinishClip;
    public AudioClip dashClip;

    Rigidbody2D rb;
    Animator animator;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        if (sfxSource == null)
            sfxSource = GetComponent<AudioSource>();
    }


    // Update is called once per frame
    void Update()
    {
        HandleMovement();
        HandleJump();
        HandleDash();
        HandleAnimation();

    }

    void HandleMovement()
    {
        float horizontal = 0f;

        if (Keyboard.current.aKey.isPressed)
        {
            horizontal = -1f;
            facingLeft = true;
            moving = true;
        }
        else if (Keyboard.current.dKey.isPressed)
        {
            horizontal = 1f;
            facingLeft = false;
            moving = true;
        }
        else
        {
            moving = false;
        }

        rb.linearVelocity = new Vector2(horizontal * moveSpeed, rb.linearVelocity.y);
    }

    void HandleDash()
    {
        if (Keyboard.current.leftShiftKey.isPressed)
        {
            float direction = 1;
            float originalGravity = rb.gravityScale;
            if (facingLeft)
                direction = -1;
            else
                direction = 1;
            rb.gravityScale = 0;
            rb.linearVelocity = new Vector2(direction * dashSpeed, 0);

            rb.gravityScale = originalGravity;
        }
        if (Keyboard.current.leftShiftKey.wasPressedThisFrame)
            sfxSource.PlayOneShot(dashClip);
    }
    void HandleJump()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            if (jumpClip != null)
            {
                sfxSource.PlayOneShot(jumpClip);
            }

        }


    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("powerup"))
        {
            StartCoroutine(Pickup(other));
        }
    }

    IEnumerator Pickup(Collider2D powerup)
    {
        Destroy(powerup.gameObject);
        moveSpeed *= mult;
        jumpForce *= 3f;
        yield return new WaitForSeconds(dur);
        moveSpeed /= mult;
        jumpForce /= 3f;
    }

    public void PlayCoinSFX()
    {
        if (coinClip != null)
        {
            sfxSource.PlayOneShot(coinClip);
        }
    }

    public void PlayEnemyKillSFX()
    {
        sfxSource.PlayOneShot(enemyKillClip);
    }

    public void PlayLevelFinishSfx()
    {
        sfxSource.PlayOneShot(levelFinishClip);
    }

    void HandleAnimation()
    {
        animator.SetBool("Moving", moving);
        animator.SetBool("FacingLeft", facingLeft);
    }
}

