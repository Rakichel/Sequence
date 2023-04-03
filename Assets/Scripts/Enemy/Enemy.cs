using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //fsm ���� 03.30 
    // ��ɺ��� ������Ʈ �и�
    //���� �۵���� ����
    //4.1~ 4.7
    //������� Ʋ ����, ���� ���, �������� �Ѿ��
    //������ �ʹ��� ���������ž�
    public float _moveSpeed;
    public int _hp;
    public int _damage;
    public float _attackPower;
    public float _attackInterval = 2f; // ���� ��� �ð�
    public bool _canAttack = true; // ���� ���� ����
    public float _lastAttackTime; // ������ ���� �ð�
    public Player _player;

    public void Init(Transform player, float moveSpeed, int hp, int damage, float attackPower)
    {
        _player = player.GetComponent<Player>();
        _moveSpeed = moveSpeed;
        _hp = hp;
        _damage = damage;
        _attackPower = attackPower;
    }

    void Start()
    {
        _player = _player.GetComponent<Player>();
    }
    private void Update()
    {
        if (_player != null)
        {
            ChasePlayer();
            AttackPlayer();
        }
        //Physics2D.OverlapCollider(Collider2D col,)
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
        if (distance < 1f)
        {
            _lastAttackTime = Time.time;
            _canAttack = false; // ontrigger enter�� �ѱ� ���
        }
    }


    

    public int GetDamage(int damage)
    {
        int actualDamage = Mathf.Clamp((short)(damage), 0, 100);
        _hp -= actualDamage;

        if (_hp <= 0)
        {
            Die();
        }

        return actualDamage;
    }


    private void Die()
    {
        // ���� �ı��ϴ� �ڵ� �ۼ�
        Destroy(gameObject);
    }
}
