using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Player와 Enemy의 부모 클래스로, 두 클래스의 중복되는 부분과 필요 구현을 
/// </summary>
abstract class Creature : MonoBehaviour
{
    #region Members
    private int _hp;
    private int _damage;
    private float _moveSpeed;
    private float _attackSpeed;
    private float _damageReduction = 0.6f;
    #endregion

    #region Property
    public int Hp { set { } get { return _hp; } }
    public int Damage { get { return _damage; } }
    public float MoveSpeed { get { return _moveSpeed; } }
    public float AttackSpeed { get { return _attackSpeed; } }
    public float DamageReduction { get { return _damageReduction; } }
    #endregion

    

    protected abstract void Move();
    protected abstract void Attack();
    protected virtual void Guard(int damage)
    {
        _hp -= Mathf.Clamp((short)(damage *_damageReduction), 0, 100);
    }
    protected virtual void HitDamage(int damage)
    {
        _hp -= damage;
    }

}
