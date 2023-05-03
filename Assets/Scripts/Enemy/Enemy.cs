using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerInfo;



public class Enemy : MonoBehaviour
{
    [SerializeField] private int _hp = 100;
    public Player _player;
    private int _damagePower;
    private float _attackSpeed;
    public Animator enemyAnimator;
    public SpriteRenderer enemySprite;
    public Rigidbody2D rigid;
    bool dieCheck = false;
    public EnemyState _enemyState;
    private float colorTimer = 1f;

    private void Awake()
    {
        rigid = transform.GetComponent<Rigidbody2D>();
        enemyAnimator = gameObject.GetComponent<Animator>();
        enemySprite = gameObject.GetComponent<SpriteRenderer>();
    }

    public void GetDamage(int damage)
    {
        Hp -= damage;
        if(Hp > 0)
        {
            colorTimer = 0f;
        }
    }

    public int Hp
    {
        set
        {
            _hp = Mathf.Clamp(value, 0, 100);
            if (_hp <= 0)
            {
                Die();
            }
        }
        get
        {
            return _hp;
        }
    }

    public int DamagePower
    {
        get
        {
            return _damagePower;
        }
        set => _damagePower = value;
    }
   
    public float AttackSpeed
    {
        get => _attackSpeed;
        set => _attackSpeed = value;
    }
    public void Update()
    {
        enemySprite.color = Color.Lerp(Color.red, Color.white, colorTimer);
        if(colorTimer < 1f)
        {
            colorTimer += 2f * Time.deltaTime;
        }
        Collider2D Player = Physics2D.OverlapCircle(gameObject.transform.position, 6f, 1 << 7); // 
        if (Player != null)
        {
            _player = Player.GetComponent<Player>();
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            Hp -= 10;
        }

        if (gameObject.GetComponent<SpriteRenderer>().material.GetFloat("_Fade") <= 0)
        {
            Manager.GameManager.Instance.KillCount++;
            Destroy(gameObject);
        }
        if (dieCheck)
        {
            Manager.GameManager.Instance.EnemyDie(gameObject.GetComponent<SpriteRenderer>().material);
        }
    }
    private void Die()
    {
        _enemyState = EnemyState.Dead;
    }
    public void DieShader()
    {
        dieCheck = true;
    }

    public bool EnemyFixedState()
    {
        // 공격 시, 대쉬 시, 맞을 시, 죽을 시 경직된 상태로 판정
        return
            (
                _enemyState != EnemyState.isAttack &&
                _enemyState != EnemyState.isHurt &&
                _enemyState != EnemyState.isStiffen &&
                _enemyState != EnemyState.Protection &&
                _enemyState != EnemyState.Shot &&
                _enemyState != EnemyState.Dead
            );
    }
}
