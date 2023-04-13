using Manager;
using System.Collections;
using UnityEngine;

namespace PlayerInfo
{

    /// <summary>
    /// �÷��̾��� ���� ����� ����ϴ� Ŭ�����Դϴ�.
    /// </summary>
    [RequireComponent(typeof(Player))]
    public class PlayerAttack : MonoBehaviour
    {
        private Player _player;
        private Coroutine _attack;
        private Coroutine _combo;
        private float _timer;

        public int Power;                   // ���ݷ�
        public float AnimTime;              // ���� �ִϸ��̼��� �ɸ��� �ð�
        public float NextAttackTime;
        public GameObject Slash;

        private void Start()
        {
            _player = GetComponent<Player>();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.X))
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
            // ���� ���� ��ȯ
            _player.State = PlayerState.Counter;
            yield return new WaitForSecondsRealtime(AnimTime);
            _timer = 0;
            while (_timer < NextAttackTime)
            {
                yield return new WaitForEndOfFrame();
                _timer = _timer + Time.unscaledDeltaTime;
                if (Input.GetKey(KeyCode.X))
                {
                    _combo = StartCoroutine(Combo());
                    StopCoroutine(_attack);
                }
                else if (Input.anyKey)
                    break;
            }

            // ���� �� Idle�� ��ȯ
            _player.State = PlayerState.Idle;
        }

        /// <summary>
        /// �÷��̾ �����ϴ� ������ ������ �ڷ�ƾ
        /// </summary>
        /// <returns></returns>
        private IEnumerator Attack()
        {
            // ���� ���� ��ȯ
            _player.State = PlayerState.Attack;
            yield return new WaitForSecondsRealtime(AnimTime);
            _timer = 0;
            while (_timer < NextAttackTime)
            {
                yield return new WaitForEndOfFrame();
                _timer = _timer + Time.unscaledDeltaTime;
                if (Input.GetKey(KeyCode.X))
                {
                    _combo = StartCoroutine(Combo());
                    StopCoroutine(_attack);
                }
                else if (Input.anyKey)
                    break;
            }

            // ���� �� Idle�� ��ȯ
            _player.State = PlayerState.Idle;
        }

        private IEnumerator Combo()
        {
            // ���� ���� ��ȯ
            _player.State = PlayerState.Combo;
            yield return new WaitForSecondsRealtime(AnimTime);
            _timer = 0;
            while (_timer < NextAttackTime)
            {
                yield return new WaitForEndOfFrame();
                _timer = _timer + Time.unscaledDeltaTime;
                if (Input.GetKey(KeyCode.X))
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
            // ���� ���� ���� �� �浹 üũ
            Collider2D[] collider;
            collider = AttackAreaSelector();
            // �� �ǰ� ��
            if (collider.Length > 0)
            {
                CameraManager.Instance.Impulse();
                // enemy �ǰ� �Լ� ȣ��
                foreach (var col in collider)
                {
                    if (col.GetComponent<Enemy>() != null)
                    {
                        col.GetComponent<Enemy>().GetDamage(Power);
                        Quaternion q = Quaternion.AngleAxis(GetAngle(transform.position, col.transform.position), Vector3.forward);
                        GameObject g = Instantiate(Slash, col.transform.position, q);
                        Destroy(g, 1f);
                    }
                }
            }
        }
        float GetAngle(Vector2 start, Vector2 end)
        {
            Vector2 v2 = end - start;
            return Mathf.Atan2(v2.y, v2.x) * Mathf.Rad2Deg;
        }

        /// <summary>
        /// ���� ����� ������ �����ϰ� �浹�� Collider������ �����ϴ� �Լ��Դϴ�.
        /// </summary>
        /// <param name="col">�浹�� ������ Collider ������ ������ ����</param>
        private Collider2D[] AttackAreaSelector()
        {
            // ���� ���� �������� ���� ���� ����
            if (_player.State == PlayerState.Counter)
            {
                return Physics2D.OverlapBoxAll(transform.position + new Vector3(0f, 0.5f), new Vector3(2f, 1f), 0f, 1 << 8);
            }
            else if(_player.State == PlayerState.Landed)
            {
                return Physics2D.OverlapBoxAll(transform.position + new Vector3(0f, 0.5f), new Vector3(2f, 1f), 0f, 1 << 8);
            }
            else if (_player.Direction == PlayerDirection.Right)
            {
                return Physics2D.OverlapBoxAll(transform.position + new Vector3(0.75f, 0.75f), new Vector3(1f, 1f), 0f, 1 << 8);
            }
            else
            {
                return Physics2D.OverlapBoxAll(transform.position + new Vector3(-0.75f, 0.75f), new Vector3(1f, 1f), 0f, 1 << 8);
            }
        }

        private void OnDrawGizmos()
        {
            if (_player != null)
            {
                Gizmos.color = new Color(1f, 0f, 0f, 0.5f);
                if (_player.Direction == PlayerDirection.Right)
                {
                    Gizmos.DrawCube(transform.position + new Vector3(0.75f, 0.5f), new Vector3(1f, 1f));
                }
                else
                {
                    Gizmos.DrawCube(transform.position + new Vector3(-0.75f, 0.5f), new Vector3(1f, 1f));
                }
                Gizmos.color = new Color(0f, 0f, 1f, 0.5f);
                Gizmos.DrawCube(transform.position + new Vector3(0f, 0.5f), new Vector3(2f, 1f));
            }
        }
    }
}
