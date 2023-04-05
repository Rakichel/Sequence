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
        private bool _isGrounded;       // ���� ���� ����� Ȯ���ϴ� �뵵

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

            // ���� �� ��� ������ ������ ���°� �ƴ� �� �����̽� �ٸ� ���� �۵�
            if (Input.GetKeyDown(KeyCode.Space) && _isGrounded && _player.PlayerFixedState())
            {
                _player.Rigidbody.AddForce(new Vector2(0, JumpForce), ForceMode2D.Impulse);
            }
            else if (Input.GetKeyDown(KeyCode.Space) && _isGrounded && _player.State == PlayerState.Dash)
            {
                _player.Rigidbody.AddForce(new Vector2(0, JumpForce), ForceMode2D.Impulse);
            }
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
