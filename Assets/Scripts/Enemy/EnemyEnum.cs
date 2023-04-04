using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEnum : MonoBehaviour
{
    Animator animator;
   
    private bool _isEnemyRight = true;
    public SpriteRenderer _enemySpriteRenderer;
    Rigidbody2D rb;
    enum EnemyState
    {
        Idle,
        Attack,
        Hurt,
        Walk,
        Run,
        Protection,
        Dead

    }
    EnemyState _enemyState = EnemyState.Idle;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        _enemySpriteRenderer = GetComponent<SpriteRenderer>();
        
    }

    // Update is called once per frame
    void Update() // 
    {
        
        if (_enemySpriteRenderer.flipX = rb.velocity.magnitude > 0 && rb.velocity.x < 0)
        {
            _enemySpriteRenderer.flipX = true;
        }
        
    }
    public void Idle() // 코루틴 공격도중에 판정을 
    {
        throw new System.NotImplementedException();
    }
    public void Attack()
    {
        throw new System.NotImplementedException();
    }
    public void Hurt()
    {
        throw new System.NotImplementedException();
    }
    public void Walk()
    {
        throw new System.NotImplementedException();
    }
    public void Protection()
    {
        throw new System.NotImplementedException();
    }
    public void Dead()
    {
        throw new System.NotImplementedException();
    }
}
