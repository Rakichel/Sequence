using BossInfo;
using Manager;
using System.Collections;
using UnityEngine;

namespace PlayerInfo
{

    /// <summary>
    /// 플레이어의 공격 기능을 담당하는 클래스입니다.
    /// </summary>
    [RequireComponent(typeof(Player))]
    public class PlayerAttack : MonoBehaviour
    {
        private string _fileName = "PlayerStatus";

        private Player _player;
        private Coroutine _attack;
        private Coroutine _combo;
        private float _timer;

        public int Power;                   // 공격력
        public float AnimTime;              // 공격 애니메이션이 걸리는 시간
        public float NextAttackTime;
        public GameObject Slash;

        private void Start()
        {
            LoadData();
            _player = GetComponent<Player>();
        }

        private void Update()
        {
            if (_player.State == PlayerState.Die)
            {
                StopAllCoroutines();
                return;
            }

            if (Input.GetKey(KeyCode.X) && _player.State != PlayerState.Die)
            {
                if (_player.PlayerFixedState() || _player.State == PlayerState.Dash)
                {
                    _attack = StartCoroutine(Attack());
                }
                else if (_player.PlayerFixedState() || _player.State == PlayerState.Guarding)
                {
                    _attack = StartCoroutine(Counter());
                }
            }
        }

        private IEnumerator Counter()
        {
            SoundManager.Instance.PlaySFXSound("AttackSound", 0.5f);
            // 공격 상태 전환
            _player.State = PlayerState.Counter;
            yield return new WaitForSecondsRealtime(AnimTime);
            _timer = 0;
            while (_timer < NextAttackTime)
            {
                yield return new WaitForEndOfFrame();

                if (_player.State == PlayerState.Die)
                    StopCoroutine(_attack);

                _timer = _timer + Time.unscaledDeltaTime;
                if (Input.GetKey(KeyCode.X) && _player.State == PlayerState.Counter)
                {
                    _combo = StartCoroutine(Combo());
                    StopCoroutine(_attack);
                }
                else if (Input.anyKey)
                    break;
            }

            // 공격 후 Idle로 전환
            _player.State = PlayerState.Idle;
        }

        /// <summary>
        /// 플레이어가 공격하는 과정을 구현한 코루틴
        /// </summary>
        /// <returns></returns>
        private IEnumerator Attack()
        {
            SoundManager.Instance.PlaySFXSound("AttackSound", 0.5f);
            // 공격 상태 전환
            _player.State = PlayerState.Attack;
            yield return new WaitForSecondsRealtime(AnimTime);
            _timer = 0;
            while (_timer < NextAttackTime)
            {
                yield return new WaitForEndOfFrame();

                if (_player.State == PlayerState.Die)
                    StopCoroutine(_attack);

                _timer = _timer + Time.unscaledDeltaTime;
                if (Input.GetKey(KeyCode.X) && _player.State == PlayerState.Attack)
                {
                    _combo = StartCoroutine(Combo());
                    StopCoroutine(_attack);
                }
                else if (Input.anyKey)
                    break;
            }

            // 공격 후 Idle로 전환
            _player.State = PlayerState.Idle;
        }

        private IEnumerator Combo()
        {
            SoundManager.Instance.PlaySFXSound("AttackSound", 0.5f);
            // 공격 상태 전환
            _player.State = PlayerState.Combo;
            yield return new WaitForSecondsRealtime(AnimTime);
            _timer = 0;
            while (_timer < NextAttackTime)
            {
                yield return new WaitForEndOfFrame();

                if (_player.State == PlayerState.Die)
                    StopCoroutine(_combo);

                _timer = _timer + Time.unscaledDeltaTime;
                if (Input.GetKey(KeyCode.X) && _player.State == PlayerState.Combo)
                {
                    _attack = StartCoroutine(Attack());
                    StopCoroutine(_combo);
                }
                else if (Input.anyKey)
                    break;
            }
            _player.State = PlayerState.Idle;
        }

        public void Attacking()
        {
            // 공격 범위 지정 및 충돌 체크
            Collider2D[] collider;
            collider = AttackAreaSelector();
            // 적 피격 시
            if (collider.Length > 0)
            {
                CameraManager.Instance.Impulse();
                // enemy 피격 함수 호출
                foreach (var col in collider)
                {
                    // 나중에 인터페이스로 묶어야 함
                    if (col.GetComponent<Enemy>() != null)
                    {
                        Enemy _enemy = col.GetComponent<Enemy>();
                        if (_enemy._enemyState != EnemyState.Dead)
                        {
                            if (_player.State == PlayerState.Counter)
                            {
                                _enemy.GetDamage(Power * 2);
                                if (_player.isDrain)
                                    _player.Hp += Power * 2;
                            }
                            else
                            {
                                _enemy.GetDamage(Power);
                                if (_player.isDrain)
                                    _player.Hp += Power;
                            }
                            if (_player.isShadow)
                            {
                                CreateSlash(transform.position, col.transform.position + new Vector3(0, Random.Range(-1f, 1f)));
                            }
                            CreateSlash(transform.position, col.transform.position);
                        }

                    }
                    else if (col.GetComponent<BossController>() != null)
                    {
                        BossController _boss = col.GetComponent<BossController>();
                        if (_boss.State != BossState.Die && _boss.State != BossState.Knockback)
                        {
                            if (_player.State == PlayerState.Counter)
                            {
                                _boss.GetDamage(Power * 2);
                                if (_player.isDrain)
                                    _player.Hp += Power * 2;
                            }
                            else
                            {
                                _boss.GetDamage(Power);
                                if (_player.isDrain)
                                    _player.Hp += Power;
                            }
                            if (_player.isShadow)
                            {
                                CreateSlash(transform.position, col.transform.position + new Vector3(0, Random.Range(0f, 2f)));
                            }
                            CreateSlash(transform.position, col.transform.position + new Vector3(0f, 1f));
                        }
                    }
                    else if (col.GetComponent<ArcherController>() != null)
                    {
                        ArcherController _archer = col.GetComponent<ArcherController>();
                        if (_player.State == PlayerState.Counter)
                        {
                            _archer.GetDamage(Power * 2);
                            if (_player.isDrain)
                                _player.Hp += (Power * 2 / 10);
                        }
                        else
                        {
                            _archer.GetDamage(Power);
                            if (_player.isDrain)
                                _player.Hp += Power / 10;
                        }
                        if (_player.isShadow)
                        {
                            CreateSlash(transform.position, col.transform.position + new Vector3(0, Random.Range(-1.5f, 0.5f)));
                        }
                        CreateSlash(transform.position, col.transform.position - new Vector3(0f, 0.5f));
                    }
                }
            }
        }
        private float GetAngle(Vector2 start, Vector2 end)
        {
            Vector2 v2 = end - start;
            return Mathf.Atan2(v2.y, v2.x) * Mathf.Rad2Deg;
        }

        private void CreateSlash(Vector3 from, Vector3 to)
        {
            Quaternion q = Quaternion.AngleAxis(GetAngle(from + new Vector3(0f, 1f), to), Vector3.forward);
            GameObject g = Instantiate(Slash, to + new Vector3(0f, 0f, -1f), q);
            Destroy(g, 1f);
        }

        /// <summary>
        /// 공격 방향과 범위를 지정하고 충돌한 Collider정보를 전달하는 함수입니다.
        /// </summary>
        /// <param name="col">충돌이 감지된 Collider 정보를 저장할 변수</param>
        private Collider2D[] AttackAreaSelector()
        {
            // 보는 방향 기준으로 공격 방향 지정
            if (_player.State == PlayerState.Counter)
            {
                return Physics2D.OverlapBoxAll(transform.position + new Vector3(0f, 0.75f), new Vector3(3.5f, 1.5f), 0f, 1 << 8 | 1 << 9);
            }
            else if (_player.State == PlayerState.Landed)
            {
                return Physics2D.OverlapBoxAll(transform.position + new Vector3(0f, 0.5f), new Vector3(2f, 1f), 0f, 1 << 8 | 1 << 9);
            }
            else if (_player.Direction == PlayerDirection.Right)
            {
                return Physics2D.OverlapBoxAll(transform.position + new Vector3(1f, 1f), new Vector3(1.5f, 2f), 0f, 1 << 8 | 1 << 9);
            }
            else
            {
                return Physics2D.OverlapBoxAll(transform.position + new Vector3(-1f, 1f), new Vector3(1.5f, 2f), 0f, 1 << 8 | 1 << 9);
            }
        }
        private void SaveData()
        {
            JsonManager<PlayerStatus>.Save(new PlayerStatus(), _fileName);
        }

        private void LoadData()
        {
            PlayerStatus data = JsonManager<PlayerStatus>.Load(_fileName);
            if (data != null)
            {
                Power = data.Power;
            }
            else
            {
                SaveData();
                LoadData();
            }
        }

        private void OnDrawGizmos()
        {
            if (_player != null)
            {
                Gizmos.color = new Color(1f, 0f, 0f, 0.5f);
                if (_player.Direction == PlayerDirection.Right)
                {
                    Gizmos.DrawCube(transform.position + new Vector3(1f, 1f), new Vector3(1.5f, 2f));
                }
                else
                {
                    Gizmos.DrawCube(transform.position + new Vector3(-1f, 1f), new Vector3(1.5f, 2f));
                }
                Gizmos.color = new Color(0f, 0f, 1f, 0.5f);
                Gizmos.DrawCube(transform.position + new Vector3(0f, 0.75f), new Vector3(3.5f, 1.5f));
            }
        }
    }
}
