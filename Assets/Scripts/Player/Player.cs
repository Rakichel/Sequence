using Manager;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PlayerInfo
{
    [Serializable]
    public class PlayerStatus
    {
        public float Speed;                 // �̵��ӵ�
        public float DashSpeed;             // �뽬 �ӵ�
        public float DashDuration;          // �뽬 ���� �ð�
        public float DashCooldown;          // �뽬 ��Ÿ��
        public float JumpForce;             // ������
        public float Drain;                  // ����
        public int Power;                   // ���ݷ�
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
        public PlayerDirection Direction = PlayerDirection.Right;   // �÷��̾ �����ִ� ����
        public PlayerState State = PlayerState.Idle;                // �÷��̾��� ����
        public SpriteRenderer SpriteRender;                         // �ܻ�ȿ���� �ʿ��� ������ �ҷ��� ����
        public GhostTrail Ghost;                                    // �ܻ��� �ʿ� �� ���� Ű�� ���� ����
        public PlayerDash Dash;                                     // �뽬 ����� ����ϴ� Ŭ����

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
