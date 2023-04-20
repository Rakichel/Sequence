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
        private BossState _currentState = BossState.None;
        private Coroutine _control;
        private Animator _animator;

        public Player Player { get { return _player; } }
        public bool IsUnscaled = false;
        public BossStatus Status;
        public BossState State;

        void Start()
        {
            _player = GameObject.FindWithTag("Player").GetComponent<Player>();
            _rigid = GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();
            _todo = null;
            State = BossState.Idle;
        }

        void Update()
        {
            Gravity();
            Unscaling();
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
                    if (_todo?.GetType().ToString() != "BossMovement")
                        _todo = new BossMovement(this, Status.Speed, Status.JumpPower);
                    break;
                default:
                    break;
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
    }
}