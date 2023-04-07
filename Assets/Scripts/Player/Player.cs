using UnityEngine;

namespace PlayerInfo
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(SpriteRenderer))]
    [RequireComponent(typeof(GhostTrail))]
    public class Player : MonoBehaviour
    {
        private float _hp;

        public float Hp
        {
            set
            {
                _hp = Mathf.Clamp(value, 0f, 100f);
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
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.K))
            {
                if (Time.timeScale >= 1f)
                {
                    Time.timeScale = 0.2f;
                }
                else
                {
                    Time.timeScale = 1f;
                }
                Time.fixedDeltaTime = 0.02f * Time.timeScale;
            }
        }

        /// <summary>
        /// �÷��̾ ������ �������� �Ǻ��ϱ� ���� �Լ��Դϴ�.
        /// </summary>
        /// <returns></returns>
        public bool PlayerFixedState()
        {
            // ���� ��, �뽬 ��, ���� ��, ���� �� ������ ���·� ����
            return
                (
                    State != PlayerState.Attack &&
                    State != PlayerState.Combo &&
                    State != PlayerState.Landing &&
                    State != PlayerState.Dash &&
                    State != PlayerState.Guard &&
                    State != PlayerState.Hit &&
                    State != PlayerState.Die
                );
        }
    }
}
