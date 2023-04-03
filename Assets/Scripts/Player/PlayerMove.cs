using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerMove : MonoBehaviour
{
    public float Speed;

    private Vector2 _movement;
    private float _horizontal;
    private Player _player;

    void Start()
    {
        _player = GetComponent<Player>();
    }

    void Update()
    {
        _horizontal = Input.GetAxisRaw("Horizontal");
    }

    private void FixedUpdate()
    {
        if (!_player.IsDashing)
        {
            _movement = new Vector2(_horizontal, 0f);
            _movement.Normalize();
            _player.Rigidbody.velocity = new Vector2(_movement.x * Speed, _player.Rigidbody.velocity.y);
        }
    }
}
