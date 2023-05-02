using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectInitail : MonoBehaviour
{
    public GameObject[] Arrows;
    int index = 0;
    // Start is called before the first frame update
    void Start()
    {
        Arrows[index].SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            Arrows[index].SetActive(false);
            index++;
            if (index > Arrows.Length)
                index = 0;
            Arrows[index].SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Arrows[index].SetActive(false);
            index--;
            if (index < 0)
                index = Arrows.Length;
            Arrows[index].SetActive(true);
        }
    }
}
