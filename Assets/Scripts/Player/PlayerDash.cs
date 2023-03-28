using UnityEngine;

namespace PlayerInfo
{
    [RequireComponent(typeof(Player))]
    public class PlayerDash : MonoBehaviour
    {
        public float dashSpeed;             // �뽬 �ӵ�
        public float dashDuration;          // �뽬 ���� �ð�
        public float dashCooldown;          // �뽬 ��Ÿ��

        private Player _player;
        private float _dashTimer = 0f;           // �뽬 ���� �ð��� ��� Ÿ�̸�
        private float _dashCooldownTimer = 0f;   // �뽬 ��Ÿ���� ��� Ÿ�̸�

        void Start()
        {
            _player = GetComponent<Player>();
        }

        void Update()
        {
            // �뽬 ��Ÿ�� ����
            if (_dashCooldownTimer > 0)
            {
                _dashCooldownTimer -= Time.deltaTime;
            }

            // Shift Ű ������ �뽬
            if (Input.GetKeyDown(KeyCode.LeftShift) && _dashCooldownTimer <= 0 && _player.PlayerFixedState())
            {
                _player.state = PlayerState.Dash;
                _dashTimer = dashDuration;
                _dashCooldownTimer = dashCooldown;
            }
        }

        void FixedUpdate()
        {
            // �뽬 ������ ���
            if (_player.state == PlayerState.Dash)
            {
                Vector2 dashVelocity;
                dashVelocity = transform.right * dashSpeed;
                if (_player.direction == PlayerDirection.Right)
                {
                    dashVelocity = transform.right * dashSpeed;
                }
                else if (_player.direction == PlayerDirection.Left)
                {
                    dashVelocity = -transform.right * dashSpeed;
                }
                _player.Rigidbody.velocity = dashVelocity;
                _dashTimer -= Time.deltaTime;

                if (_dashTimer <= 0)
                {
                    _player.state = PlayerState.Idle;
                }
            }
        }
    }

}
