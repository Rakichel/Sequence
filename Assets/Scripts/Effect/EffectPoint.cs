using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectPoint : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(gameObject.transform.GetComponentInParent<SpriteRenderer>().flipX)
        {
            gameObject.transform.localPosition = new Vector3(0.9f, 0.7f, 0);
        }
        else
        {
            gameObject.transform.localPosition = new Vector3(-0.9f, 0.7f, 0);
        }
    }
}
