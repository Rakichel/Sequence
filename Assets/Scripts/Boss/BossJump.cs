using UnityEngine;

namespace BossInfo
{
    public class BossJump : IBossTodo
    {
        private BossController _controller;
        private Rigidbody2D _rigid;
        private Transform _transform;
        private float _timeScale;
        private float _jumpPower;
        private bool isJump;
        public BossJump(BossController _controller, float _jumpPower = 20f)
        {
            this._controller = _controller;
            this._jumpPower = _jumpPower;
            _rigid = _controller.GetComponent<Rigidbody2D>();
            _transform = _controller.transform;
            _timeScale = 1f;
        }

        public void Work()
        {
            VelocityControl();

            if (IsGround() && !isJump)
            {
                Jump();
                isJump = true;
            }
            else if(IsGround() && isJump) 
            {
                _controller.State = BossState.Idle;
            }
        }

        private void VelocityControl()
        {
            if (_timeScale != Time.timeScale)
            {
                if (Time.timeScale == 1f)
                    _rigid.velocity = new Vector2(_rigid.velocity.x, _rigid.velocity.y * 5f);
                else
                    _rigid.velocity = new Vector2(_rigid.velocity.x, _rigid.velocity.y / 5f);
                _timeScale = Time.timeScale;
            }
        }

        private bool IsGround()
        {
            return Physics2D.OverlapBox(_transform.position, new Vector2(0.5f, 0.02f), 0f, 1 << 6);
        }

        private void Jump()
        {
            _rigid.velocity = new Vector2(_rigid.velocity.x, 0);
            _rigid.velocity = new Vector2(0, _jumpPower * Time.timeScale);
        }
    }
}
