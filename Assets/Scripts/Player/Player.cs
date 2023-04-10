using Manager;
using UnityEngine;

namespace PlayerInfo
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(SpriteRenderer))]
    [RequireComponent(typeof(GhostTrail))]
    public class Player : MonoBehaviour
    {
        private float _hp = 10f;

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
        private float _skillGage = 100f;
        public float SkillGage
        {
            get
            {
                return _skillGage;
            }
        }
        private bool _isSkill = false;
        private float _delay = 0f;

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
            if (Input.GetKeyDown(KeyCode.F) && _isSkill == false)
            {
                _isSkill = true;
                CameraManager.Instance.Chronos();
            }
            else if ((Input.GetKeyDown(KeyCode.F) && _isSkill == true && _delay > 1f) || _skillGage == 0f)
            {
                _isSkill = false;
                CameraManager.Instance.ChronosBGA = 0f;
            }

            if (_isSkill == true)
            {
                _skillGage = Mathf.Clamp(_skillGage - Time.unscaledDeltaTime * 20f, 0f, 100f);
                _delay += Time.unscaledDeltaTime;
            }
            else if (_isSkill == false)
            {
                _skillGage = Mathf.Clamp(_skillGage + Time.unscaledDeltaTime * 10f, 0f, 100f);
                _delay = 0f;
            }
            Debug.Log(_skillGage);
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
                    State != PlayerState.Counter &&
                    State != PlayerState.Landing &&
                    State != PlayerState.Landed &&
                    State != PlayerState.Dash &&
                    State != PlayerState.Guard &&
                    State != PlayerState.Guarding &&
                    State != PlayerState.Hit &&
                    State != PlayerState.Die
                );
        }
    }
}
