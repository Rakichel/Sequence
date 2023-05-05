using Manager;
using UnityEngine;

namespace PlayerInfo
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(SpriteRenderer))]
    [RequireComponent(typeof(GhostTrail))]
    public class Player : MonoBehaviour
    {
        [SerializeField] private float _hp = 100f;

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

        public GameObject Shadow;
        public bool isShadow = false;
        public bool isDrain = false;
        public bool isDash = false;
        public Rigidbody2D Rigidbody;
        public PlayerDirection Direction = PlayerDirection.Right;   // �÷��̾ �����ִ� ����
        public PlayerState State = PlayerState.Idle;                // �÷��̾��� ����
        public SpriteRenderer SpriteRender;                         // �ܻ�ȿ���� �ʿ��� ������ �ҷ��� ����
        public GhostTrail Ghost;                                    // �ܻ��� �ʿ� �� ���� Ű�� ���� ����
        public PlayerDash Dash;                                     // �뽬 ����� ����ϴ� Ŭ����

        private void Start()
        {
            Rigidbody = GetComponent<Rigidbody2D>();
            SpriteRender = GetComponent<SpriteRenderer>();
            Ghost = GetComponent<GhostTrail>();
            Dash = GetComponent<PlayerDash>();
            if (Shadow == null)
            {
                Shadow = GameObject.Find("Shadow");
            }
        }
        private void Update()
        {
            if (isShadow && !Shadow.activeSelf)
                Shadow.SetActive(true);
            else if (!isShadow && Shadow.activeSelf)
                Shadow.SetActive(false);
            if (Input.GetKeyDown(KeyCode.F) && _isSkill == false && _skillGage == 100f && State != PlayerState.Die)
            {
                _isSkill = true;
                CameraManager.Instance.Chronos();
                EffectManager.Instance.Chronos();
                GameManager.Instance.TimeS = 0.2f;
            }
            else if (_skillGage == 0f)
            {
                _isSkill = false;
                CameraManager.Instance.ChronosBGA = 0f;
                GameManager.Instance.TimeS = 1f;
            }

            if (_isSkill == true)
            {
                _skillGage = Mathf.Clamp(_skillGage - Time.unscaledDeltaTime * 20f, 0f, 100f);
            }
            else if (_isSkill == false)
            {
                _skillGage = Mathf.Clamp(_skillGage + Time.unscaledDeltaTime * 50f, 0f, 100f);
            }
        }

        public void ShadowPartner()
        {
            isShadow = true;
        }

        public void DrainAble()
        {
            isDrain = true;
        }

        public void DashUp()
        {
            isDash = true;
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
