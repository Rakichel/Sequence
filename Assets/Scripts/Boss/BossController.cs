using PlayerInfo;
using UnityEngine;

namespace BossInfo
{
    public class BossController : MonoBehaviour
    {
        private Rigidbody2D _rigid;
        private Player _player;
        private BossState _currentState = BossState.None;
        private IBossTodo _todo;

        public BossStatus Status;

        void Start()
        {
            _player = GameObject.FindWithTag("Player").GetComponent<Player>();
            _rigid = GetComponent<Rigidbody2D>();
            _todo = null;
        }

        void Update()
        {
            Gravity();
            _todo?.Work();

            if (Status.State == _currentState)
                return;

            _currentState = Status.State;
            switch (Status.State)
            {
                case BossState.Idle:
                    _todo = new BossIdle();
                    break;
                case BossState.Move:
                    _todo = new BossMove(_rigid, transform, _player.transform, Status.Speed);
                    break;
                case BossState.Jump:
                    _todo = new BossJump(_rigid, transform, Status.JumpPower);
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