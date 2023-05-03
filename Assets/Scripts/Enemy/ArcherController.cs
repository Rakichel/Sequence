using PlayerInfo;
using UnityEngine;

public class ArcherController : MonoBehaviour
{
    private Animator Animator;
    private Player target;
    private SpriteRenderer archerSprite;
    private float colorTimer = 1f;

    public int hp = 50;
    public GameObject Arrow;
    public bool dieShader = false;

    // Start is called before the first frame update
    void Start()
    {
        Animator = GetComponent<Animator>();
        target = GameObject.FindWithTag("Player").GetComponent<Player>();
        archerSprite = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (dieShader)
        {
            Manager.GameManager.Instance.EnemyDie(gameObject.GetComponent<SpriteRenderer>().material);
            Debug.Log("Åº´Ú");
        }
        if (gameObject.GetComponent<SpriteRenderer>().material.GetFloat("_Fade") <= 0)
        {
            Manager.GameManager.Instance.KillCount++;
            Destroy(gameObject);
        }
        archerSprite.color = Color.Lerp(Color.red, Color.white, colorTimer);
        if (colorTimer < 1f)
        {
            colorTimer += 2f * Time.deltaTime;
        }
        if (hp <= 0)
        {
            Animator.SetBool("Shot", false);
            Animator.SetBool("Dead", true);
            return;
        }
        LookDir();
        Collider2D col = Physics2D.OverlapCircle(transform.position, 10f, 1 << 7);
        if (col != null)
        {
            Animator.SetBool("Shot", true);
        }

    }
    public void GetDamage(int damage)
    {
        hp -= damage;
        if(hp > 0)
        {
            colorTimer = 0f;
        }
    }
    public void StopShot()
    {
        Collider2D col = Physics2D.OverlapCircle(transform.position, 10f, 1 << 7);
        if (col == null)
        {
            Animator.SetBool("Shot", false);
        }
    }
    public void CreateArrow()
    {
        Quaternion q = Quaternion.AngleAxis(GetAngle(transform.position, target.transform.position + new Vector3(0f, 0.5f)), Vector3.forward);

        Instantiate(Arrow, transform.position, q);
    }
    public void LookDir()
    {
        if (transform.position.x > target.transform.position.x)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
        else
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }
    }
    private float GetAngle(Vector2 start, Vector2 end)
    {
        Vector2 v2 = end - start;
        return Mathf.Atan2(v2.y, v2.x) * Mathf.Rad2Deg;
    }
    public void Die()
    {
        dieShader = true;
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, 10f);
    }
}
