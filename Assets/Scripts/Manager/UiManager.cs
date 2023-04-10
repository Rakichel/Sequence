using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PlayerInfo;

public class UiManager : Singleton<UiManager>
{
    Player _player;
    float hp;
    public Image HpBar;
    // Start is called before the first frame update
    private void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        HpBar.fillAmount = _player.Hp / 100f;
    }
}
