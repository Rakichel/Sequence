using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimator : MonoBehaviour
{
    Enemy _enemy;
    Animator _enemyAnimaotr;
    EnemyState _enemyState;

    private void Awake()
    {
        _enemy = gameObject.GetComponent<Enemy>();
        _enemyAnimaotr = gameObject.GetComponent<Animator>();
        _enemyState = _enemy._enemyState;
    }
    void Update()
    {
        if (_enemyState == _enemy._enemyState)
            return;

        if (_enemy._enemyState != EnemyState.Idle)
            _enemyAnimaotr.SetBool(_enemy._enemyState.ToString(), true);

        if (_enemyState != EnemyState.Idle)
            _enemyAnimaotr.SetBool(_enemyState.ToString(), false);

        _enemyState = _enemy._enemyState;
    }
}
