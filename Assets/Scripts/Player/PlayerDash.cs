using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerDash : MonoBehaviour
{
    public float dashSpeed;             // 대쉬 속도
    public float dashDuration;          // 대쉬 지속 시간
    public float dashCooldown;          // 대쉬 쿨타임

    private Player _player;
    private float _dashTimer = 0f;           // 대쉬 지속 시간을 재는 타이머
    private float _dashCooldownTimer = 0f;   // 대쉬 쿨타임을 재는 타이머

    void Start()
    {
        _player = GetComponent<Player>();
    }

    void Update()
    {
        // 대쉬 쿨타임 감소
        if (_dashCooldownTimer > 0)
        {
            _dashCooldownTimer -= Time.deltaTime;
        }

        // Shift 키 누르면 대쉬
        if (Input.GetKeyDown(KeyCode.LeftShift) && _dashCooldownTimer <= 0)
        {
            _player.IsDashing = true;
            _dashTimer = dashDuration;
            _dashCooldownTimer = dashCooldown;
        }
    }

    void FixedUpdate()
    {
        // 대쉬 상태인 경우
        if (_player.IsDashing)
        {
            Vector2 dashVelocity = transform.right * dashSpeed;
            if (Input.GetAxisRaw("Horizontal") < 0)
            {
                dashVelocity = -transform.right * dashSpeed;
            }
            _player.Rigidbody.velocity = dashVelocity;
            _dashTimer -= Time.deltaTime;

            if (_dashTimer <= 0)
            {
                _player.IsDashing = false;
            }
        }
    }
}
