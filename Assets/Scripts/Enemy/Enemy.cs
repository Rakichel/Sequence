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
        rigid.velocity = Vector2.zero; // ������ ����
        enemyAnimator.SetBool("IsWalking", false); // �ȱ� �ִϸ��̼� ����
        enemyAnimator.SetBool("isAttack", false); // ���� �ִϸ��̼� ����
        // ���� �ı��ϴ� �ڵ� �ۼ�
        Destroy(gameObject, 2f);
    }
}
