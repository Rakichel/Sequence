using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PlayerInfo;

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
        [SerializeField] private Slider BGM;
        [SerializeField] private Slider SFX;
        // Start is called before the first frame update
        private void Awake()
        {
            try
            {
                _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
                HpBar = GameObject.Find("HpBar").GetComponent<Image>();
                SkillBar = GameObject.Find("SkillBar").GetComponent<Image>();
                PauseFade = GameObject.Find("PauseFade");
                PauseMenu = GameObject.Find("PauseMenu");
                SettingMenu = GameObject.Find("SettingMenu");
                BGM = GameObject.Find("BgmSlider").GetComponent<Slider>();
                SFX = GameObject.Find("SfxSlider").GetComponent<Slider>();
                GameStart();
            }
            catch
            {
                Debug.Log("씬 내에 할당되지 않은 객체가 있습니다.");
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
            SoundManager.Instance.VolumeBGM = BGM.value;
            SoundManager.Instance.VolumeSFX = SFX.value;
            
        }

        private void Update()
        {
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

        public void GameStart()
        {
            PauseFade.SetActive(false);
            PauseMenu.SetActive(false);
            SettingMenu.SetActive(false);
        }
        public void Pause(bool c)
        {
            if(c)
            {
                GameManager.Instance.TimeS = 0f;
                Time.timeScale = 0f;
            }
            else
            {
                GameManager.Instance.TimeS = 1f;
                Time.timeScale = 1f;
            }
            PauseFade.SetActive(c);
            PauseMenu.SetActive(c);
        }

        public void Setting(bool c)
        {
            SettingMenu.SetActive(c);
        }

        public void MainMenu()
        {
            StageManager.Instance.GotoTitle();
        }
    }
}

