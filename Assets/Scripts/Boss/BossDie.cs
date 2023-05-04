using UnityEngine;

public class BossDie : IBossTodo
{
    private float timer = 0f;
    public BossDie() { }
    public void Work()
    {
        timer += Time.unscaledDeltaTime;
        if (timer > 1f)
        {
            // 씬로드 함수 호출
        }
    }
}