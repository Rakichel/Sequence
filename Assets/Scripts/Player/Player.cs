using System.Collections;
using UnityEngine;

namespace PlayerInfo
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(SpriteRenderer))]
    [RequireComponent(typeof(GhostTrail))]
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
        public PlayerDirection Direction = PlayerDirection.Right;   // 플레이어가 보고있는 방향
        public PlayerState State = PlayerState.Idle;                // 플레이어의 상태
        public SpriteRenderer SpriteRender;                         // 잔상효과에 필요한 정보를 불러올 변수
        public GhostTrail Ghost;                                    // 잔상을 필요 시 끄고 키기 위한 변수

        private void Start()
        {
            Rigidbody = GetComponent<Rigidbody2D>();
            SpriteRender = GetComponent<SpriteRenderer>();
            Ghost = GetComponent<GhostTrail>();
        }

        /// <summary>
        /// 플레이어가 경직된 상태인지 판별하기 위한 함수입니다.
        /// </summary>
        /// <returns></returns>
        public bool PlayerFixedState()
        {
            // 공격 시, 대쉬 시, 맞을 시, 죽을 시 경직된 상태로 판정
            return (State != PlayerState.Attack && State != PlayerState.Dash && State != PlayerState.Hit && State != PlayerState.Die);
        }
    }
}
