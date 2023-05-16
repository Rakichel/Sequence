using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Manager
{

    public class GameManager : Singleton<GameManager>
    {
        public static float GameTimer;
        public float TimeS = 1f;
        public int KillCount = 0;
        public int EnemyLevel = 1;
        public int LevelScale;
        public int KillCountTotal = 0;
        [SerializeField] private float dieTime = 0.1f;
        [SerializeField] private float _fade = 0.1f;
        private bool dieShader = true;

        private void Awake()
        {
            if (SceneManager.GetActiveScene().name == "Japanese landscape")
            {
                GameTimer = 0f;
            }
        }
        private void FixedUpdate()
        {
            if (Time.timeScale != 0.1f)
                Physics2D.Simulate(0.02f);
            if (SceneManager.GetActiveScene().name == "Japanese landscape" || SceneManager.GetActiveScene().name == "BossScene")
                GameTimer += Time.unscaledDeltaTime;
        }
        private void Update()
        {
            Time.timeScale = TimeS;
            Time.fixedDeltaTime = 0.02f * Time.timeScale;

            if (Input.GetKeyDown(KeyCode.T))
            {
                Debug.Log(Time.timeScale);
            }

            if (SceneManager.GetActiveScene().name == "Japanese landscape")
            {
                if (KillCount == LevelScale + KillCountTotal)
                {
                    EnemyLevel++;
                    UiManager.Instance.LevelUpMenu(true);
                    KillCountTotal += 4;
                    KillCount = 0;
                }
            }

            if (EnemyLevel == 5)
            {
                Manager.StageManager.Instance.GotoBoss();
            }
        }

        public void EnemyDie(Material EnemyM)
        {
            if (dieShader && EnemyM.GetFloat("_Fade") > 0)
            {
                dieShader = false;
                float fade = EnemyM.GetFloat("_Fade");
                EnemyM.SetFloat("_Fade", (fade - _fade));
                StartCoroutine(IEnemyDie());
            }
        }
        private IEnumerator IEnemyDie()
        {
            yield return new WaitForSeconds(dieTime);
            dieShader = true;
        }
    }

}
