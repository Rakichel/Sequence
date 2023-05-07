using Manager;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    private string _fileName = "EnemyStatus";

    public Enemy _enemy;
    private int _moveSpeed = 3;
    [SerializeField] float distance = 0;
    public int MoveSpeed
    {
        get
        {
            return _moveSpeed;
        }
        set => _moveSpeed = value;
    }
    // Start is called before the first frame update
    void Start()
    {
        LoadData();
        _enemy = gameObject.GetComponent<Enemy>();

    }
    void Update()
    {
        if (_enemy._enemyState != EnemyState.Dead)
        {
            ChasePlayer();
        }
    }
    private void ChasePlayer()
    {
        if (_enemy._player != null)
        {
            distance = transform.position.x - _enemy._player.transform.position.x; // �� ������.x���� �÷��̾��������� ���� ������ ���� �������̸� ���
            Vector2 dis = new Vector2(distance, 0f);
            dis.Normalize();
            if (distance < 0)
            {
                _enemy.enemySprite.flipX = false;
            }
            else
            {
                _enemy.enemySprite.flipX = true;
            }
            if (Mathf.Abs(distance) <= 5f && Mathf.Abs(distance) >= 1f)
            {
                _enemy.rigid.velocity = new Vector2(-dis.x * MoveSpeed * Time.timeScale, _enemy.rigid.velocity.y);
                _enemy._enemyState = EnemyState.isRunning;
            }
            else
            {
                if (_enemy.EnemyFixedState())
                {
                    _enemy._enemyState = EnemyState.Idle;
                }
            }
        }
    }
    private void SaveData()
    {
        JsonManager<EnemyData>.Save(new EnemyData(), _fileName);
    }

    private void LoadData()
    {
        EnemyData data = JsonManager<EnemyData>.Load(_fileName);
        if (data != null)
        {
            MoveSpeed = data.Status[GameManager.Instance.EnemyLevel - 1].MoveSpeed;
        }
        else
        {
            SaveData();
            LoadData();
        }
    }
}
