using UnityEngine;

public class BossMove : IBossTodo
{
    private float _distance = 0f;
    private float _speed = 8f;
    private Vector2 _movement = Vector2.one;
    private Rigidbody2D _rigid;
    private Transform _start;
    private Transform _end;

    public BossMove(Rigidbody2D _rigid, Transform _start, Transform _end, float _speed = 8f)
    {
        this._rigid = _rigid;
        this._start = _start;
        this._end = _end;
        this._speed = _speed;
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
        }
    }

    private void Movement(float _direction)
    {
        _movement = new Vector2(_direction, 0f);
        _movement.Normalize();
        _rigid.velocity = new Vector2(_movement.x * _speed * Time.timeScale, _rigid.velocity.y);
    }
}
