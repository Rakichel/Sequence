using UnityEngine;
using UnityEngine.SceneManagement;

public class BossDie : IBossTodo
{
    private float timer = 0f;
    private bool isNext = false;
    public BossDie() { }
    public void Work()
    {
        timer += Time.unscaledDeltaTime;
        if (timer > 1f && !isNext)
        {
            // 씬로드 함수 호출
            SceneManager.LoadScene("ReaderBoard");
            isNext = true;
        }
    }
}