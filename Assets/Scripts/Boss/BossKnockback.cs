using UnityEngine;

namespace BossInfo
{
    public class BossKnockback : IBossTodo
    {
        private float _timer = 0f;

        private BossController _controller;

        public BossKnockback(BossController _controller)
        {
            this._controller = _controller;
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

            if (_timer > 1f)
            {
                _controller.State = BossState.Idle;
            }
        }
    }
}

