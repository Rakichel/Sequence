using UnityEngine;

public class EnemyMove : MonoBehaviour
{
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
        _enemy = gameObject.GetComponent<Enemy>();

    }
    void Update()
    {
        if(_enemy._enemyState != EnemyState.Dead)
        {
            ChasePlayer();
        }
    }
    private void ChasePlayer()
    {
        if (_enemy._player != null)
        {
            distance = transform.position.x - _enemy._player.transform.position.x; // 내 포지션.x에서 플레이어포지션을 빼면 음수면 왼쪽 오른쪽이면 양수
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
    // Update is called once per frame

}
