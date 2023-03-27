using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private float _moveSpeed;
    private int _hp;
    private int _damage;
    private float _attackPower;
    private AudioSource _audioSource;
    private AudioClip _hitSound;
    private float _attackInterval = 2f; // 공격 대기 시간
    private bool _canAttack = true; // 공격 가능 여부
    private float _lastAttackTime; // 마지막 공격 시간
    private Player _player;

    public void Init(Transform player, float moveSpeed, int hp, int damage, float attackPower, AudioSource audioSource, AudioClip hitSound)
    {
        _player = player.GetComponent<Player>();
        _moveSpeed = moveSpeed;
        _hp = hp;
        _damage = damage;
        _attackPower = attackPower;
        _audioSource = audioSource;
        _hitSound = hitSound;
    }
    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

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
        if (distance < 1f)
        {
            _player.HitDamage(_damage);

            _lastAttackTime = Time.time;
            _canAttack = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<Player>().HitDamage(_damage);
        }
    }

    public int GetDamage(int damage)
    {
        int actualDamage = Mathf.Clamp((short)(damage), 0, 100);
        _hp -= actualDamage;
        _audioSource.PlayOneShot(_hitSound);

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
