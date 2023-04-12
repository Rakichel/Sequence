using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Manager
{
    
    public class GameManager : Singleton<GameManager>
    {
        public float TimeS = 1f;
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
            if (Input.GetKeyDown(KeyCode.F))
            {
                if (TimeS == 0.2f)
                    TimeS = 1;
                else
                {
                    TimeS = 0.2f;
                }
            }
        }
    }

}
