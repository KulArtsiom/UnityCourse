using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCube : MonoBehaviour
{
    [SerializeField] private GameObject m_Platform;
    
    private Quaternion rotationY;
    private Quaternion rotationRight;
    private Quaternion rotationLeft;
    
    private float minX;
    private float maxX;
    private float minZ;
    private float maxZ;

    // Start is called before the first frame update
    void Start()
    {
        MeshRenderer meshRenderer = m_Platform.GetComponent<MeshRenderer>();
        
        var bounds = meshRenderer.bounds;
        minX = bounds.min.x;
        maxX = bounds.max.x;
        minZ = bounds.min.z;
        maxZ = bounds.max.z;
        
//        rotationY = Quaternion.AngleAxis(1, Vector3.up);
//        rotationRight = Quaternion.AngleAxis(1, Vector3.right);
//        rotationLeft = Quaternion.AngleAxis(90, Vector3.left);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            if (gameObject.transform.position.x < maxX )
            {
                gameObject.transform.position += Vector3.right / 2;
            }
            //transform.rotation *= rotationRight;
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            if (gameObject.transform.position.x > minX)
            {
                gameObject.transform.position += Vector3.left / 2;

            }
            //transform.rotation *= rotationLeft;
        }

        if (Input.GetKeyDown(KeyCode.W))
        { 
            gameObject.transform.position += Vector3.forward / 2;
        }
        
        if (Input.GetKeyDown(KeyCode.S))
        { 
            gameObject.transform.position += Vector3.back / 2;
        }
    }

    private void FixedUpdate()
    {
    }
}