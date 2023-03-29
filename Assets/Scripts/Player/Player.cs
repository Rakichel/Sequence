using System.Collections;
using UnityEngine;

namespace PlayerInfo
{
    public enum PlayerDirection { Right, Left }
    public enum PlayerState { Idle = 0, Move, Jump, Dash, Attack, Hit, Die }
    public class Player : MonoBehaviour
    {
        private int _hp;
        public int Hp 
        { 
            set
            {
                _hp = Mathf.Clamp(value, 0, 100);
            }
            get 
            { 
                return _hp; 
            } 
        }
        public Rigidbody2D Rigidbody;
        public PlayerDirection direction = PlayerDirection.Right;   // 플레이어가 보고있는 방향
        public PlayerState state = PlayerState.Idle;                // 플레이어의 상태

        private void Start()
        {
            if (Rigidbody == null)
                Rigidbody = GetComponent<Rigidbody2D>();
        }

        public bool PlayerFixedState()
        {
            return (state != PlayerState.Attack && state != PlayerState.Dash && state != PlayerState.Hit && state != PlayerState.Die);
        }
    }
}
