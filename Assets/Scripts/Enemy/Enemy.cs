using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerInfo;

public class Enemy : MonoBehaviour
{
    private int _hp;
    private Player _player;
    public Animator enemyAnimator;


    public int GetDamage(int damage)
    {
        int actualDamage = Mathf.Clamp(damage, 0, 100);
        _hp -= actualDamage;

        if (_hp <= 0)
        {
            
            Die();
        }

        return actualDamage;
    }


    private void Die()
    {
        enemyAnimator.SetTrigger("Die");
        // 적을 파괴하는 코드 작성
        Destroy(gameObject);
    }
}
