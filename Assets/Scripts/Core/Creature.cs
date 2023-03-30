using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Player�� Enemy�� �θ� Ŭ������, �� Ŭ������ �ߺ��Ǵ� �κа� �ʿ� ������ 
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
