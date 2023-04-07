using PlayerInfo;
using System.Collections.Generic;
using UnityEngine;

public class GhostTrail : MonoBehaviour
{
    private Player _player;
    private SpriteRenderer _spriteRenderer;     // 새로 생성한 ghost의 SpriteRenderer를 수정하기 위한 변수
    private float _timer = 0;                   // 주기적으로 생성하기 위한 타이머
    private Stack<GameObject> _ghostPool = new Stack<GameObject>();
    private bool isOne = false;

    public GameObject GhostGroup;
    public GameObject GhostPrefab;          // 생성될 Prefab
    public float Delay = 1.0f;              // 생성하는데 걸리는 시간
    public float DestroyTime = 0.1f;        // 생성된 오브젝트가 삭제되는 시간
    public Color color;                     // 잔상 색 지정
    public Color StartColor;
    public Color EndColor;
    public float Timer;

    private void Start()
    {
        _player = GetComponent<Player>();
        if (GhostGroup == null)
            GhostGroup = new GameObject("GhostGroup");
        GhostInit();
    }

    private void Update()
    {
        if (Time.timeScale < 1f)
        {
            isOne = true;
            if (_timer > 0)
            {
                _timer -= Time.unscaledDeltaTime;
            }
            else
            {
                if (_player.State != PlayerState.Idle && _player.State != PlayerState.Attack && _player.State != PlayerState.Combo)
                {
                    Timer += Delay;
                    _timer = Delay;
                    DrawGhost();
                }
            }
        }
        else if (isOne)
        {
            for (int i = 0; i < GhostGroup.transform.childCount; i++)
            {
                GhostGroup.transform.GetChild(i).gameObject.SetActive(false);
                _ghostPool.Push(GhostGroup.transform.GetChild(i).gameObject);
            }
            isOne = false;
        }
    }

    private void GhostInit()
    {
        for (int i = 0; i < 20; i++)
            _ghostPool.Push(CreateGhost());
    }

    /// <summary>
    /// 잔상을 생성해주는 함수입니다.
    /// </summary>
    /// 
    private GameObject CreateGhost()
    {
        // 생성, 크기조절
        GameObject ghostObj = Instantiate(GhostPrefab, Vector2.zero, Quaternion.identity, GhostGroup.transform);
        ghostObj.transform.localScale = _player.transform.localScale;
        ghostObj.SetActive(false);
        return ghostObj;
    }
    private void DrawGhost()
    {
        if (_ghostPool.Count > 0)
        {
            GameObject g = _ghostPool.Pop();
            g.transform.position = transform.position;
            g.transform.rotation = transform.rotation;

            _spriteRenderer = g.GetComponent<SpriteRenderer>();
            _spriteRenderer.sprite = _player.SpriteRender.sprite;
            _spriteRenderer.flipX = _player.SpriteRender.flipX;
            Color newColor = new Color(
                Mathf.Lerp(StartColor.r, EndColor.r, Mathf.Abs(Mathf.Sin(Timer))),
                Mathf.Lerp(StartColor.g, EndColor.g, Mathf.Abs(Mathf.Sin(Timer))),
                Mathf.Lerp(StartColor.b, EndColor.b, Mathf.Abs(Mathf.Sin(Timer))),
                0.5f);
            _spriteRenderer.color = newColor;
            //_spriteRenderer.color = color;
            g.SetActive(true);
        }
        else
        {
            _ghostPool.Push(CreateGhost());
            DrawGhost();
        }
    }

    //private void CreateGhost()
    //{
    //    // 생성, 크기조절, 삭제 타이머
    //    GameObject ghostObj = Instantiate(GhostPrefab, transform.position, transform.rotation);
    //    ghostObj.transform.localScale = _player.transform.localScale;
    //    Destroy(ghostObj, DestroyTime);

    //    // 생성된 오브젝트의 스프라이트 수정
    //    _spriteRenderer = ghostObj.GetComponent<SpriteRenderer>();
    //    _spriteRenderer.sprite = _player.SpriteRender.sprite;
    //    _spriteRenderer.flipX = _player.SpriteRender.flipX;
    //    Color newColor = new Color(
    //        Mathf.Lerp(StartColor.r, EndColor.r, Mathf.Abs(Mathf.Sin(Timer))),
    //        Mathf.Lerp(StartColor.g, EndColor.g, Mathf.Abs(Mathf.Sin(Timer))),
    //        Mathf.Lerp(StartColor.b, EndColor.b, Mathf.Abs(Mathf.Sin(Timer))),
    //        0.5f);
    //    _spriteRenderer.color = newColor;
    //    //_spriteRenderer.color = color;
    //}

}