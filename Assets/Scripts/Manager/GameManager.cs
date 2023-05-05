using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Manager
{
    
    public class GameManager : Singleton<GameManager>
    {
        public float TimeS = 1f;
        public int KillCount = 0;
        public int EnemyLevel = 1;
        public int LevelScale;
        public int KillCountTotal = 0;
        [SerializeField] private float dieTime = 0.1f;
        [SerializeField] private float _fade = 0.1f;
        private bool dieShader = true;

        private void FixedUpdate()
        {
            Physics2D.Simulate(0.02f);
        }
        private void Update()
        {
            Time.timeScale = TimeS;
            Time.fixedDeltaTime = 0.02f * Time.timeScale;
            

            if (Input.GetKeyDown(KeyCode.T))
            {
                Debug.Log(Time.timeScale);
            }

            if (KillCount == LevelScale + KillCountTotal)
            {
                EnemyLevel++;
                UiManager.Instance.LevelUpMenu(true);
                KillCountTotal += KillCount;
                KillCount = 0;
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
