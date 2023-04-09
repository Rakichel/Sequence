using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using PlayerInfo;


public class EnemyAttack : MonoBehaviour
{
    public int _damage;
    public float _moveSpeed;
    public float _attackPower;
    public float _attackInterval = 2f; // ���� ��� �ð�
    public bool _canAttack = true; // ���� ���� ����
    public float _lastAttackTime = 0; // ������ ���� �ð�
    public Player _player;
    public PlayerHit playerHit;
    public Animator enemyAnimator;
    public LayerMask playerLayer;


    // Update is called once per frame


    public void Update()
    {
        if (_player != null)
        {
            ChasePlayer();
            AttackPlayer();
        }
        if(!_canAttack)
            _lastAttackTime += Time.deltaTime;
    }

    private void ChasePlayer()
    {
        float distance = Vector2.Distance(transform.position, _player.transform.position);
        if (distance < 5f)
        {
            transform.position = Vector2.MoveTowards(transform.position, _player.transform.position, _moveSpeed * Time.deltaTime);
            enemyAnimator.SetBool("IsWalking", true);
        }
        
    }

    private void AttackPlayer()
    {
        if (!_canAttack) // ������ �������� �ʴٸ�

        {
            enemyAnimator.SetBool("IsWalking", false);
           
        
        if (_lastAttackTime >= _attackInterval) // ���� ������ ������ ���ݽð����� ũ�ų� ���ٸ� 
            {
                _canAttack = true;
                enemyAnimator.SetTrigger("Attack");
                _lastAttackTime = 0;
            }
            
            return;
        }
   

        float distance = Vector2.Distance(transform.position, _player.transform.position);
        Collider2D hit = Physics2D.OverlapBox(transform.position + new Vector3(0.5f, 0.1f, 0), new Vector3(1, 1, 1), 1<<7);
        if (distance < 1f && hit)
        {
            enemyAnimator.SetTrigger("Attack");
            playerHit.GetDamage(_damage);
            _canAttack = false;
            
        }
        else 
        {
            enemyAnimator.ResetTrigger("Attack");
        }
            
        
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position + new Vector3(0.5f, 0.1f, 0), new Vector3(1, 1, 1));
    }
}
