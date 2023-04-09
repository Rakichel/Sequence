using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEnum : MonoBehaviour
{
    Animator animator;
   
    
    public SpriteRenderer _enemySpriteRenderer;
    

    enum EnemyDirection { Left, Right }
    enum EnemyState
    {
        Idle = 0,
        Attack,
        Hurt,
        Walk,
        Run,
        Protection,
        Dead

    }
    EnemyState _enemyState = EnemyState.Idle;
    // Start is called before the first frame update
    private void Awake()
    {
        animator = GetComponent<Animator>();
        _enemySpriteRenderer = GetComponent<SpriteRenderer>();
        
    }

    // Update is called once per frame
    void Update() // 
    {

    }
    public void Idle() // 코루틴 공격도중에 판정을 
    {
        _enemyState = EnemyState.Idle;
    }
    public void Attack()
    {
        _enemyState = EnemyState.Attack;
    }
    public void Hurt()
    {
        _enemyState = EnemyState.Hurt;
    }
    public void Walk()
    {
        _enemyState = EnemyState.Walk;
    }
    public void Protection()
    {
        _enemyState = EnemyState.Protection;
    }
    public void Dead()
    {
        _enemyState = EnemyState.Dead;
    }
}
