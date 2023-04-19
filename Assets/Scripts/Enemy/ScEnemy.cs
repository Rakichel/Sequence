using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerInfo;

public class ScEnemy : Enemy
{
    [SerializeField] private int _hp1;
    private void Awake()
    {
        enemyAnimator = transform.GetComponent<Animator>();
        Hp = _hp1;
    }
}
