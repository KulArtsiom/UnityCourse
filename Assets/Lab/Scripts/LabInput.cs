using System;
using UnityEngine;

public class LabInput : MonoBehaviour
{
    private float strafe;

    public float Strafe => strafe;

    private float screenCenter;

    private void Start()
    {
        screenCenter = Screen.width * 0.5f;
    }

    private void Update()
    {
        if (!Input.GetMouseButton(0))
        {
            return;
        }

        var mousePosition = Input.mousePosition.x;
        if (mousePosition > screenCenter)
        {
            strafe = (mousePosition - screenCenter) / screenCenter;
        }
        else
        {
            strafe = 1f - mousePosition / screenCenter;
            strafe *= -1f;
        }
    }
}
