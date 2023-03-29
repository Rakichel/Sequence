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
        public PlayerDirection Direction = PlayerDirection.Right;   // �÷��̾ �����ִ� ����
        public PlayerState State = PlayerState.Idle;                // �÷��̾��� ����
        public SpriteRenderer SpriteRender;                         // �ܻ�ȿ���� �ʿ��� ������ �ҷ��� ����
        public GhostTrail Ghost;                                    // �ܻ��� �ʿ� �� ���� Ű�� ���� ����

        private void Start()
        {
            Rigidbody = GetComponent<Rigidbody2D>();
            SpriteRender = GetComponent<SpriteRenderer>();
            Ghost = GetComponent<GhostTrail>();
        }

        /// <summary>
        /// �÷��̾ ������ �������� �Ǻ��ϱ� ���� �Լ��Դϴ�.
        /// </summary>
        /// <returns></returns>
        public bool PlayerFixedState()
        {
            // ���� ��, �뽬 ��, ���� ��, ���� �� ������ ���·� ����
            return (State != PlayerState.Attack && State != PlayerState.Dash && State != PlayerState.Hit && State != PlayerState.Die);
        }
    }
}
