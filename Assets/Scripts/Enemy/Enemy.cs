using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerInfo;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int _hp;
    private Player _player;
    public Animator enemyAnimator;
    bool d = false;
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

    public void Update()
    {
        if(d)
            Manager.GameManager.Instance.EnemyDie(gameObject.GetComponent<SpriteRenderer>().material);
        if (gameObject.GetComponent<SpriteRenderer>().material.GetFloat("_Fade") <= 0)
        {
            Destroy(gameObject);
        }
    }
    private void Die()
    {
        gameObject.GetComponent<EnemyAttack>().enabled = false;
        d = true;
        //enemyAnimator.SetBool("Die",true);
        // 적을 파괴하는 코드 작성
        //Destroy(gameObject, 2f);
    }
}
