using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageMaker : MonoBehaviour
{
    [SerializeField] private GameObject[] Block;
    [SerializeField] private Transform StartPos;
    [SerializeField] private Transform LastPos;
    [SerializeField] private float BDistance = 18.5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.O))
        {
            for (float i = 0; i < 10; i++)
            {
                var newBlock = Instantiate(Block[0], gameObject.transform.position + new Vector3(BDistance * i, 0, 0), gameObject.transform.rotation);
            }
        }
    }
}
