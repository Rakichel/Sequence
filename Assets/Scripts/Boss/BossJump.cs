using UnityEngine;

namespace BossInfo
{
    public class BossJump : IBossTodo
    {
        Rigidbody2D _rigid;
        float _timeScale;
        float _jumpPower;
        Transform _transform;

        public BossJump(Rigidbody2D _rigid, Transform _transform, float _jumpPower = 20f)
        {
            this._rigid = _rigid;
            this._transform = _transform;
            this._jumpPower = _jumpPower;
            _timeScale = 1f;
        }

        public void Work()
        {
            VelocityControl();

            if (IsGround())
            {
                Jump();
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
            return Physics2D.OverlapBox(_transform.position + Vector3.down / 2f, new Vector2(0.5f, 0.02f), 0f, 1 << 6);
        }

        private void Jump()
        {
            _rigid.velocity = new Vector2(0, _jumpPower * Time.timeScale);
        }
    }
}
