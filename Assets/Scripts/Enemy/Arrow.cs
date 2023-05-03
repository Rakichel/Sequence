using PlayerInfo;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    Transform target;
    Vector3 direct;
    void Start()
    {
        target = GameObject.FindWithTag("Player").transform;
        direct = (target.position + new Vector3(0, 0.5f)) - transform.position;
        direct.Normalize();
        Destroy(gameObject, 10f);
    }

    void FixedUpdate()
    {
        transform.position += direct * 10f * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerHit>().GetDamage(10);
            Destroy(gameObject);
        }
    }
}
