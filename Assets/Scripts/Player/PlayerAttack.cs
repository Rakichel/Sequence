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
        private Player _player;

        public int Power;                   // 공격력
        public float AnimTime = 0.5f;       // 공격 애니메이션이 걸리는 시간
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
        /// 플레이어가 공격하는 과정을 구현한 코루틴
        /// </summary>
        /// <returns></returns>
        private IEnumerator Attack()
        {
            // 공격 상태 전환
            _player.State = PlayerState.Attack;

            // 공격 범위 지정 및 충돌 체크
            Collider2D enemy;
            enemy = AttackAreaSelector();

            // 적 피격 시
            if (enemy != null)
            {
                // enemy 피격 함수 호출
            }
            yield return new WaitForSeconds(AnimTime);
            // 공격 후 Idle로 전환
            _player.State = PlayerState.Idle;
        }

        /// <summary>
        /// 공격 방향과 범위를 지정하고 충돌한 Collider정보를 전달하는 함수입니다.
        /// </summary>
        /// <param name="col">충돌이 감지된 Collider 정보를 저장할 변수</param>
        private Collider2D AttackAreaSelector()
        {
            // 보는 방향 기준으로 공격 방향 지정
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
