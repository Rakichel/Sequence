using UnityEngine;

namespace PlayerInfo
{
    /// <summary>
    /// 플레이어의 대쉬 기능을 담당하는 클래스입니다.
    /// </summary>
    [RequireComponent(typeof(Player))]
    public class PlayerDash : MonoBehaviour
    {
        private Player _player;
        private float _dashTimer = 0f;           // 대쉬 지속 시간을 재는 타이머
        private float _dashCooldownTimer = 0f;   // 대쉬 쿨타임을 재는 타이머
        private Vector2 dashVelocity;            // 최종 대쉬 속도값을 받을 벡터

        public float DashSpeed;             // 대쉬 속도
        public float DashDuration;          // 대쉬 지속 시간
        public float DashCooldown;          // 대쉬 쿨타임

        private void Start()
        {
            _player = GetComponent<Player>();
        }

        private void Update()
        {
            // 대쉬 쿨타임 감소
            if (_dashCooldownTimer > 0)
            {
                _dashCooldownTimer -= Time.deltaTime;
            }

            // Shift 키 누르면 대쉬
            if (Input.GetKeyDown(KeyCode.LeftShift) && _dashCooldownTimer <= 0 && _player.PlayerFixedState())
            {
                DashInit();
            }
        }

        private void FixedUpdate()
        {
            // 대쉬 상태인 경우
            if (_player.State == PlayerState.Dash)
            {
                // 대쉬 움직임
                DashMovement();

                // 시간이 다 지나면 Idle 상태로 전환
                _dashTimer -= Time.deltaTime;
                if (_dashTimer <= 0)
                {
                    _player.State = PlayerState.Idle;
                }
            }
        }
        /// <summary>
        /// 대쉬할 때 필요한 변수들을 초기화 해주는 함수입니다.
        /// </summary>
        private void DashInit()
        {
            _player.State = PlayerState.Dash;
            _dashTimer = DashDuration;
            _dashCooldownTimer = DashCooldown;
        }

        /// <summary>
        /// 대쉬 움직임이 구현된 함수입니다.
        /// </summary>
        private void DashMovement()
        {
            //지금 보고있는 방향을 통해 대쉬 방향 지정
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
