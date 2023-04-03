using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public Player _player;
    public float _attackPower;
    public float _attackInterval = 2f;
    public bool _canAttack = true;
    public float _lastAttackTime;
    public float _moveSpeed;
    // Start is called before the first frame update
    void Start()
    {
        _player = _player.GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
       // physics 2d중심점 .overlapbox 
       // onDrawGizmos
        
    }
    
}
