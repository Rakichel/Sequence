using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextChage : MonoBehaviour
{
    Text text;
    char alpha;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
        alpha = text.text[0];
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.GetChild(0).gameObject.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                alpha++;
                if (alpha > 'Z')
                    alpha = 'A';
                text.text = alpha.ToString();
            }
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                alpha--;
                if (alpha < 'A')
                    alpha = 'Z';
                text.text = alpha.ToString();
            }
        }
    }
}
