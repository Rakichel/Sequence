using PlayerInfo;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private int _hp = 10;
    private Rigidbody2D rigid;
    private Player _player;


    public Animator enemyAnimator;

    private void Awake()
    {
        rigid = transform.GetComponent<Rigidbody2D>();
    }

    public int GetDamage(int damage = 10)
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
        enemyAnimator.SetBool("Die", true);
        rigid.velocity = Vector2.zero; // 움직임 중지
        enemyAnimator.SetBool("IsWalking", false); // 걷기 애니메이션 중지
        enemyAnimator.SetBool("isAttack", false); // 공격 애니메이션 중지
        // 적을 파괴하는 코드 작성
        Destroy(gameObject, 2f);
    }
}
