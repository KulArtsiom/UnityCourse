using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorSnakeScore : MonoBehaviour
{
    // Start is called before the first frame update
    public static int scoreValue = 0;
    private Text text;

    void Start()
    {
        text = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        text.text = "Score: " + scoreValue;
    }
}