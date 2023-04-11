using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PlayerInfo;

public class UiManager : Singleton<UiManager>
{
    Player _player;
    [SerializeField]
    private Image HpBar;
    [SerializeField]
    private Image SkillBar;
    // Start is called before the first frame update
    private void Awake()
    {
        try
        {
            _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
            HpBar = GameObject.Find("HpBar").GetComponent<Image>();
            SkillBar = GameObject.Find("SkillBar").GetComponent<Image>();
        }
        catch
        {
            Debug.Log("�� ���� �Ҵ���� ���� ��ü�� �ֽ��ϴ�.");
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(HpBar != null && SkillBar != null)
        {
            HpBar.fillAmount = _player.Hp / 100f;
            SkillBar.fillAmount = _player.SkillGage / 100f;
        }
    }
}
