using Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMovement : IBossTodo
{
    private float _horizontal = 0f;
    private Vector2 _movement = Vector2.one;

    Rigidbody2D _rigid;
    public BossMovement()
    {
        _rigid = GameObject.Find("Boss").GetComponent<Rigidbody2D>();
    }
    public BossMovement(Rigidbody2D _rigid)
    {
        this._rigid = _rigid;
    }
    public void Work()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _rigid.velocity = new Vector2(0, 20f * Time.timeScale);
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            GameManager.Instance.TimeS = 0.2f;
            _rigid.velocity = new Vector2(_rigid.velocity.x, _rigid.velocity.y * 0.2f);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            GameManager.Instance.TimeS = 1f;
            _rigid.velocity = new Vector2(_rigid.velocity.x, _rigid.velocity.y * 5f);
        }
        Movement();
    }
    private void Movement()
    {
        _horizontal = Input.GetAxisRaw("Horizontal");
        _movement = new Vector2(_horizontal, 0f);
        _movement.Normalize();
        _rigid.velocity = new Vector2(_movement.x * 8f * Time.timeScale, _rigid.velocity.y);
    }
}
