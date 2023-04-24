using PlayerInfo;
using UnityEngine;

namespace BossInfo
{
    public class BossAttack : IBossTodo
    {
        private BossController _controller;
        private Transform _transform;
        private int _power;
        private float _timer;
        private bool _isAttack;
        public BossAttack(BossController _controller)
        {
            this._controller = _controller;
            _transform = _controller.transform;
            _power = _controller.Status.Power;
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

            if (_timer > 0.25f && !_isAttack)
            {
                Attack();
                _isAttack = true;
            }

            if (_timer > 0.8f)
            {
                NextState();
                _isAttack = false;
                _timer = 0f;
            }
        }

        public void Attack()
        {
            Collider2D _collider = AttackAreaSelector();
            _collider?.GetComponent<PlayerHit>().GetDamage(_power);
        }

        public void NextState()
        {
            Collider2D _collider = AttackAreaSelector();
            if (_collider == null)
            {
                _controller.State = BossState.Idle;
            }
            else
            {
                if (_controller.State == BossState.Attack)
                {
                    _controller.State = BossState.Combo;
                }
                else if (_controller.State == BossState.Combo)
                {
                    _controller.State = BossState.Attack;
                }
            }
        }

        private Collider2D AttackAreaSelector()
        {
            // 보는 방향 기준으로 공격 방향 지정
            if (_controller.Direction == BossDirection.Right)
            {
                return Physics2D.OverlapBox(_transform.position + new Vector3(0.75f, 0.75f), new Vector3(1f, 1f), 0f, 1 << 7);
            }
            else
            {
                return Physics2D.OverlapBox(_transform.position + new Vector3(-0.75f, 0.75f), new Vector3(1f, 1f), 0f, 1 << 7);
            }
        }
    }
}
