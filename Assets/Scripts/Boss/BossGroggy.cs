using UnityEngine;

namespace BossInfo
{
    public class BossGroggy : IBossTodo
    {
        private BossController _controller;
        private float _timer = 0f;
        public BossGroggy(BossController _controller)
        {
            this._controller = _controller;
        }

        public void Work()
        {
            if (_controller.IsUnscaled)
            {
                _timer += Time.unscaledDeltaTime;
            }
            else
            {
                _timer += Time.deltaTime;
            }

            if (_timer > 3f)
            {
                _timer = 0f;
                _controller.Status.Posture = 5;
                _controller.State = BossState.Idle;
            }
        }
    }
}
