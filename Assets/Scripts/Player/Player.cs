using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, ICreature
{
    public int Hp { get; private set; }
    public float moveSpeed = 5f;
    public int maxHp = 100;
    public int hp = 100;
    private int _damage;
    public float _damageReduction = 0.1f;
    public AudioClip hitSound;
    private float _attackPower;
    public AudioSource _audioSource;
    private AudioClip _hitSound;

    private Rigidbody2D _rigidbody2D;
    private SpriteRenderer _spriteRenderer;
    private Animator _animator;


    private Vector2 _moveInput;
    private bool _isMoving;
    private bool _isFacingRight = true;

    private float _lastHitTime;
    private float _hitInterval = 1f;
    private bool _canHit = true;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
    }
    void Start()
    {
        // ...
        Debug.Log($"Player HP: {hp}");
    }

    private void Update()
    {
        if (_canHit == false)
        {
            if (Time.time - _lastHitTime >= _hitInterval)
            {
                _canHit = true;
            }
        }

        _moveInput.x = Input.GetAxisRaw("Horizontal");
        _moveInput.y = Input.GetAxisRaw("Vertical");

        if (_moveInput != Vector2.zero)
        {
            _isMoving = true;
        }
        else
        {
            _isMoving = false;
        }

        _animator.SetBool("isMoving", _isMoving);

        if (_isMoving)
        {
            if (_moveInput.x > 0 && !_isFacingRight)
            {
                Flip();
            }
            else if (_moveInput.x < 0 && _isFacingRight)
            {
                Flip();
            }
        }
    }

    private void FixedUpdate()
    {
        _rigidbody2D.velocity = _moveInput * moveSpeed;
    }

    private void Flip()
    {
        _isFacingRight = !_isFacingRight;
        _spriteRenderer.flipX = !_spriteRenderer.flipX;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            HitDamage(collision.gameObject.GetComponent<Enemy>().GetDamage(10));
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Item"))
        {
            Destroy(collision.gameObject);
        }
    }

    public void HitDamage(int damage)
    {
        hp -= damage;

        _animator.SetTrigger("Hit");

        if (hp <= 0)
        {
            Die();
        }
        else
        {
            _audioSource.PlayOneShot(hitSound);
            _spriteRenderer.color = new Color(1f, 0.5f, 0.5f, 1f);
            Invoke("ResetColor", 0.1f);
        }
    }

    public void Heal(int amount)
    {
        hp += amount;
        hp = Mathf.Clamp(hp, 0, maxHp);
    }

    private void Die()
    {
        Debug.Log("Player died.");
        gameObject.SetActive(false);
        Destroy(gameObject);
    }

    public void AttackEnemy(Enemy enemy)
    {
        _damage = Mathf.RoundToInt(_attackPower * (1 - _damageReduction));
        enemy.GetDamage(_damage);
    }
}



