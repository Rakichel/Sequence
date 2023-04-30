using BossInfo;
using UnityEngine;

public class BossHit : IBossTodo
{
    private float _timer = 0f;
    private int _curHp;
    private int hitCount = 1;

    private BossController _controller;

    public BossHit(BossController _controller)
    {
        this._controller = _controller;
        _curHp = _controller.Status.Hp;
    }

    public void Work()
    {
        Timer();
    }

    private void Timer()
    {
        if (_controller.IsUnscaled)
        {
            _timer += Time.unscaledDeltaTime;
        }
        else
        {
            _timer += Time.deltaTime;
        }

        if (_curHp != _controller.Status.Hp)
        {
            _curHp = _controller.Status.Hp;
            _timer = 0f;
            hitCount++;
        }

        if (hitCount == 5)
        {
            _controller.State = BossState.Knockback;
        }

        if (_timer > 1f)
        {
            _controller.State = BossState.Idle;
        }
    }
}
