using PlayerInfo;
using UnityEngine;


public class EnemyAttack : MonoBehaviour
{
    private Rigidbody2D rigid;
    public int _damage;
    public float _moveSpeed;
    public float _attackPower;
    public float _attackInterval = 2f; // 공격 대기 시간
    public bool _canAttack = true; // 공격 가능 여부
    public float _lastAttackTime = 0; // 마지막 공격 시간
    public Player _player;
    public Animator enemyAnimator;
    private SpriteRenderer enemySprite;
    private bool _isDead = false; // 적의 사망 상태 여부
    public bool _attackin;
    private void Awake()
    {
        // 적 초기화 시 공격 대기 시간 초기화
        _lastAttackTime = _attackInterval;
        enemySprite = gameObject.GetComponent<SpriteRenderer>();
        rigid = transform.GetComponent<Rigidbody2D>();
        
    }
    // Update is called once per frame
    public void Update()
    {
        if (!_isDead)
        {
            if (_player != null && !enemyAnimator.GetCurrentAnimatorStateInfo(0).IsName("Die"))
            {
                ChasePlayer();
                AttackPlayer();
            }
            else
            {
                Collider2D Player = Physics2D.OverlapCircle(gameObject.transform.position, 6f, 1 << 7); // 
                if (Player != null)
                {
                    _player = Player.GetComponent<Player>();
                }
            }
            if (!_canAttack)
                _lastAttackTime += Time.deltaTime;
        }
        else
            enabled = false;
    }
    public void Attackin(int attackEvent) // 애니메이션 이벤트 사용
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
    private void ChasePlayer()
    {
        float distance = transform.position.x - _player.transform.position.x; // 내 포지션.x에서 플레이어포지션을 빼면 음수면 왼쪽 오른쪽이면 양수
        Vector2 dis = new Vector2(distance, 0f);
        dis.Normalize();
        if (distance < 0)
        {
            enemySprite.flipX = false;
        }
        else
        {
            enemySprite.flipX = true;
        }
        if (Mathf.Abs(distance) <= 5f && Mathf.Abs(distance) >= 1f)
        {
            rigid.velocity = new Vector2(-dis.x * _moveSpeed * Time.timeScale, rigid.velocity.y);
            enemyAnimator.SetBool("IsWalking", true);
        }
        else
        {
            enemyAnimator.SetBool("IsWalking", false);
        }
    }

    private void AttackPlayer()
    {
        Collider2D hit;
        if (!enemySprite.flipX)
        {
            hit = Physics2D.OverlapBox(transform.position + new Vector3(0.5f, 0.1f, 0), new Vector3(1, 1, 1), 0f, 1 << 7);
        }
        else
        {
            hit = Physics2D.OverlapBox(transform.position + new Vector3(-0.5f, 0.1f, 0), new Vector3(1, 1, 1), 0f, 1 << 7);
        }
        if (hit != null)
        {
            _moveSpeed = 0;
            enemyAnimator.SetBool("IsWalking", false);
            enemyAnimator.SetBool("isAttack", true);
        }
        else
        {
            _moveSpeed = 3;
            enemyAnimator.SetBool("isAttack", false);
        }
        if(_attackin)
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
