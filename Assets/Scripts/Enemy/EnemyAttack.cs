using PlayerInfo;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public int _damage;
    public float _attackPower;
    [SerializeField] private Enemy _enemy;
    public bool _attackin;
    private void Awake()
    {
        // 적 초기화 시 공격 대기 시간 초기화
        _enemy = gameObject.GetComponent<Enemy>();
    }
    // Update is called once per frame
    private void Update()
    {
        if(_enemy._enemyState != EnemyState.Dead)
        {
            AttackPlayer();
        }
    }
    public void AttackIn(int attackEvent) // 애니메이션 이벤트 사용
    {
        if (attackEvent == 0)
        {
            _attackin = true;
        }
        else
        {
            _attackin = false;
        }
    }
    private void AttackPlayer()
    {
        Collider2D hit;
        if (!_enemy.enemySprite.flipX)
        {
            hit = Physics2D.OverlapBox(transform.position + new Vector3(0.5f, 0.1f, 0), new Vector3(1, 1, 1), 0f, 1 << 7);
        }
        else
        {
            hit = Physics2D.OverlapBox(transform.position + new Vector3(-0.5f, 0.1f, 0), new Vector3(1, 1, 1), 0f, 1 << 7);
        }
        if (hit != null)
        {
            _enemy._enemyState = EnemyState.isAttack;
        }
        if (_attackin)
        {
            if (hit != null)
                hit.GetComponent<PlayerHit>().GetDamage(_damage);
            _attackin = false;
        }
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position + new Vector3(0.5f, 0.1f, 0), new Vector3(1, 1, 1));
    }
}
