using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerInfo;
public class GhostTrail : MonoBehaviour
{
    public GameObject ghostPrefab;
    public float delay = 1.0f;
    float delta = 0;

    Player player;
    SpriteRenderer spriteRenderer;
    public float destroyTime = 0.1f;
    public Color color;
    public Material material = null;

    private void Start()
    {
        player = GetComponent<Player>();
    }

    private void Update()
    {
        if(delta > 0) { delta -= Time.deltaTime; }
        else { delta = delay; createGhost(); }
    }

    void createGhost()
    {
        GameObject ghostObj = Instantiate(ghostPrefab, transform.position, transform.rotation);
        ghostObj.transform.localScale = player.transform.localScale;
        Destroy(ghostObj, destroyTime);
        spriteRenderer = ghostObj.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = player.spriteRenderer.sprite;
        spriteRenderer.color = color;

        if (material != null) spriteRenderer.material = material;
    }
}