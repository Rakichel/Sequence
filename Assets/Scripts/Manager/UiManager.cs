using PlayerInfo;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Manager
{
    public class UiManager : Singleton<UiManager>
    {
        Player _player;
        [SerializeField] private Image HpBar;
        [SerializeField] private Image SkillBar;
        [SerializeField] private GameObject PauseFade;
        [SerializeField] private GameObject PauseMenu;
        [SerializeField] private GameObject SettingMenu;
        [SerializeField] private GameObject ProgressBar;
        [SerializeField] private Text EnemyLevel;
        [SerializeField] private Text Timer;
        [SerializeField] private GameObject LevelUp;
        [SerializeField] private Slider BGM;
        [SerializeField] private Slider SFX;
        [SerializeField] private Image SkillCheck;
        int iTime;
        public float saveScale;
        // Start is called before the first frame update
        private void Awake()
        {
            try
            {
                if (SceneManager.GetActiveScene().name == "Japanese landscape")
                {
                    _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
                    HpBar = GameObject.Find("HpBar").GetComponent<Image>();
                    SkillBar = GameObject.Find("SkillBar").GetComponent<Image>();
                    PauseFade = GameObject.Find("PauseFade");
                    PauseMenu = GameObject.Find("PauseMenu");
                    SettingMenu = GameObject.Find("SettingMenu");
                    ProgressBar = GameObject.Find("ProgressBar");
                    EnemyLevel = GameObject.Find("EnemyLevel").GetComponent<Text>();
                    LevelUp = GameObject.Find("LevelUpMenu");
                    Timer = GameObject.Find("Timer").GetComponent<Text>();
                    BGM = GameObject.Find("BgmSlider").GetComponent<Slider>();
                    SFX = GameObject.Find("SfxSlider").GetComponent<Slider>();
                    BGM.value = SoundManager.VolumeBGM;
                    SFX.value = SoundManager.VolumeSFX;
                    GameStart();
                }
                else if (SceneManager.GetActiveScene().name == "BossScene")
                {
                    _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
                    HpBar = GameObject.Find("HpBar").GetComponent<Image>();
                    SkillBar = GameObject.Find("SkillBar").GetComponent<Image>();
                    PauseFade = GameObject.Find("PauseFade");
                    PauseMenu = GameObject.Find("PauseMenu");
                    SettingMenu = GameObject.Find("SettingMenu");
                    Timer = GameObject.Find("Timer").GetComponent<Text>();
                    BGM = GameObject.Find("BgmSlider").GetComponent<Slider>();
                    SFX = GameObject.Find("SfxSlider").GetComponent<Slider>();
                    BGM.value = SoundManager.VolumeBGM;
                    SFX.value = SoundManager.VolumeSFX;
                    GameStart();
                }
                else
                {
                    _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
                    HpBar = GameObject.Find("HpBar").GetComponent<Image>();
                    SkillBar = GameObject.Find("SkillBar").GetComponent<Image>();
                    PauseFade = GameObject.Find("PauseFade");
                    PauseMenu = GameObject.Find("PauseMenu");
                    SettingMenu = GameObject.Find("SettingMenu");
                    BGM = GameObject.Find("BgmSlider").GetComponent<Slider>();
                    SFX = GameObject.Find("SfxSlider").GetComponent<Slider>();
                    BGM.value = SoundManager.VolumeBGM;
                    SFX.value = SoundManager.VolumeSFX;
                    GameStart();
                }
            }
            catch
            {
                Debug.Log("씬 내에 할당되지 않은 객체가 있습니다.");
                GameStart();
            }
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            if (HpBar != null && SkillBar != null)
            {
                HpBar.fillAmount = _player.Hp / 100f;
                SkillBar.fillAmount = _player.SkillGage / 100f;
            }
            SoundManager.VolumeBGM = BGM.value;
            SoundManager.VolumeSFX = SFX.value;
        }

        private void Update()
        {
            if (SceneManager.GetActiveScene().name == "Japanese landscape")
            {
                EnemyLevelUp();
                Progress();
                TimerUpdate();
            }
            else if (SceneManager.GetActiveScene().name == "BossScene")
            {
                TimerUpdate();
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (!PauseMenu.activeSelf)
                {
                    Pause(true);
                }
                else
                {
                    Pause(false);
                }
            }
        }

        public void TimerUpdate()
        {
            iTime = Mathf.FloorToInt(GameManager.GameTimer);
            Timer.text = "TIME : " + iTime;
        }
        public void EnemyLevelUp()
        {
            EnemyLevel.text = "Enemy Level : " + GameManager.Instance.EnemyLevel;
        }
        public void Progress()
        {
            string strFirst = null;
            string strSecond = null;
            string strThird = null;
            ProgressBar.GetComponent<Image>().fillAmount = (float)GameManager.Instance.KillCount / (GameManager.Instance.LevelScale + GameManager.Instance.KillCountTotal);
            if (ProgressBar.GetComponent<Image>().fillAmount >= 0.44)
            {
                strFirst = string.Format("<color=black>{0}</color>", GameManager.Instance.KillCount);
            }
            else
            {
                strFirst = string.Format("<color=white>{0}</color>", GameManager.Instance.KillCount);
            }

            if (ProgressBar.GetComponent<Image>().fillAmount >= 0.49)
            {
                strSecond = string.Format("<color=black> / </color>");
            }
            else
            {
                strSecond = string.Format("<color=white> / </color>");
            }

            if (ProgressBar.GetComponent<Image>().fillAmount >= 0.55)
            {
                strThird = string.Format("<color=black>{0}</color>", GameManager.Instance.LevelScale + GameManager.Instance.KillCountTotal);
            }
            else
            {
                strThird = string.Format("<color=white>{0}</color>", GameManager.Instance.LevelScale + GameManager.Instance.KillCountTotal);
            }
            ProgressBar.GetComponentInChildren<Text>().text = strFirst + strSecond + strThird;
        }
        public void GameStart()
        {
            PauseFade.SetActive(false);
            PauseMenu.SetActive(false);
            SettingMenu.SetActive(false);
            if (SceneManager.GetActiveScene().name == "Japanese landscape")
            {
                LevelUp.SetActive(false);
            }
        }
        public void Pause(bool c)
        {
            PauseFade.SetActive(c);
            PauseMenu.SetActive(c);
            SettingMenu.SetActive(false);
        }

        public void Setting(bool c)
        {
            SettingMenu.SetActive(c);
        }

        public void MainMenu()
        {
            StageManager.Instance.GotoTitle();
        }

        public void LevelUpMenu(bool c)
        {
            if (Manager.GameManager.Instance.EnemyLevel <= 4)
            {
                LevelUp.SetActive(c);
                saveScale = GameManager.Instance.TimeS;
                GameManager.Instance.TimeS = 0.1f;
            }
        }
        public void LevelUpMenuClose()
        {
            if (Manager.GameManager.Instance.EnemyLevel <= 4)
            {
                LevelUp.SetActive(false);
                GameManager.Instance.TimeS = saveScale;
            }
        }

        public void SkillInfo(int a)
        {
            if (LevelUp.transform.GetChild(a).GetChild(0).gameObject.activeSelf)
            {
                LevelUp.transform.GetChild(a).GetChild(0).gameObject.SetActive(false);
            }
            else
            {
                LevelUp.transform.GetChild(a).GetChild(0).gameObject.SetActive(true);
            }
        }


        public void SkillUp(string str)
        {
            switch (str)
            {
                case "Shadow":
                    _player.ShadowPartner();
                    LevelUpMenuClose();
                    LevelUp.transform.GetChild(2).GetChild(1).gameObject.SetActive(false);
                    LevelUp.transform.GetChild(2).GetChild(2).gameObject.SetActive(true);
                    break;
                case "Drain":
                    _player.DrainAble();
                    LevelUpMenuClose();

                    LevelUp.transform.GetChild(1).GetChild(1).gameObject.SetActive(false);
                    LevelUp.transform.GetChild(1).GetChild(2).gameObject.SetActive(true);
                    break;
                case "Dash":
                    _player.DashUp();
                    LevelUpMenuClose();

                    LevelUp.transform.GetChild(0).GetChild(1).gameObject.SetActive(false);
                    LevelUp.transform.GetChild(0).GetChild(2).gameObject.SetActive(true);
                    break;
            }
        }
    }
}

