using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LabScores : MonoBehaviour
{
    // Start is called before the first frame update
    public static int scoreValue = 0;
    public static int scoreValueGates = 0;
    private Text text;
    private Slider slider;

    void Start()
    {
        text = GetComponent<Text>();
        slider = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        text.text = "Lifes: " + scoreValue;
        slider.value = scoreValueGates;
    }
}
