using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class EnemyAttack : MonoBehaviour
{
    public int _damage;
    public float _moveSpeed;
    public float _attackPower;
    public float _attackInterval = 2f; // ���� ��� �ð�
    public bool _canAttack = true; // ���� ���� ����
    public float _lastAttackTime; // ������ ���� �ð�
    public Player _player;

    // Start is called before the first frame update
    void Start()
    {
        _player = _player.GetComponent<Player>();
    }

    // Update is called once per frame


    public void Update()
    {
        if (_player != null)
        {
            ChasePlayer();
            AttackPlayer();
        }
    }

    private void ChasePlayer()
    {
        float distance = Vector2.Distance(transform.position, _player.transform.position);
        if (distance < 5f)
        {
            transform.position = Vector2.MoveTowards(transform.position, _player.transform.position, _moveSpeed * Time.deltaTime);
        }
    }

    private void AttackPlayer()
    {
        if (!_canAttack)
        {
            if (Time.time - _lastAttackTime >= _attackInterval)
            {
                _canAttack = true;
            }
            return;
        }

        float distance = Vector2.Distance(transform.position, _player.transform.position);
        Collider2D hit = Physics2D.OverlapBox(transform.position, new Vector2(1, 1), 0);
        if (distance < 1f & hit)
        {
            //_player.HitDamage(_damage);

            _lastAttackTime = Time.time;
            _canAttack = false;
        }
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector2(1,1));
    }
}
