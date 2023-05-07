using BossInfo;
using UnityEngine;

public class Dash : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void De()
    {
        Destroy(gameObject);
    }

    public void sActive(bool b)
    {
        gameObject.SetActive(b);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            collision.GetComponent<Enemy>().GetDamage(10);
        }
        else if (collision.CompareTag("Boss"))
        {
            collision.GetComponent<BossController>().GetDamage(10);
        }
    }
}
