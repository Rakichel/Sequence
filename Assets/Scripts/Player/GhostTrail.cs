using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerInfo;
public class GhostTrail : MonoBehaviour
{
    private Player _player;
    private SpriteRenderer _spriteRenderer;     // ���� ������ ghost�� SpriteRenderer�� �����ϱ� ���� ����
    private float _timer = 0;                   // �ֱ������� �����ϱ� ���� Ÿ�̸�

    public GameObject GhostPrefab;          // ������ Prefab
    public float Delay = 1.0f;              // �����ϴµ� �ɸ��� �ð�
    public float DestroyTime = 0.1f;        // ������ ������Ʈ�� �����Ǵ� �ð�
    public Color color;                     // �ܻ� �� ����

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
    /// �ܻ��� �������ִ� �Լ��Դϴ�.
    /// </summary>
    private void CreateGhost()
    {
        // ����, ũ������, ���� Ÿ�̸�
        GameObject ghostObj = Instantiate(GhostPrefab, transform.position, transform.rotation);
        ghostObj.transform.localScale = _player.transform.localScale;
        Destroy(ghostObj, DestroyTime);

        // ������ ������Ʈ�� ��������Ʈ ����
        _spriteRenderer = ghostObj.GetComponent<SpriteRenderer>();
        _spriteRenderer.sprite = _player.SpriteRender.sprite;
        _spriteRenderer.color = color;
    }
}