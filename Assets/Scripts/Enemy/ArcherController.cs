using PlayerInfo;
using UnityEngine;

public class ArcherController : MonoBehaviour
{
    private Animator Animator;

    public int hp = 10;
    public GameObject Arrow;
    Player target;

    // Start is called before the first frame update
    void Start()
    {
        Animator = GetComponent<Animator>();
        target = GameObject.FindWithTag("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
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

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, 10f);
    }
}
