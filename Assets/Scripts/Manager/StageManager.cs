using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


namespace Manager
{ 
    public class StageManager : Singleton<StageManager>
    {
        private void Update()
        {
            if(SceneManager.GetActiveScene().name == "Title")
            {
                if(Input.anyKey)
                {
                    SceneManager.LoadScene("Tutorial");
                }
            }

            if (SceneManager.GetActiveScene().name == "Tutorial")
            {
                SoundManager.Instance.PlayBGMSound();
            }

            if (Input.GetKey(KeyCode.Minus))
            {
                SceneManager.LoadScene(gameObject.scene.name);
            }

            if (Input.GetKeyDown(KeyCode.P))
            {
                GotoBoss();
            }
            if (Input.GetKeyDown(KeyCode.O))
            {
                GotoReader();
            }
            if(Input.GetKeyDown(KeyCode.I))
            {
                SceneManager.LoadScene("Japanese landscape");
            }
        }

        public void GotoTitle()
        {
            SceneManager.LoadScene("Title");
        }

        public void GotoBoss()
        {
            SceneManager.LoadScene("BossScene");
        }

        public void GotoReader()
        {
            SceneManager.LoadScene("ReaderBoard");
        }
    }
}
