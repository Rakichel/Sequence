using BossInfo;
using UnityEngine;

public class BossMove : IBossTodo
{
    private float _distance = 0f;
    private Vector2 _movement = Vector2.one;
    private float _speed;
    private BossController _controller;
    private Rigidbody2D _rigid;
    private Transform _start;
    private Transform _end;
    public BossMove(BossController _controller, float _speed = 8f)
    {
        this._controller = _controller;
        this._speed = _speed;
        _rigid = _controller.GetComponent<Rigidbody2D>();
        _start = _controller.transform;
        _end = _controller.Player.transform;
    }

    public void Work()
    {
        _distance = _end.position.x - _start.position.x;
        if (Mathf.Abs(_distance) > 2f)
        {
            Movement(_distance);
        }
        else
        {
            Movement(0f);
            _controller.State = BossState.Idle;
        }
    }

    private void Movement(float _direction)
    {
        _movement = new Vector2(_direction, 0f);
        _movement.Normalize();
        _rigid.velocity = new Vector2(_movement.x * _speed * Time.timeScale, _rigid.velocity.y);
    }
}
