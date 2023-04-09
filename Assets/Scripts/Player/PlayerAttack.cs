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

        private void Start()
        {
            _player = GetComponent<Player>();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.X) && _player.PlayerFixedState())
            {
                _attack = StartCoroutine(Attack());
            }
            else if (Input.GetKeyDown(KeyCode.X) && _player.State == PlayerState.Dash)
            {
                _attack = StartCoroutine(Attack());
            }
        }

        /// <summary>
        /// �÷��̾ �����ϴ� ������ ������ �ڷ�ƾ
        /// </summary>
        /// <returns></returns>
        private IEnumerator Attack()
        {
            // ���� ���� ��ȯ
            _player.State = PlayerState.Attack;

            // ���� ���� ���� �� �浹 üũ
            Collider2D collider;
            collider = AttackAreaSelector();
            // �� �ǰ� ��
            if (collider != null)
            {
                // enemy �ǰ� �Լ� ȣ��
                if (collider.GetComponent<Enemy>() != null)
                {
                    Enemy enemy = collider.GetComponent<Enemy>();
                    enemy.GetDamage(Power);
                }
            }
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

            // ���� ���� ���� �� �浹 üũ
            Collider2D collider;
            collider = AttackAreaSelector();
            // �� �ǰ� ��
            if (collider != null)
            {
                // enemy �ǰ� �Լ� ȣ��
                if (collider.GetComponent<Enemy>() != null)
                {
                    Enemy enemy = collider.GetComponent<Enemy>();
                    enemy.GetDamage(Power);
                }
            }
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

        /// <summary>
        /// ���� ����� ������ �����ϰ� �浹�� Collider������ �����ϴ� �Լ��Դϴ�.
        /// </summary>
        /// <param name="col">�浹�� ������ Collider ������ ������ ����</param>
        private Collider2D AttackAreaSelector()
        {
            // ���� ���� �������� ���� ���� ����
            if (_player.Direction == PlayerDirection.Right)
            {
                return Physics2D.OverlapBox(transform.position + new Vector3(0.75f, 0.75f), new Vector3(1f, 1f), 0f, 1 << 7);
            }
            else
            {
                return Physics2D.OverlapBox(transform.position + new Vector3(-0.75f, 0.75f), new Vector3(1f, 1f), 0f, 1 << 7);
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = new Color(1f, 0f, 0f, 0.5f);

            if (_player != null)
            {
                if (_player.Direction == PlayerDirection.Right)
                {
                    Gizmos.DrawCube(transform.position + new Vector3(0.75f, 0.75f), new Vector3(1f, 1f));
                }
                else
                {
                    Gizmos.DrawCube(transform.position + new Vector3(-0.75f, 0.75f), new Vector3(1f, 1f));
                }
            }
        }
    }
}
