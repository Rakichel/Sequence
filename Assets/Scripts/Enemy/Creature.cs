using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creature
{
    public enum EnemyType
    {
        Humanoid,
        Beast,
        Demon
    }

    public enum AttackType
    {
        Physical,
        Magical
    }

    public enum State
    {
        Idle,
        Attacking,
        Defending,
        Dead
    }

    public EnemyType Type { get; set; }
    public AttackType Attack { get; set; }
    public State CurrentState { get; set; }

    public void Attack2()
    {
        if (CurrentState == State.Attacking)
        {
            Debug.Log("Attacking with " + Attack.ToString() + " attack!");
        }
    }

    public void Defend()
    {
        if (CurrentState == State.Defending)
        {
            Debug.Log("Defending against enemy " + Type.ToString() + "!");
        }
    }

    public void Die()
    {
        if (CurrentState != State.Dead)
        {
            CurrentState = State.Dead;
            Debug.Log("Creature has died.");
        }
    }
}

