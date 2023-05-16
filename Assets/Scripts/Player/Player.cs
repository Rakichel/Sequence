using Manager;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PlayerInfo
{
    [Serializable]
    public class PlayerStatus
    {
        public float Speed;                 // 이동속도
        public float DashSpeed;             // 대쉬 속도
        public float DashDuration;          // 대쉬 지속 시간
        public float DashCooldown;          // 대쉬 쿨타임
        public float JumpForce;             // 점프력
        public float Drain;                  // 피흡
        public int Power;                   // 공격력
        public PlayerStatus()
        {
            Speed = 8f;
            DashSpeed = 35f;
            DashDuration = 0.2f;
            DashCooldown = 0.31f;
            JumpForce = 20f;
            Drain = 0.1f;
            Power = 10;
        }
    }

    [Serializable]
    public class PlayerData
    {
        public float Hp;
        public bool isShadow;
        public bool isDrain;
        public bool isDash;

        public PlayerData(float hp, bool isShadow, bool isDrain, bool isDash)
        {
            Hp = hp;
            this.isShadow = isShadow;
            this.isDrain = isDrain;
            this.isDash = isDash;
        }
    }

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
        private float _skillGage = 100f;
        public float SkillGage
        {
            get
            {
                return _skillGage;
            }
        }
        private bool _isSkill = false;
        private string _fileName = "PlayerSave";

        public GameObject Shadow;
        public bool isShadow;
        public bool isDrain;
        public bool isDash;
        public Rigidbody2D Rigidbody;
        public PlayerDirection Direction = PlayerDirection.Right;   // 플레이어가 보고있는 방향
        public PlayerState State = PlayerState.Idle;                // 플레이어의 상태
        public SpriteRenderer SpriteRender;                         // 잔상효과에 필요한 정보를 불러올 변수
        public GhostTrail Ghost;                                    // 잔상을 필요 시 끄고 키기 위한 변수
        public PlayerDash Dash;                                     // 대쉬 기능을 담당하는 클래스

        private void Awake()
        {
            if (SceneManager.GetActiveScene().name == "Tutorial")
                DeleteData();
        }
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
            LoadData();
        }
        private void Update()
        {
            if (SceneManager.GetActiveScene().name == "Tutorial")
                Hp = 100;

            if (isShadow && !Shadow.activeSelf)
                Shadow.SetActive(true);
            else if (!isShadow && Shadow.activeSelf)
                Shadow.SetActive(false);
            if (Time.timeScale > 0.1f)
            {
                if (Input.GetKeyDown(KeyCode.F) && _isSkill == false && _skillGage == 100f && State != PlayerState.Die)
                {
                    _isSkill = true;
                    CameraManager.Instance.Chronos();
                    EffectManager.Instance.Chronos();
                    SoundManager.Instance.PlaySFXSound("ChronosSound", 0.5f);
                    GameManager.Instance.TimeS = 0.2f;
                }
                else if (_skillGage == 0f)
                {
                    _isSkill = false;
                    CameraManager.Instance.ChronosBGA = 0f;
                    GameManager.Instance.TimeS = 1f;
                }
            }

            if (_isSkill == true && Time.timeScale != 0.1f)
            {
                _skillGage = Mathf.Clamp(_skillGage - Time.unscaledDeltaTime * 20f, 0f, 100f);
            }
            else if (_isSkill == false && Time.timeScale != 0.1f)
            {
                _skillGage = Mathf.Clamp(_skillGage + Time.unscaledDeltaTime * 50f, 0f, 100f);
            }

            SaveData();
        }

        private void SaveData()
        {
            JsonManager<PlayerData>.Save(new PlayerData(_hp, isShadow, isDrain, isDash), _fileName);
        }

        private void LoadData()
        {
            PlayerData data = JsonManager<PlayerData>.Load(_fileName);
            if (data != null)
            {
                _hp = data.Hp;
                isShadow = data.isShadow;
                isDrain = data.isDrain;
                isDash = data.isDash;
            }
            else
            {
                _hp = 100f;
                isShadow = false;
                isDrain = false;
                isDash = false;
                SaveData();
            }
        }

        private void DeleteData()
        {
            JsonManager<PlayerData>.Delete(_fileName);
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
