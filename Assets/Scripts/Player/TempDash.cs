using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempDash : MonoBehaviour
{
    public float speed = 5f;            // 플레이어 이동 속도
    public float dashSpeed = 10f;       // 대쉬 속도
    public float dashDuration = 0.2f;   // 대쉬 지속 시간
    public float dashCooldown = 1f;     // 대쉬 쿨타임
    private Rigidbody2D rb;             // Rigidbody2D 컴포넌트
    private bool isDashing = false;     // 대쉬 상태인지 여부
    private float dashTimer = 0f;       // 대쉬 지속 시간을 재는 타이머
    private float dashCooldownTimer = 0f;   // 대쉬 쿨타임을 재는 타이머

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();   // Rigidbody2D 컴포넌트 가져오기
    }

    void Update()
    {
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

    void FixedUpdate()
    {
        // 대쉬 상태인 경우
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
            }
        }
        // 대쉬 상태가 아닌 경우
        else
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");
            Vector2 movement = new Vector2(horizontal, vertical);
            rb.velocity = movement * speed;
        }
    }
}
