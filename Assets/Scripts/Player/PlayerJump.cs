using Manager;
using System.Collections;
using UnityEngine;

namespace PlayerInfo
{
    /// <summary>
    /// �÷��̾��� ���� ����� ����ϴ� Ŭ�����Դϴ�.
    /// </summary>
    [RequireComponent(typeof(Player))]
    public class PlayerJump : MonoBehaviour
    {
        private Player _player;         // �÷��̾� ������ �޾ƿ��� ���� ��ü
        private Collider2D _isGrounded;       // ���� ���� ����� Ȯ���ϴ� �뵵
        private PlatformEffector2D _getGround;
        private int _jumpCnt = 2;

        public float JumpForce;         // ������
        public Transform GroundCheck;   // ���� üũ�ϱ� ���� ������ ��ǥ
        public LayerMask WhatIsGround;  // ���� ���̾ �޾ƿ�
        public float CheckSizeX;        // üũ�ڽ��� ũ�⸦ ���� (����)
        public float CheckSizeY;        // üũ�ڽ��� ũ�⸦ ���� (����)
        public float LandAnimationTime;

        private void Start()
        {
            _player = GetComponent<Player>();
        }

        private void Update()
        {
            // ���� �� ��� �ִ��� üũ
            _isGrounded = Physics2D.OverlapBox(GroundCheck.position, new Vector2(CheckSizeX, CheckSizeY), 0f, WhatIsGround);

            if (_isGrounded)
            {
                _jumpCnt = 2;
            }
            // üũ�� ������ �÷��̾� ���� ��ȯ
            JumpStateSelector();

            // ����
            if (Input.GetKeyDown(KeyCode.Space))
            {
                // �ϴ� ����
                if (Input.GetKey(KeyCode.DownArrow) && _player.PlayerFixedState())
                {
                    // ���߿� ���� �� ����
                    if (!_isGrounded)
                    {
                        StartCoroutine(Landing());
                    }
                    // ���鿡 ���� �� �������� ��������
                    else if (_isGrounded && _isGrounded.CompareTag("Platform"))
                    {
                        _jumpCnt--;
                        StartCoroutine(DownJump());
                    }
                }
                // ���� �� ��� ������ ������ ���°� �ƴ� �� Ȥ�� �뽬 �� �϶� �۵�
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
        /// �ϴ� ���� ������ �Դϴ�.
        /// ��� �ִ� ���� ǥ���� 180���� �����Ͽ� �÷��̾ ���� ��� ���� �ʰ� �մϴ�.
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
        /// ���� ������ �Դϴ�.
        /// ���������� ������ ���� ���ؼ� ������ �������� �ϰ� �ִϸ��̼ǿ� ���缭 �����·� ���� �մϴ�.
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
        /// �÷��̾��� ���� ���¸� ��ȯ�ϴ� �Լ��Դϴ�.
        /// </summary>
        private void JumpStateSelector()
        {
            // ���� �� ���� �ٽ� ������ ��
            if (_isGrounded && (_player.State == PlayerState.Jump || _player.State == PlayerState.DJump))
            {
                _player.State = PlayerState.Idle;
            }

            // ���߿� �����鼭 �÷��̾ ������ ���°� �ƴ� ��
            else if (!_isGrounded && _player.PlayerFixedState() && _player.State != PlayerState.DJump)
            {
                _player.State = PlayerState.Jump;
            }
        }

        private void OnDrawGizmos()
        {
            // üũ ���� �ð�ȭ
            Gizmos.color = Color.yellow;
            Gizmos.DrawCube(GroundCheck.position, new Vector2(CheckSizeX, CheckSizeY));
        }
    }
}
