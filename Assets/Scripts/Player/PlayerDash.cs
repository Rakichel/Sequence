using UnityEngine;

namespace PlayerInfo
{
    /// <summary>
    /// �÷��̾��� �뽬 ����� ����ϴ� Ŭ�����Դϴ�.
    /// </summary>
    [RequireComponent(typeof(Player))]
    public class PlayerDash : MonoBehaviour
    {
        private Player _player;
        private float _dashTimer = 0f;           // �뽬 ���� �ð��� ��� Ÿ�̸�
        private float _dashCooldownTimer = 0f;   // �뽬 ��Ÿ���� ��� Ÿ�̸�
        private Vector2 dashVelocity;            // ���� �뽬 �ӵ����� ���� ����

        public float DashSpeed;             // �뽬 �ӵ�
        public float DashDuration;          // �뽬 ���� �ð�
        public float DashCooldown;          // �뽬 ��Ÿ��

        private void Start()
        {
            _player = GetComponent<Player>();
        }

        private void Update()
        {
            // �뽬 ��Ÿ�� ����
            if (_dashCooldownTimer > 0)
            {
                _dashCooldownTimer -= Time.deltaTime;
            }

            // Shift Ű ������ �뽬
            if (Input.GetKeyDown(KeyCode.LeftShift) && _dashCooldownTimer <= 0 && _player.PlayerFixedState())
            {
                DashInit();
            }
        }

        private void FixedUpdate()
        {
            // �뽬 ������ ���
            if (_player.State == PlayerState.Dash)
            {
                // �뽬 ������
                DashMovement();

                // �ð��� �� ������ Idle ���·� ��ȯ
                _dashTimer -= Time.deltaTime;
                if (_dashTimer <= 0)
                {
                    _player.State = PlayerState.Idle;
                }
            }
        }
        /// <summary>
        /// �뽬�� �� �ʿ��� �������� �ʱ�ȭ ���ִ� �Լ��Դϴ�.
        /// </summary>
        private void DashInit()
        {
            _player.State = PlayerState.Dash;
            _dashTimer = DashDuration;
            _dashCooldownTimer = DashCooldown;
        }

        /// <summary>
        /// �뽬 �������� ������ �Լ��Դϴ�.
        /// </summary>
        private void DashMovement()
        {
            //���� �����ִ� ������ ���� �뽬 ���� ����
            if (_player.Direction == PlayerDirection.Right)
            {
                dashVelocity = transform.right * DashSpeed;
            }
            else 
            {
                dashVelocity = -transform.right * DashSpeed;
            }
            _player.Rigidbody.velocity = dashVelocity;
        }
    }
}
