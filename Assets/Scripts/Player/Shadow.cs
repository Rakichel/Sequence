using PlayerInfo;
using UnityEngine;

public class Shadow : MonoBehaviour
{
    public Player parent;
    public SpriteRenderer sprite;
    private SpriteRenderer mySprite;
    // Start is called before the first frame update
    void Start()
    {
        if (parent == null)
        {
            parent = GameObject.Find("Player").GetComponent<Player>();
            sprite = parent.GetComponent<SpriteRenderer>();
            mySprite = GetComponent<SpriteRenderer>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (parent.Direction == PlayerDirection.Right)
        {
            mySprite.flipX = false;
            transform.localPosition = new Vector3(-0.3f, 0);
        }
        else if (parent.Direction == PlayerDirection.Left)
        {
            mySprite.flipX = true;
            transform.localPosition = new Vector3(0.3f, 0);
        }
        mySprite.sprite = sprite.sprite;
    }
}
