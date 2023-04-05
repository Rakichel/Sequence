using System.Collections;
using System.Collections.Generic;
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

        public float JumpForce;         // ������
        public Transform GroundCheck;   // ���� üũ�ϱ� ���� ������ ��ǥ
        public LayerMask WhatIsGround;  // ���� ���̾ �޾ƿ�
        public float CheckSizeX;        // üũ�ڽ��� ũ�⸦ ���� (����)
        public float CheckSizeY;        // üũ�ڽ��� ũ�⸦ ���� (����)

        private void Start()
        {
            _player = GetComponent<Player>();
        }

        private void Update()
        {
            // ���� �� ��� �ִ��� üũ
            _isGrounded = Physics2D.OverlapBox(GroundCheck.position, new Vector2(CheckSizeX, CheckSizeY), 0f, WhatIsGround);

            // üũ�� ������ �÷��̾� ���� ��ȯ
            JumpStateSelector();

            // �ϴ� ����
            if (Input.GetKeyDown(KeyCode.Space) && Input.GetKey(KeyCode.DownArrow) && _isGrounded && _player.PlayerFixedState())
            {
                //_player.Rigidbody.AddForce(new Vector2(0, -JumpForce * 100f), ForceMode2D.Impulse);
                if(_isGrounded.CompareTag("Platform"))
                    StartCoroutine(DownJump());
            }
            // ���� �� ��� ������ ������ ���°� �ƴ� �� �����̽� �ٸ� ���� �۵�
            else if (Input.GetKeyDown(KeyCode.Space) && _isGrounded && _player.PlayerFixedState())
            {
                _player.Rigidbody.velocity = new Vector2(_player.Rigidbody.velocity.x, 0);
                _player.Rigidbody.AddForce(new Vector2(0, JumpForce), ForceMode2D.Impulse);
            }
            // �뽬 �߿��� ���� ����
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
        /// �÷��̾��� ���� ���¸� ��ȯ�ϴ� �Լ��Դϴ�.
        /// </summary>
        private void JumpStateSelector()
        {
            // ���� �� ���� �ٽ� ������ ��
            if (_isGrounded && _player.State == PlayerState.Jump)
            {
                _player.State = PlayerState.Idle;
            }

            // ���߿� �����鼭 �÷��̾ ������ ���°� �ƴ� ��
            else if (!_isGrounded && _player.PlayerFixedState())
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
