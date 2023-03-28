using System.Collections;
using UnityEngine;

namespace PlayerInfo
{
    [RequireComponent(typeof(Player))]
    public class PlayerAttack : MonoBehaviour
    {
        public int Power;
        public float AnimationTime = 0.5f;
        Player _player;
        void Start()
        {
            _player = GetComponent<Player>();
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape) && _player.PlayerFixedState())
            {
                StartCoroutine(Attack());
            }
        }
        IEnumerator Attack()
        {
            _player.state = PlayerState.Attack;
            Collider2D enemy;
            if (_player.direction == PlayerDirection.Right)
            {
                enemy = Physics2D.OverlapBox(transform.right, new Vector3(1f, 1f), 0f, 1 << 7);
            }
            else
            {
                enemy = Physics2D.OverlapBox(-transform.right, new Vector3(1f, 1f), 0f, 1 << 7);
            }
            if (enemy != null)
            {
                // enemy 피격 함수 호출
            }
            yield return new WaitForSeconds(AnimationTime);
            _player.state = PlayerState.Idle;
        }
        private void OnDrawGizmos()
        {
            Gizmos.color = new Color(1f, 0f, 0f, 0.5f);

            if (_player != null)
            {
                if (_player.direction == PlayerDirection.Right)
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
