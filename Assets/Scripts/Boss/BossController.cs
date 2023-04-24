using PlayerInfo;
using System.Collections;
using UnityEngine;

namespace BossInfo
{
    public class BossController : MonoBehaviour
    {
        private Player _player;
        private IBossTodo _todo;
        private Rigidbody2D _rigid;
        private Coroutine _control;
        private Animator _animator;
        private SpriteRenderer _spriteRenderer;
        private BossState _currentState = BossState.None;

        public Player Player
        {
            get
            {
                if (_player == null)
                    _player = GameObject.FindWithTag("Player").GetComponent<Player>();
                return _player;
            }
        }
        public bool IsUnscaled = false;
        public BossDirection Direction;
        public BossStatus Status;
        public BossState State;

        void Start()
        {
            _rigid = GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _todo = null;
            State = BossState.Idle;
            Direction = BossDirection.Left;
        }

        void Update()
        {
            Gravity();
            Unscaling();
            LookDirection();
            _todo?.Work();

            if (State == _currentState)
                return;

            _currentState = State;
            switch (State)
            {
                case BossState.Idle:
                    _todo = new BossIdle(this);
                    break;
                case BossState.Move:
                case BossState.Jump:
                    if (_todo?.GetType().ToString() != "BossInfo.BossMovement")
                    {
                        _todo = new BossMovement(this, Status.Speed, Status.JumpPower);
                    }
                    break;
                case BossState.Attack:
                case BossState.Combo:
                    if (_todo?.GetType().ToString() != "BossInfo.BossAttack")
                    {
                        _todo = new BossAttack(this);
                    }
                    break;
                case BossState.Hit:
                    _todo = new BossHit(this);
                    break;
                case BossState.Knockback:
                    _todo = new BossKnockback(this);
                    break;
                case BossState.Die:
                    _todo = new BossDie();
                    break;
                default:
                    break;
            }
        }

        public void GetDamage(int _damage)
        {
            if (State == BossState.Knockback)
                return;

            Status.Hp -= _damage;
            if (Status.Hp <= 0)
            {
                State = BossState.Die;
            }
            else
            {
                State = BossState.Hit;
            }
        }

        IEnumerator VelocityControl()
        {
            _rigid.velocity = new Vector2(_rigid.velocity.x, _rigid.velocity.y / 5f);
            _animator.updateMode = AnimatorUpdateMode.Normal;
            yield return new WaitForSecondsRealtime(1f);
            IsUnscaled = true;
            _rigid.velocity = new Vector2(_rigid.velocity.x, _rigid.velocity.y * 5f);
            _animator.updateMode = AnimatorUpdateMode.UnscaledTime;
        }
        private void Unscaling()
        {
            if (Time.timeScale != 1f)
            {
                if (_control == null)
                    _control = StartCoroutine(VelocityControl());
            }
            else
            {
                if (_control != null)
                    _control = null;
                IsUnscaled = false;
                _animator.updateMode = AnimatorUpdateMode.Normal;
            }
        }
        private void Gravity()
        {
            if (IsUnscaled)
            {
                _rigid.velocity = new Vector2(_rigid.velocity.x, _rigid.velocity.y + Status.Gravity * Status.GravityAccel * Time.unscaledDeltaTime);
            }
            else
            {
                _rigid.velocity = new Vector2(_rigid.velocity.x, _rigid.velocity.y + Status.Gravity * Status.GravityAccel * Time.deltaTime * Time.timeScale);
            }
        }
        private void LookDirection()
        {
            if (Direction == BossDirection.Left)
                _spriteRenderer.flipX = true;
            else if (Direction == BossDirection.Right)
                _spriteRenderer.flipX = false;
        }
    }
}