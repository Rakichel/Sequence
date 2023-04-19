using BossInfo;
using Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerInfo;
using UnityEngine.UIElements;

public class BossController : MonoBehaviour
{
    private float _gravity = 5f;
    private float _gravityAccel = -9.8f;
    private Rigidbody2D _rigid;
    private Player _player;

    [SerializeField] private BossState _bossState;
    private BossState _currentState;
    private IBossTodo _todo;
    // Start is called before the first frame update
    void Start()
    {
        _rigid = GetComponent<Rigidbody2D>();
        _todo = null;
        _bossState = BossState.Idle;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(_player);
        Gravity();
        _todo?.Work();

        if(_player == null)
            Chase();

        if (Input.GetKeyDown(KeyCode.Q))
            _bossState = BossState.Move;

        if (_bossState == _currentState)
            return;

        _currentState = _bossState;
        switch (_bossState)
        {
            case BossState.Idle:
                _todo = new BossIdle();
                break;
            case BossState.Move:
                _todo = new BossMovement(_rigid);
                break;
            default:
                break;
        }
    }

    private void Gravity()
    {
        _rigid.velocity = new Vector2(_rigid.velocity.x, _rigid.velocity.y + Time.deltaTime * _gravity * _gravityAccel * Time.timeScale);
    }
    private void Chase()
    {
        Collider2D col = Physics2D.OverlapCircle(_transform.position, 5f);
        _player = col?.GetComponent<Player>();
    }
}
