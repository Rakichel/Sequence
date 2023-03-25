using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempDash : MonoBehaviour
{
    public float speed = 5f;            // �÷��̾� �̵� �ӵ�
    public float dashSpeed = 10f;       // �뽬 �ӵ�
    public float dashDuration = 0.2f;   // �뽬 ���� �ð�
    public float dashCooldown = 1f;     // �뽬 ��Ÿ��
    private Rigidbody2D rb;             // Rigidbody2D ������Ʈ
    private bool isDashing = false;     // �뽬 �������� ����
    private float dashTimer = 0f;       // �뽬 ���� �ð��� ��� Ÿ�̸�
    private float dashCooldownTimer = 0f;   // �뽬 ��Ÿ���� ��� Ÿ�̸�

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();   // Rigidbody2D ������Ʈ ��������
    }

    void Update()
    {
        // �뽬 ��Ÿ�� ����
        if (dashCooldownTimer > 0)
        {
            dashCooldownTimer -= Time.deltaTime;
        }

        // Shift Ű ������ �뽬
        if (Input.GetKeyDown(KeyCode.LeftShift) && dashCooldownTimer <= 0)
        {
            isDashing = true;
            dashTimer = dashDuration;
            dashCooldownTimer = dashCooldown;
        }
    }

    void FixedUpdate()
    {
        // �뽬 ������ ���
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
        // �뽬 ���°� �ƴ� ���
        else
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");
            Vector2 movement = new Vector2(horizontal, vertical);
            rb.velocity = movement * speed;
        }
    }
}
