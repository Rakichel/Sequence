using BossInfo;
using UnityEngine;
using UnityEngine.UI;

public class BossHpBar : MonoBehaviour
{
    public BossController Boss;
    private Image image;
    public float curHp;
    public float maxHp;
    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
        maxHp = Boss.Status.Hp;
        curHp = maxHp;
    }

    // Update is called once per frame
    void Update()
    {
        curHp = Boss.Status.Hp;
        image.fillAmount = curHp / maxHp;
    }
}
