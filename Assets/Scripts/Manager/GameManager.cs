using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Manager
{
    
    public class GameManager : Singleton<GameManager>
    {
        public float TimeS = 0.2f;
        // Start is called before the first frame update
        private void Start()
        {
            Time.timeScale = TimeS;
        }

        private void FixedUpdate()
        {
            Time.fixedDeltaTime = 0.02f * Time.timeScale;
            Physics2D.Simulate(0.02f);
        }
        // Update is called once per frame
        private void Update()
        {
            if(Input.GetKey(KeyCode.Minus))
            {
                SceneManager.LoadScene(gameObject.scene.name);
            }
        }
    }

}
