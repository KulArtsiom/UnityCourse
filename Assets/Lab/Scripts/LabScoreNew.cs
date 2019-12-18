using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LabScoreNew : MonoBehaviour
{
    // Start is called before the first frame update
    public static int scoreValueGates = 0;
    private Slider slider;

    void Start()
    {
        slider = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        slider.value = scoreValueGates;
    }
}
