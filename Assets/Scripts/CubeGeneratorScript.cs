using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CubeGeneratorScript : MonoBehaviour
{

    private float randomX;
    private float randomZ;
    private float customY;
    private float minX;
    private float maxX;
    private float minZ;
    private float maxZ;
    
    
    
    
    void Start()
    {
        MeshRenderer meshRenderer = gameObject.GetComponent<MeshRenderer>();

        var bounds = meshRenderer.bounds;
         minX = bounds.min.x;
         maxX = bounds.max.x;
         minZ = bounds.min.z;
         maxZ = bounds.max.z;

         
         customY = gameObject.transform.position.y + 5;     
    }

    // Update is called once per frame
    void Update()
    {
        randomX = Random.Range(minX, maxX);
        randomZ = Random.Range(minZ, maxZ);
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            cube.AddComponent<Rigidbody>();
            cube.transform.position = new Vector3(randomX, customY, randomZ);
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        collision.gameObject.GetComponent<Rigidbody>().AddExplosionForce(1000, collision.contacts[0].point, 100);
    }
}
