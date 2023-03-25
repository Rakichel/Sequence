using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempMove : MonoBehaviour
{

    // Jump
    public float speed = 5f;
    public float jumpForce = 10f;
    public Transform groundCheck;
    public float groundCheckRadius = 0.1f;
    public LayerMask whatIsGround;

    private Rigidbody2D rb;
    private float horizontal;
    private bool isGrounded;

    // Dash
    public float dashSpeed = 10f;       // 대쉬 속도
    public float dashDuration = 0.2f;   // 대쉬 지속 시간
    public float dashCooldown = 1f;     // 대쉬 쿨타임
    private bool isDashing = false;     // 대쉬 상태인지 여부
    private float dashTimer = 0f;       // 대쉬 지속 시간을 재는 타이머
    private float dashCooldownTimer = 0f;   // 대쉬 쿨타임을 재는 타이머

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        isGrounded = Physics2D.OverlapBox(groundCheck.position, new Vector2(1f, 1f), 0f, whatIsGround);

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        }
        // 대쉬 쿨타임 감소
        if (dashCooldownTimer > 0)
        {
            dashCooldownTimer -= Time.deltaTime;
        }

        // Shift 키 누르면 대쉬
        if (Input.GetKeyDown(KeyCode.LeftShift) && dashCooldownTimer <= 0)
        {
            isDashing = true;
            dashTimer = dashDuration;
            dashCooldownTimer = dashCooldown;
        }
    }

    private void FixedUpdate()
    {
        if (isDashing)
        {
            Vector2 dashVelocity = transform.right * dashSpeed;
            if (Input.GetAxisRaw("Horizontal") < 0)
            {
                dashVelocity = -transform.right * dashSpeed;
            }
            rb.velocity = dashVelocity;
            dashTimer -= Time.deltaTime;

            if (dashTimer <= 0)
            {
                isDashing = false;
                rb.velocity = new Vector2(horizontal, 0f);
            }
        }
        else
        {
            Vector2 movement = new Vector2(horizontal, 0f);
            movement.Normalize();
            rb.AddForce(movement * speed);
        }
    }
}
