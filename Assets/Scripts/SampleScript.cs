using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampleScript : MonoBehaviour
{
    private Vector3 startPosition;
    private Vector3 newPosition;
    void Start()
    {
       

    }

    void Update()
    {
        startPosition  = transform.position;
        newPosition = new Vector3(startPosition.x + 0.1f * Time.deltaTime, startPosition.y, startPosition.z);
        if (newPosition.x < 2.3f)
        {
            gameObject.transform.position = newPosition;
        }
    }
}
