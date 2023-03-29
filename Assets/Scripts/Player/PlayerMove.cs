using UnityEngine;

namespace PlayerInfo
{
    [RequireComponent(typeof(Player))]
    public class PlayerMove : MonoBehaviour
    {
        public float Speed;

        private Vector2 _movement;
        private float _horizontal;
        private Player _player;

        void Start()
        {
            _player = GetComponent<Player>();
        }

        void Update()
        {
            if (_player.PlayerFixedState())
                _horizontal = Input.GetAxisRaw("Horizontal");
            else
                _horizontal = 0;

            if (!Input.anyKey && _player.state == PlayerState.Move)
                _player.state = PlayerState.Idle;

            if (_horizontal != 0 && _player.state == PlayerState.Idle)
            {
                _player.state = PlayerState.Move;
            }
            if (_horizontal > 0)
            {
                _player.direction = PlayerDirection.Right;
            }
            else if (_horizontal < 0)
            {
                _player.direction = PlayerDirection.Left;
            }
        }

        private void FixedUpdate()
        {
            if (_player.state != PlayerState.Dash)
            {
                _movement = new Vector2(_horizontal, 0f);
                _movement.Normalize();
                _player.Rigidbody.velocity = new Vector2(_movement.x * Speed, _player.Rigidbody.velocity.y);
            }
        }
    }
}