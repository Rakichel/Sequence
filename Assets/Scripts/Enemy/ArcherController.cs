using Manager;
using PlayerInfo;
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ArcherStatus
{
    public int Hp;
    public int Damage;

    public ArcherStatus(int level)
    {
        switch (level)
        {
            case 1:
                Hp = 100;
                Damage = 1;
                break;
            case 2:
                Hp = 110;
                Damage = 1;
                break;
            case 3:
                Hp = 120;
                Damage = 1;
                break;
            case 4:
                Hp = 130;
                Damage = 1;
                break;
            case 5:
                Hp = 140;
                Damage = 1;
                break;
            case 6:
                Hp = 150;
                Damage = 1;
                break;
            case 7:
                Hp = 160;
                Damage = 1;
                break;
            case 8:
                Hp = 170;
                Damage = 1;
                break;
            case 9:
                Hp = 180;
                Damage = 1;
                break;
            case 10:
                Hp = 190;
                Damage = 1;
                break;
            default:
                break;
        }
    }
}

[Serializable]
public class ArcherData
{
    public List<ArcherStatus> Status = new List<ArcherStatus>();

    public ArcherData()
    {
        for (int i = 1; i <= 10; i++)
        {
            Status.Add(new ArcherStatus(i));
        }
    }
}
public class ArcherController : MonoBehaviour
{
    private string _fileName = "EnemyStatus";

    private Animator Animator;
    private Player target;
    private SpriteRenderer archerSprite;
    private float colorTimer = 1f;

    public int hp;
    public int Damage;
    public GameObject Arrow;
    public bool dieShader = false;

    // Start is called before the first frame update
    void Start()
    {
        Animator = GetComponent<Animator>();
        target = GameObject.FindWithTag("Player").GetComponent<Player>();
        archerSprite = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (dieShader)
        {
            GameManager.Instance.EnemyDie(gameObject.GetComponent<SpriteRenderer>().material);
        }
        if (gameObject.GetComponent<SpriteRenderer>().material.GetFloat("_Fade") <= 0)
        {
            GameManager.Instance.KillCount++;
            Destroy(gameObject);
        }
        archerSprite.color = Color.Lerp(Color.red, Color.white, colorTimer);
        if (colorTimer < 1f)
        {
            colorTimer += 2f * Time.deltaTime;
        }
        if (hp <= 0)
        {
            Animator.SetBool("Shot", false);
            Animator.SetBool("Dead", true);
            return;
        }
        LookDir();
        Collider2D col = Physics2D.OverlapCircle(transform.position, 15f, 1 << 7);
        if (col != null)
        {
            Animator.SetBool("Shot", true);
        }

    }
    public void GetDamage(int damage)
    {
        hp -= damage;
        if (hp > 0)
        {
            colorTimer = 0f;
        }
    }
    public void StopShot()
    {
        Collider2D col = Physics2D.OverlapCircle(transform.position, 15f, 1 << 7);
        if (col == null)
        {
            Animator.SetBool("Shot", false);
        }
    }
    public void CreateArrow()
    {
        Quaternion q = Quaternion.AngleAxis(GetAngle(transform.position, target.transform.position + new Vector3(0f, 0.5f)), Vector3.forward);

        Arrow g = Instantiate(Arrow, transform.position, q).GetComponent<Arrow>();
        g.Damage = Damage;
    }
    public void LookDir()
    {
        if (transform.position.x > target.transform.position.x)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
        else
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }
    }
    private float GetAngle(Vector2 start, Vector2 end)
    {
        Vector2 v2 = end - start;
        return Mathf.Atan2(v2.y, v2.x) * Mathf.Rad2Deg;
    }
    public void Die()
    {
        dieShader = true;
    }

    private void SaveData()
    {
        JsonManager<ArcherData>.Save(new ArcherData(), _fileName);
    }

    private void LoadData()
    {
        ArcherData data = JsonManager<ArcherData>.Load(_fileName);
        if (data != null)
        {
            hp = data.Status[GameManager.Instance.EnemyLevel - 1].Hp;
            Damage = data.Status[GameManager.Instance.EnemyLevel - 1].Damage;
        }
        else
        {
            SaveData();
            LoadData();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, 15f);
    }
}
