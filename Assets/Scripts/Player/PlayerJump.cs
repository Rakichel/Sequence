using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerInfo
{
    /// <summary>
    /// 플레이어의 점프 기능을 담당하는 클래스입니다.
    /// </summary>
    [RequireComponent(typeof(Player))]
    public class PlayerJump : MonoBehaviour
    {
        private Player _player;         // 플레이어 정보를 받아오기 위한 객체
        private Collider2D _isGrounded;       // 땅에 발을 딛는지 확인하는 용도
        private PlatformEffector2D _getGround;

        public float JumpForce;         // 점프력
        public Transform GroundCheck;   // 땅을 체크하기 위한 범위의 좌표
        public LayerMask WhatIsGround;  // 땅의 레이어를 받아옴
        public float CheckSizeX;        // 체크박스의 크기를 조절 (가로)
        public float CheckSizeY;        // 체크박스의 크기를 조절 (세로)

        private void Start()
        {
            _player = GetComponent<Player>();
        }

        private void Update()
        {
            // 땅에 발 딛고 있는지 체크
            _isGrounded = Physics2D.OverlapBox(GroundCheck.position, new Vector2(CheckSizeX, CheckSizeY), 0f, WhatIsGround);

            // 체크한 값으로 플레이어 상태 전환
            JumpStateSelector();

            // 하단 점프
            if (Input.GetKeyDown(KeyCode.Space) && Input.GetKey(KeyCode.DownArrow) && _isGrounded && _player.PlayerFixedState())
            {
                //_player.Rigidbody.AddForce(new Vector2(0, -JumpForce * 100f), ForceMode2D.Impulse);
                if(_isGrounded.CompareTag("Platform"))
                    StartCoroutine(DownJump());
            }
            // 땅에 발 딛고 있으며 경직된 상태가 아닐 때 스페이스 바를 통해 작동
            else if (Input.GetKeyDown(KeyCode.Space) && _isGrounded && _player.PlayerFixedState())
            {
                _player.Rigidbody.velocity = new Vector2(_player.Rigidbody.velocity.x, 0);
                _player.Rigidbody.AddForce(new Vector2(0, JumpForce), ForceMode2D.Impulse);
            }
            // 대쉬 중에도 점프 가능
            else if (Input.GetKeyDown(KeyCode.Space) && _isGrounded && _player.State == PlayerState.Dash)
            {
                _player.Rigidbody.velocity = new Vector2(_player.Rigidbody.velocity.x, 0);
                _player.Rigidbody.AddForce(new Vector2(0, JumpForce), ForceMode2D.Impulse);
            }
        }

        //
        IEnumerator DownJump()
        {
            _getGround = _isGrounded.GetComponent<PlatformEffector2D>();
            _getGround.surfaceArc = 0;
            yield return new WaitForSeconds(0.5f);
            _getGround.surfaceArc = 180;
        }

        /// <summary>
        /// 플레이어의 점프 상태를 전환하는 함수입니다.
        /// </summary>
        private void JumpStateSelector()
        {
            // 점프 후 땅에 다시 착지할 때
            if (_isGrounded && _player.State == PlayerState.Jump)
            {
                _player.State = PlayerState.Idle;
            }

            // 공중에 있으면서 플레이어가 경직된 상태가 아닐 때
            else if (!_isGrounded && _player.PlayerFixedState())
            {
                _player.State = PlayerState.Jump;
            }
        }

        private void OnDrawGizmos()
        {
            // 체크 구역 시각화
            Gizmos.color = Color.yellow;
            Gizmos.DrawCube(GroundCheck.position, new Vector2(CheckSizeX, CheckSizeY));
        }
    }
}
