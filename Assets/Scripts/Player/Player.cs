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
        public PlayerDirection Direction = PlayerDirection.Right;   // 플레이어가 보고있는 방향
        public PlayerState State = PlayerState.Idle;                // 플레이어의 상태
        public SpriteRenderer SpriteRender;                         // 잔상효과에 필요한 정보를 불러올 변수
        public GhostTrail Ghost;                                    // 잔상을 필요 시 끄고 키기 위한 변수
        public PlayerDash Dash;                                     // 대쉬 기능을 담당하는 클래스

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
        /// 플레이어가 경직된 상태인지 판별하기 위한 함수입니다.
        /// </summary>
        /// <returns></returns>
        public bool PlayerFixedState()
        {
            // 공격 시, 대쉬 시, 맞을 시, 죽을 시 경직된 상태로 판정
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
