using BossInfo;
using UnityEngine;

public class BossMovement : IBossTodo
{
    private float _distanceX = 0f;
    private float _distanceY = 0f;
    private Vector2 _movement = Vector2.one;

    private BossController _controller;
    private float _jumpPower;
    private float _speed;
    private Rigidbody2D _rigid;
    private Transform _my;
    private Transform _destination;


    public BossMovement(BossController _controller, float _speed = 8f, float _jumpPower = 20f)
    {
        this._controller = _controller;
        this._jumpPower = _jumpPower;
        this._speed = _speed;
        _rigid = _controller.GetComponent<Rigidbody2D>();
        _my = _controller.transform;
        _destination = _controller.Player.transform;
    }

    public void Work()
    {
        DistanceCalcurator();
        MovementState();
        if (Mathf.Abs(_distanceX) > 2f)
        {
            Movement(_distanceX);
        }
        else
        {
            Movement(0f);
            _controller.State = BossState.Idle;
        }

        if (_distanceY > 2f && IsGround())
        {
            Jump();
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
        _distanceX = _destination.position.x - _my.position.x;
        _distanceY = _destination.position.y - _my.position.y;
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
        return Physics2D.OverlapBox(_my.position, new Vector2(0.5f, 0.02f), 0f, 1 << 6);
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
