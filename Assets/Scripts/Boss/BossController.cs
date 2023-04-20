using PlayerInfo;
using UnityEngine;

namespace BossInfo
{
    public class BossController : MonoBehaviour
    {
        private Player _player;
        private IBossTodo _todo;
        private Rigidbody2D _rigid;
        private BossState _currentState = BossState.None;

        public Player Player { get { return _player; } }
        public BossStatus Status;
        public BossState State;

        void Start()
        {
            _player = GameObject.FindWithTag("Player").GetComponent<Player>();
            _rigid = GetComponent<Rigidbody2D>();
            _todo = null;
            State = BossState.Idle;
        }

        void Update()
        {
            Gravity();
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
                    _todo = new BossMove(this, Status.Speed);
                    break;
                case BossState.Jump:
                    _todo = new BossJump(this, Status.JumpPower);
                    break;
                default:
                    break;
            }
        }
        private void Gravity()
        {
            _rigid.velocity = new Vector2(_rigid.velocity.x, _rigid.velocity.y + Status.Gravity * Status.GravityAccel * Time.deltaTime * Time.timeScale);
        }
    }
}