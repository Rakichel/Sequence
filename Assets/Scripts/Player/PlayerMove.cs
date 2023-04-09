using UnityEngine;

namespace PlayerInfo
{
    /// <summary>
    /// 플레이어의 이동을 담당하는 클래스입니다.
    /// </summary>
    [RequireComponent(typeof(Player))]
    public class PlayerMove : MonoBehaviour
    {
        private Vector2 _movement;  // 플레이어를 움직이는 벡터
        private float _horizontal;  // 입력받은 값의 방향을 정함
        private Player _player;     // 플레이어 정보를 받아오기 위한 객체

        public float Speed;         // 이동속도

        void Start()
        {
            _player = GetComponent<Player>();
        }

        void Update()
        {
            // 경직된 상태가 아닐 때만 이동을 원함
            if (_player.PlayerFixedState())
                _horizontal = Input.GetAxisRaw("Horizontal");
            else
                _horizontal = 0;

            // 플레이어 상태 전환
            MoveStateSelector();
            // 플레이어 방향 전환
            DirectionSelector();

            if (_player.State != PlayerState.Dash)
            {
                // 움직임
                Movement();
            }
        }

        /// <summary>
        /// 플레이어의 이동상태 전환을 판단하는 함수입니다.
        /// </summary>
        private void MoveStateSelector()
        {
            if (!Input.anyKey && _player.State == PlayerState.Move)
                _player.State = PlayerState.Idle;

            else if (_horizontal != 0 && _player.State == PlayerState.Idle)
            {
                _player.State = PlayerState.Move;
            }
        }

        /// <summary>
        /// 플레이어가 보고있는 방향을 판단하는 함수입니다.
        /// </summary>
        private void DirectionSelector()
        {
            if (_horizontal > 0)
            {
                _player.Direction = PlayerDirection.Right;
                _player.SpriteRender.flipX = false;
            }
            else if (_horizontal < 0)
            {
                _player.Direction = PlayerDirection.Left;
                _player.SpriteRender.flipX = true;
            }
        }

        /// <summary>
        /// 움직임을 구현한 함수입니다.
        /// </summary>
        private void Movement()
        {
            _movement = new Vector2(_horizontal, 0f);
            _movement.Normalize();
            _player.Rigidbody.velocity = new Vector2(_movement.x * Speed / Time.timeScale, _player.Rigidbody.velocity.y);
        }
    }
}