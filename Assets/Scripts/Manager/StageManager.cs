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
                    SceneManager.LoadScene("Japanese landscape");
                }
            }

            if (Input.GetKey(KeyCode.Minus))
            {
                SceneManager.LoadScene(gameObject.scene.name);
            }

            if (SceneManager.GetActiveScene().name == "Japanese landscape")
            {
                if (GameManager.Instance.KillCount == 100)
                {

                }
            }
        }
    }
}
