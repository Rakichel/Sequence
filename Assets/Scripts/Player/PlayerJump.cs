using Manager;
using System.Collections;
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
        private int _jumpCnt = 2;

        public float JumpForce;         // 점프력
        public Transform GroundCheck;   // 땅을 체크하기 위한 범위의 좌표
        public LayerMask WhatIsGround;  // 땅의 레이어를 받아옴
        public float CheckSizeX;        // 체크박스의 크기를 조절 (가로)
        public float CheckSizeY;        // 체크박스의 크기를 조절 (세로)
        public float LandAnimationTime;

        private void Start()
        {
            _player = GetComponent<Player>();
        }

        private void Update()
        {
            // 땅에 발 딛고 있는지 체크
            _isGrounded = Physics2D.OverlapBox(GroundCheck.position, new Vector2(CheckSizeX, CheckSizeY), 0f, WhatIsGround);

            if (_isGrounded)
            {
                _jumpCnt = 2;
            }
            // 체크한 값으로 플레이어 상태 전환
            JumpStateSelector();

            // 점프
            if (Input.GetKeyDown(KeyCode.Space))
            {
                // 하단 점프
                if (Input.GetKey(KeyCode.DownArrow) && _player.PlayerFixedState())
                {
                    // 공중에 있을 시 랜딩
                    if (!_isGrounded)
                    {
                        StartCoroutine(Landing());
                    }
                    // 지면에 있을 시 밑층으로 내려가기
                    else if (_isGrounded && _isGrounded.CompareTag("Platform"))
                    {
                        _jumpCnt--;
                        StartCoroutine(DownJump());
                    }
                }
                // 땅에 발 딛고 있으며 경직된 상태가 아닐 때 혹은 대쉬 중 일때 작동
                else if (_isGrounded && (_player.PlayerFixedState() || _player.State == PlayerState.Dash))
                {
                    _jumpCnt--;
                    _player.Rigidbody.velocity = new Vector2(_player.Rigidbody.velocity.x, 0);
                    _player.Rigidbody.AddForce(new Vector2(0, JumpForce), ForceMode2D.Impulse);
                }
                else if (_player.State == PlayerState.Jump && _jumpCnt > 0)
                {
                    _jumpCnt--;
                    _player.State = PlayerState.DJump;
                    _player.Rigidbody.velocity = new Vector2(_player.Rigidbody.velocity.x, 0);
                    _player.Rigidbody.AddForce(new Vector2(0, JumpForce), ForceMode2D.Impulse);
                }
            }
        }

        /// <summary>
        /// 하단 점프 구현부 입니다.
        /// 밟고 있는 땅의 표면을 180도로 변경하여 플레이어가 땅을 밟고 있지 않게 합니다.
        /// </summary>
        /// <returns></returns>
        IEnumerator DownJump()
        {
            _getGround = _isGrounded.GetComponent<PlatformEffector2D>();
            _getGround.surfaceArc = 0;
            yield return new WaitForSecondsRealtime(0.4f);
            _getGround.surfaceArc = 180;
        }

        /// <summary>
        /// 랜딩 구현부 입니다.
        /// 순간적으로 밑으로 힘을 가해서 빠르게 내려가게 하고 애니메이션에 맞춰서 원상태로 복귀 합니다.
        /// </summary>
        /// <returns></returns>
        IEnumerator Landing()
        {
            yield return null;
            _player.State = PlayerState.Landing;
            _player.Rigidbody.velocity = new Vector2(_player.Rigidbody.velocity.x, 0);
            _player.Rigidbody.AddForce(new Vector2(0, -JumpForce), ForceMode2D.Impulse);
            while (!_isGrounded)
            {
                yield return new WaitForEndOfFrame();
            }
            _player.State = PlayerState.Landed;
            CameraManager.Instance.Impulse();
            yield return new WaitForSecondsRealtime(LandAnimationTime);
            _player.State = PlayerState.Idle;
        }

        /// <summary>
        /// 플레이어의 점프 상태를 전환하는 함수입니다.
        /// </summary>
        private void JumpStateSelector()
        {
            // 점프 후 땅에 다시 착지할 때
            if (_isGrounded && (_player.State == PlayerState.Jump || _player.State == PlayerState.DJump))
            {
                _player.State = PlayerState.Idle;
            }

            // 공중에 있으면서 플레이어가 경직된 상태가 아닐 때
            else if (!_isGrounded && _player.PlayerFixedState() && _player.State != PlayerState.DJump)
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
