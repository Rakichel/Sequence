using UnityEngine;

namespace BossInfo
{
    public class BossMovement : IBossTodo
    {
        private float _distanceX = 0f;
        private float _distanceY = 0f;
        private Vector2 _movement = Vector2.one;
        private PlatformEffector2D _getGround;
        private Collider2D _isGrounded;       // 땅에 발을 딛는지 확인하는 용도
        private float _timer = 0f;
        private bool _isActive;

        private BossController _controller;
        private float _jumpPower;
        private float _speed;
        private Rigidbody2D _rigid;
        private Transform _transform;
        private Transform _destination;


        public BossMovement(BossController _controller, float _speed = 8f, float _jumpPower = 20f)
        {
            this._controller = _controller;
            this._jumpPower = _jumpPower;
            this._speed = _speed;
            _rigid = _controller.GetComponent<Rigidbody2D>();
            _transform = _controller.transform;
            _destination = _controller.Player.transform;
        }

        public void Work()
        {
            _isGrounded = Physics2D.OverlapBox(_controller.transform.position, new Vector2(0.5f, 0.02f), 0f, 1 << 6);

            DistanceCalcurator();
            MovementState();
            if (Mathf.Abs(_distanceX) > 1f)
            {
                Movement(_distanceX);
            }
            else
            {
                Movement(0f);
            }

            if (_distanceY > 2f && IsGround())
            {
                Jump();
            }
            else if (_distanceY < -2f && IsGround() && !_isActive)
            {
                _getGround = _isGrounded.GetComponent<PlatformEffector2D>();
                _getGround.colliderMask = _getGround.colliderMask ^ 1 << 9;
                _timer = 0f;
                _isActive = true;
            }

            if (_getGround != null && _isActive)
            {
                _timer += Time.unscaledDeltaTime;
                if (_timer > 0.6f)
                {
                    _getGround.colliderMask = _getGround.colliderMask | 1 << 9;
                    _isActive = false;
                }
            }

            if (Vector2.Distance(_transform.position, _destination.position) < 1f)
            {
                _controller.State = BossState.Attack;
            }
        }

        private void MovementState()
        {
            if (IsGround())
            {
                _controller.State = BossState.Move;
            }
            else
            {
                _controller.State = BossState.Jump;
            }
        }

        private void DistanceCalcurator()
        {
            _distanceX = _destination.position.x - _transform.position.x;
            _distanceY = _destination.position.y - _transform.position.y;

            if (_distanceX > 0)
            {
                _controller.Direction = BossDirection.Right;
            }
            else if (_distanceX < 0)
            {
                _controller.Direction = BossDirection.Left;
            }
        }

        private void Movement(float _direction)
        {
            _movement = new Vector2(_direction, 0f);
            _movement.Normalize();
            if (_controller.IsUnscaled)
            {
                _rigid.velocity = new Vector2(_movement.x * _speed, _rigid.velocity.y);
            }
            else
            {
                _rigid.velocity = new Vector2(_movement.x * _speed * Time.timeScale, _rigid.velocity.y);
            }
        }

        private bool IsGround()
        {
            return Physics2D.OverlapBox(_transform.position, new Vector2(0.5f, 0.02f), 0f, 1 << 6);
        }

        private void Jump()
        {
            _rigid.velocity = new Vector2(_rigid.velocity.x, 0);
            if (_controller.IsUnscaled)
            {
                _rigid.velocity = new Vector2(0, _jumpPower);
            }
            else
            {
                _rigid.velocity = new Vector2(0, _jumpPower * Time.timeScale);
            }

        }
    }
}
