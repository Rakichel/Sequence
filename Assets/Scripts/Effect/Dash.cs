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
}
