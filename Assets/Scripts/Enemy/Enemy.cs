using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //fsm 구현 03.30 
    // 기능별로 컴포넌트 분리
    //공격 작동방식 수정
    //4.1~ 4.7
    //어느정도 틀 조작, 적을 잡기, 스테이지 넘어가기
    //동선이 너무해 버려버릴거야
    public float _moveSpeed;
    public int _hp;
    public int _damage;
    public float _attackPower;
    public float _attackInterval = 2f; // 공격 대기 시간
    public bool _canAttack = true; // 공격 가능 여부
    public float _lastAttackTime; // 마지막 공격 시간
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
            _canAttack = false; // ontrigger enter로 넘길 방법
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
        // 적을 파괴하는 코드 작성
        Destroy(gameObject);
    }
}
