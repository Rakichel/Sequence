using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerInfo;

public class ESamurai_CAttack : MonoBehaviour
{
    public int _damage;
    public float _moveSpeed;
    public float _attackPower;
    public float _attackInterval = 2f; // ���� ��� �ð�
    public bool _canAttack = true; // ���� ���� ����
    public float _lastAttackTime = 0; // ������ ���� �ð�
    public Player _player;
    public PlayerHit playerHit;
    public Animator ESamuraiAnimator;

    private void Awake()
    {
        // �� �ʱ�ȭ �� ���� ��� �ð� �ʱ�ȭ
        _lastAttackTime = _attackInterval;
        ESamuraiAnimator = GetComponent<Animator>(); // enemyAnimator ���� �ʱ�ȭ
    }
    
    public void Update()
    {
        if (_player != null)
        {
            CChasePlayer();
            CAttackPlayer();
        }
        else
        {
            Collider2D Player = Physics2D.OverlapCircle(gameObject.transform.position, 6f, 1 << 7); // 
            if (Player != null)
            {
                _player = Player.GetComponent<Player>();
                playerHit = Player.GetComponent<PlayerHit>();
            }
        }
        if (!_canAttack)
            _lastAttackTime += Time.deltaTime;
    }
    private void CChasePlayer()
    {
        float distance = Vector2.Distance(transform.position, _player.transform.position);
        if (distance < 5f)
        {
            transform.position = Vector2.MoveTowards(transform.position, _player.transform.position, _moveSpeed * Time.deltaTime);
            ESamuraiAnimator.SetBool("Walk", true);
        }
        else
            ESamuraiAnimator.SetBool("Walk", false);
        
    }

    private void CAttackPlayer()
    {
        if (!_canAttack) // ������ �������� �ʴٸ�

        {
            ESamuraiAnimator.SetBool("Walk", false);


            if (_lastAttackTime >= _attackInterval) // ���� ������ ������ ���ݽð����� ũ�ų� ���ٸ� 
            {
                _canAttack = true;
                //enemyAnimator.SetTrigger("Attack");
                _lastAttackTime = 0;
            }

            return;
        }


        float distance = Vector2.Distance(transform.position, _player.transform.position);
        Collider2D hit = Physics2D.OverlapBox(transform.position + new Vector3(0.5f, 0.1f, 0), new Vector3(1, 1, 1), 0f, 1 << 7);
        if (transform.position.x < _player.transform.position.x)
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }
        else
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
        if (hit)
        {
            
            ESamuraiAnimator.SetBool("Attack", true);
            playerHit.GetDamage(_damage);
            _canAttack = false;

        }
        else
        {
            ESamuraiAnimator.SetBool("Attack", false);
        }


    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position + new Vector3(0.5f, 0.1f, 0), new Vector3(1, 1, 1));
    }

}

    
    
