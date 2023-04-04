using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICreature
{
    int Hp { get; }
    //int Damage { get; }
    //float MoveSpeed { get; }
    //float AttackSpeed { get; }
    //float DamageReduction { get; }

    //void Move();
    //void Attack();
    //void Guard(int damage);
    void HitDamage(int damage);
}
