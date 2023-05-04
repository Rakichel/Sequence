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
        [SerializeField] private float dieTime = 0.1f;
        [SerializeField] private float _fade = 0.1f;
        private bool dieShader = true;
        // Start is called before the first frame update
        private void Start()
        {
            
        }

        private void FixedUpdate()
        {
            Time.timeScale = TimeS;
            Time.fixedDeltaTime = 0.02f * Time.timeScale;
            Physics2D.Simulate(0.02f);
        }
        // Update is called once per frame
        private void Update()
        {
            //EnemyDie(null);
            if(Input.GetKeyDown(KeyCode.T))
            {
                Debug.Log(Time.timeScale);
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
