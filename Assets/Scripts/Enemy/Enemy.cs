using Manager;
using PlayerInfo;
using System;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyUnit
{
    None = 0, Enemy, Comander
}

[Serializable]
public class EnemyStatus
{
    public int Hp;
    public int Damage;
    public int MoveSpeed;

    public EnemyStatus(int level)
    {
        switch (level)
        {
            case 1:
                Hp = 100;
                Damage = 1;
                MoveSpeed = 3;
                break;
            case 2:
                Hp = 110;
                Damage = 1;
                MoveSpeed = 3;
                break;
            case 3:
                Hp = 120;
                Damage = 1;
                MoveSpeed = 3;
                break;
            case 4:
                Hp = 130;
                Damage = 1;
                MoveSpeed = 3;
                break;
            case 5:
                Hp = 140;
                Damage = 1;
                MoveSpeed = 3;
                break;
            case 6:
                Hp = 150;
                Damage = 1;
                MoveSpeed = 3;
                break;
            case 7:
                Hp = 160;
                Damage = 1;
                MoveSpeed = 3;
                break;
            case 8:
                Hp = 170;
                Damage = 1;
                MoveSpeed = 3;
                break;
            case 9:
                Hp = 180;
                Damage = 1;
                MoveSpeed = 3;
                break;
            case 10:
                Hp = 190;
                Damage = 1;
                MoveSpeed = 3;
                break;
            default:
                break;
        }
    }
}

[Serializable]
public class EnemyData
{
    public List<EnemyStatus> Status = new List<EnemyStatus>();

    public EnemyData()
    {
        for (int i = 1; i <= 10; i++)
        {
            Status.Add(new EnemyStatus(i));
        }
    }
}

[Serializable]
public class ComanderStatus
{
    public int Hp;
    public int Damage;
    public int MoveSpeed;

    public ComanderStatus(int level)
    {
        switch (level)
        {
            case 1:
                Hp = 100;
                Damage = 1;
                MoveSpeed = 3;
                break;
            case 2:
                Hp = 110;
                Damage = 1;
                MoveSpeed = 3;
                break;
            case 3:
                Hp = 120;
                Damage = 1;
                MoveSpeed = 3;
                break;
            case 4:
                Hp = 130;
                Damage = 1;
                MoveSpeed = 3;
                break;
            case 5:
                Hp = 140;
                Damage = 1;
                MoveSpeed = 3;
                break;
            case 6:
                Hp = 150;
                Damage = 1;
                MoveSpeed = 3;
                break;
            case 7:
                Hp = 160;
                Damage = 1;
                MoveSpeed = 3;
                break;
            case 8:
                Hp = 170;
                Damage = 1;
                MoveSpeed = 3;
                break;
            case 9:
                Hp = 180;
                Damage = 1;
                MoveSpeed = 3;
                break;
            case 10:
                Hp = 190;
                Damage = 1;
                MoveSpeed = 3;
                break;
            default:
                break;
        }
    }
}

[Serializable]
public class ComanderData
{
    public List<EnemyStatus> Status = new List<EnemyStatus>();

    public ComanderData()
    {
        for (int i = 1; i <= 10; i++)
        {
            Status.Add(new EnemyStatus(i));
        }
    }
}

public class Enemy : MonoBehaviour
{
    private string _fileName = "EnemyStatus";
    [SerializeField] private int _hp;
    private float colorTimer = 1f;

    public bool dieCheck = false;
    public Player _player;
    public Animator enemyAnimator;
    public SpriteRenderer enemySprite;
    public Rigidbody2D rigid;
    public EnemyState _enemyState;
    public EnemyUnit unit;

    private void Awake()
    {
        LoadData();
        rigid = transform.GetComponent<Rigidbody2D>();
        enemyAnimator = gameObject.GetComponent<Animator>();
        enemySprite = gameObject.GetComponent<SpriteRenderer>();
    }

    public void GetDamage(int damage)
    {
        Hp -= damage;
        if (Hp > 0)
        {
            colorTimer = 0f;
        }
    }

    public int Hp
    {
        set
        {
            _hp = Mathf.Clamp(value, 0, 100);
            if (_hp <= 0)
            {
                Die();
            }
        }
        get
        {
            return _hp;
        }
    }

    public void Update()
    {
        enemySprite.color = Color.Lerp(Color.red, Color.white, colorTimer);
        if (colorTimer < 1f)
        {
            colorTimer += 2f * Time.deltaTime;
        }
        Collider2D Player = Physics2D.OverlapCircle(gameObject.transform.position, 6f, 1 << 7); // 
        if (Player != null)
        {
            _player = Player.GetComponent<Player>();
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            Hp -= 10;
        }

        if (gameObject.GetComponent<SpriteRenderer>().material.GetFloat("_Fade") <= 0)
        {
            Manager.GameManager.Instance.KillCount++;
            Destroy(gameObject);
        }
        if (dieCheck)
        {
            Manager.GameManager.Instance.EnemyDie(gameObject.GetComponent<SpriteRenderer>().material);
        }
    }
    private void Die()
    {
        _enemyState = EnemyState.Dead;
    }
    public void DieShader()
    {
        dieCheck = true;
    }

    public bool EnemyFixedState()
    {
        // 공격 시, 대쉬 시, 맞을 시, 죽을 시 경직된 상태로 판정
        return
            (
                _enemyState != EnemyState.isAttack &&
                _enemyState != EnemyState.isHurt &&
                _enemyState != EnemyState.isStiffen &&
                _enemyState != EnemyState.Protection &&
                _enemyState != EnemyState.Shot &&
                _enemyState != EnemyState.Dead
            );
    }
    private void SaveData()
    {
        if (unit == EnemyUnit.Enemy)
            JsonManager<EnemyData>.Save(new EnemyData(), _fileName);
        else if (unit == EnemyUnit.Comander)
            JsonManager<ComanderData>.Save(new ComanderData(), _fileName);
    }

    private void LoadData()
    {
        if (unit == EnemyUnit.Enemy)
        {
            EnemyData data = JsonManager<EnemyData>.Load(_fileName);
            if (data != null)
            {
                _hp = data.Status[GameManager.Instance.EnemyLevel - 1].Hp;
            }
            else
            {
                SaveData();
                LoadData();
            }
        }
        else if (unit == EnemyUnit.Comander)
        {
            ComanderData data = JsonManager<ComanderData>.Load(_fileName);
            if (data != null)
            {
                _hp = data.Status[GameManager.Instance.EnemyLevel - 1].Hp;
            }
            else
            {
                SaveData();
                LoadData();
            }
        }
    }
}
