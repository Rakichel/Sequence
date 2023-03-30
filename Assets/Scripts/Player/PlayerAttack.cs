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

        public int Power;                   // ���ݷ�
        public float AnimTime = 0.5f;       // ���� �ִϸ��̼��� �ɸ��� �ð�
        private void Start()
        {
            _player = GetComponent<Player>();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.LeftControl) && _player.PlayerFixedState())
            {
                StartCoroutine(Attack());
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
            Collider2D enemy;
            enemy = AttackAreaSelector();

            // �� �ǰ� ��
            if (enemy != null)
            {
                // enemy �ǰ� �Լ� ȣ��
            }
            yield return new WaitForSeconds(AnimTime);
            // ���� �� Idle�� ��ȯ
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
                return Physics2D.OverlapBox(transform.right, new Vector3(1f, 1f), 0f, 1 << 7);
            }
            else
            {
                return Physics2D.OverlapBox(-transform.right, new Vector3(1f, 1f), 0f, 1 << 7);
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = new Color(1f, 0f, 0f, 0.5f);

            if (_player != null)
            {
                if (_player.Direction == PlayerDirection.Right)
                {
                    Gizmos.DrawCube(transform.position + Vector3.right, new Vector3(1f, 1f));
                }
                else
                {
                    Gizmos.DrawCube(transform.position + Vector3.left, new Vector3(1f, 1f));
                }
            }
        }
    }
}
