using UnityEngine;

namespace PlayerInfo
{
    [RequireComponent(typeof(Player))]
    public class PlayerJump : MonoBehaviour
    {
        public float JumpForce;
        public Transform GroundCheck;
        public LayerMask WhatIsGround;
        public float CheckSizeX;
        public float CheckSizeY;

        private Player _player;
        private bool _isGrounded;

        void Start()
        {
            _player = GetComponent<Player>();
        }

        void Update()
        {
            // 땅에 발 딛고 있는지 체크
            _isGrounded = Physics2D.OverlapBox(GroundCheck.position - new Vector3(0, 0.5f - (CheckSizeY / 2f)), new Vector2(CheckSizeX, CheckSizeY), 0f, WhatIsGround);
            if (_isGrounded && _player.state == PlayerState.Jump)
            {
                _player.state = PlayerState.Idle;
            }
            else if(!_isGrounded && _player.PlayerFixedState())
            {
                _player.state = PlayerState.Jump;
            }
            if (Input.GetKeyDown(KeyCode.Space) && _isGrounded && _player.PlayerFixedState())
            {
                _player.Rigidbody.AddForce(new Vector2(0, JumpForce), ForceMode2D.Impulse);
            }
        }

        private void OnDrawGizmos()
        {
            // 체크 구역 시각화
            Gizmos.color = Color.yellow;
            Gizmos.DrawCube(GroundCheck.position - new Vector3(0, 0.5f - (CheckSizeY / 2f)), new Vector2(CheckSizeX, CheckSizeY));
        }
    }
}
