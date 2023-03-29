using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerInfo;
public class GhostTrail : MonoBehaviour
{
    private Player player;
    private SpriteRenderer spriteRenderer;  // ���� ������ ghost�� SpriteRenderer�� �����ϱ� ���� ����
    private float _timer = 0;               // �ֱ������� �����ϱ� ���� Ÿ�̸�

    public GameObject GhostPrefab;          // ������ Prefab
    public float Delay = 1.0f;              // �����ϴµ� �ɸ��� �ð�
    public float destroyTime = 0.1f;        // ������ ������Ʈ�� �����Ǵ� �ð�
    public Color color;                     // �ܻ� �� ����

    private void Start()
    {
        player = GetComponent<Player>();
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
        ghostObj.transform.localScale = player.transform.localScale;
        Destroy(ghostObj, destroyTime);

        // ������ ������Ʈ�� ��������Ʈ ����
        spriteRenderer = ghostObj.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = player.SpriteRender.sprite;
        spriteRenderer.color = color;
    }
}