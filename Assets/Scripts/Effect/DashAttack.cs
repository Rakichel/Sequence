using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BossInfo;

public class DashAttack : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Enemy>() != null)
        {
            collision.GetComponent<Enemy>().GetDamage(10);
        }
        if (collision.GetComponent<ArcherController>() != null)
        {
            collision.GetComponent<ArcherController>().GetDamage(10);
        }
        else if (collision.GetComponent<BossController>() != null)
        {
            collision.GetComponent<BossController>().GetDamage(10);
        }
    }
}
