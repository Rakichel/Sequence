using UnityEngine;

namespace PlayerInfo
{
    /// <summary>
    /// �÷��̾��� �뽬 ����� ����ϴ� Ŭ�����Դϴ�.
    /// </summary>
    [RequireComponent(typeof(Player))]
    public class PlayerDash : MonoBehaviour
    {
        private string _fileName = "PlayerStatus";

        private Player _player;
        private float _dashTimer = 0f;              // �뽬 ���� �ð��� ��� Ÿ�̸�
        private float _dashCooldownTimer = 0f;      // �뽬 ��Ÿ���� ��� Ÿ�̸�
        private Vector2 _dashVelocity;              // ���� �뽬 �ӵ����� ���� ����

        public float DashSpeed;             // �뽬 �ӵ�
        public float DashDuration;          // �뽬 ���� �ð�
        public float DashCooldown;          // �뽬 ��Ÿ��

        private void Start()
        {
            LoadData();
            _player = GetComponent<Player>();
        }

        private void Update()
        {
            // �뽬 ��Ÿ�� ����
            if (_dashCooldownTimer > 0)
            {
                _dashCooldownTimer -= Time.unscaledDeltaTime;
            }

            // Shift Ű ������ �뽬
            if (Input.GetKeyDown(KeyCode.C) && _dashCooldownTimer <= 0 && _player.PlayerFixedState())
            {
                DashInit();
                //Manager.SoundManager.Instance.PlaySFXSound("DashSound", 1f);
                if (_player.isDash)
                    Manager.EffectManager.Instance.Dash();
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
                _dashTimer -= Time.unscaledDeltaTime;
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
                _dashVelocity = transform.right * DashSpeed;
            }
            else
            {
                _dashVelocity = -transform.right * DashSpeed;
            }
            _player.Rigidbody.velocity = new Vector2(_dashVelocity.x, _player.Rigidbody.velocity.y);
        }
        private void SaveData()
        {
            JsonManager<PlayerStatus>.Save(new PlayerStatus(), _fileName);
        }

        private void LoadData()
        {
            PlayerStatus data = JsonManager<PlayerStatus>.Load(_fileName);
            if (data != null)
            {
                DashSpeed = data.DashSpeed;
                DashDuration = data.DashDuration;
                DashCooldown = data.DashCooldown;
            }
            else
            {
                SaveData();
                LoadData();
            }
        }
    }
}
