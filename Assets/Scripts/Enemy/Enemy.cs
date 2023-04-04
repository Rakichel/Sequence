using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int _hp;
    private Player _player;


    public int GetDamage(int damage)
    {
        int actualDamage = Mathf.Clamp((int)(damage), 0, 100);
        _hp -= actualDamage;


        if (_hp <= 0)
        {
            Die();
        }

        return actualDamage;
    }

    private void Die()
    {
        // 적을 파괴하는 코드 작성
        Destroy(gameObject);
    }
}
