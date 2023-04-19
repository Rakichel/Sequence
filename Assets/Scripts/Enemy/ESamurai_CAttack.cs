using PlayerInfo;
using UnityEngine;

public class ESamurai_CAttack : MonoBehaviour
{
    private Rigidbody2D rigid;
    public int _damage;
    public float _moveSpeed;
    public float _attackPower;
    private Player _player;
    public Animator ESenemyAnimator;
    private SpriteRenderer ESEnemySprite;
    private bool _isDead = false; // ���� ��� ���� ����
    public bool _attackin;
    private void Awake()
    {
        ESEnemySprite = gameObject.GetComponent<SpriteRenderer>();
        rigid = transform.GetComponent<Rigidbody2D>();
    }
    // Update is called once per frame
    public void Update()
    {
        if (!_isDead)
        {
            if (_player != null && !ESenemyAnimator.GetCurrentAnimatorStateInfo(0).IsName("Die1"))
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
        }
    }
    public void AttackIn(int attackEvent) // �ִϸ��̼� �̺�Ʈ ���
    {
        if (attackEvent == 0)
        {
            _attackin = true;
        }
        else if (attackEvent == 1)
        {
            _attackin = false;
        }
    }
    private void ChasePlayer()
    {
        float distance = transform.position.x - _player.transform.position.x; // �� ������.x���� �÷��̾��������� ���� ������ ���� �������̸� ���
        Vector2 dis = new Vector2(distance, 0f);
        dis.Normalize();
        if (distance < 0)
        {
            ESEnemySprite.flipX = false;
        }
        else
        {
            ESEnemySprite.flipX = true;
        }
        if (Mathf.Abs(distance) <= 5f && Mathf.Abs(distance) >= 1f)
        {
            rigid.velocity = new Vector2(-dis.x * _moveSpeed * Time.timeScale, rigid.velocity.y);
            ESenemyAnimator.SetBool("isWalking", true);
        }
        else
        {
            ESenemyAnimator.SetBool("isWalking", false);
        }
    }

    private void AttackPlayer()
    {
        Collider2D hit;
        if (!ESEnemySprite.flipX)
        {
            hit = Physics2D.OverlapBox(transform.position + new Vector3(1f, 0.1f, 0), new Vector3(1, 2, 1), 0f, 1 << 7);
        }
        else
        {
            hit = Physics2D.OverlapBox(transform.position + new Vector3(-1f, 0.1f, 0), new Vector3(1, 2, 1), 0f, 1 << 7);
        }
        if (hit != null)
        {
            _moveSpeed = 0;
            ESenemyAnimator.SetBool("isWalking", false);
            ESenemyAnimator.SetBool("isAttack", true);
        }
        else
        {
            _moveSpeed = 2.5f;
            ESenemyAnimator.SetBool("isAttack", false);
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
        Gizmos.DrawWireCube(transform.position + new Vector3(1f, 0.1f, 0), new Vector3(1, 2, 1));
    }
}



