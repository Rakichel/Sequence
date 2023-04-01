using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerInfo;
public class GhostTrail : MonoBehaviour
{
    private Player _player;
    private SpriteRenderer _spriteRenderer;     // 새로 생성한 ghost의 SpriteRenderer를 수정하기 위한 변수
    private float _timer = 0;                   // 주기적으로 생성하기 위한 타이머

    public GameObject GhostPrefab;          // 생성될 Prefab
    public float Delay = 1.0f;              // 생성하는데 걸리는 시간
    public float DestroyTime = 0.1f;        // 생성된 오브젝트가 삭제되는 시간
    public Color color;                     // 잔상 색 지정

    private void Start()
    {
        _player = GetComponent<Player>();
    }

    private void Update()
    {
        if(_timer > 0) 
        {
            _timer -= Time.deltaTime; 
        }
        else 
        {
            _timer = Delay; 
            CreateGhost(); 
        }
    }

    /// <summary>
    /// 잔상을 생성해주는 함수입니다.
    /// </summary>
    private void CreateGhost()
    {
        // 생성, 크기조절, 삭제 타이머
        GameObject ghostObj = Instantiate(GhostPrefab, transform.position, transform.rotation);
        ghostObj.transform.localScale = _player.transform.localScale;
        Destroy(ghostObj, DestroyTime);

        // 생성된 오브젝트의 스프라이트 수정
        _spriteRenderer = ghostObj.GetComponent<SpriteRenderer>();
        _spriteRenderer.sprite = _player.SpriteRender.sprite;
        _spriteRenderer.color = color;
    }
}