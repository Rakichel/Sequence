using UnityEngine;

public class ArcherSpawn : MonoBehaviour
{
    public GameObject Archer;
    public float SpawnTime = 15f;
    public float Cycle = 10f;
    public float Timer = 0f;

    void Update()
    {
        if (transform.childCount == 0)
            Timer += Time.deltaTime;

        if (Timer > SpawnTime)
        {
            Instantiate(Archer, transform);
            SpawnTime += Cycle;
        }
    }
}
