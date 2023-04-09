using UnityEngine;

namespace PlayerInfo
{
    /// <summary>
    /// �÷��̾��� �̵��� ����ϴ� Ŭ�����Դϴ�.
    /// </summary>
    [RequireComponent(typeof(Player))]
    public class PlayerMove : MonoBehaviour
    {
        private Vector2 _movement;  // �÷��̾ �����̴� ����
        private float _horizontal;  // �Է¹��� ���� ������ ����
        private Player _player;     // �÷��̾� ������ �޾ƿ��� ���� ��ü

        public float Speed;         // �̵��ӵ�

        void Start()
        {
            _player = GetComponent<Player>();
        }

        void Update()
        {
            // ������ ���°� �ƴ� ���� �̵��� ����
            if (_player.PlayerFixedState())
                _horizontal = Input.GetAxisRaw("Horizontal");
            else
                _horizontal = 0;

            // �÷��̾� ���� ��ȯ
            MoveStateSelector();
            // �÷��̾� ���� ��ȯ
            DirectionSelector();

            if (_player.State != PlayerState.Dash)
            {
                // ������
                Movement();
            }
        }

        /// <summary>
        /// �÷��̾��� �̵����� ��ȯ�� �Ǵ��ϴ� �Լ��Դϴ�.
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
        /// �÷��̾ �����ִ� ������ �Ǵ��ϴ� �Լ��Դϴ�.
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
        /// �������� ������ �Լ��Դϴ�.
        /// </summary>
        private void Movement()
        {
            _movement = new Vector2(_horizontal, 0f);
            _movement.Normalize();
            _player.Rigidbody.velocity = new Vector2(_movement.x * Speed / Time.timeScale, _player.Rigidbody.velocity.y);
        }
    }
}