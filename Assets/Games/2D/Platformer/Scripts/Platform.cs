using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class Platform : MonoBehaviour
{
    [SerializeField] private bool isAnimated;
    [SerializeField] private float animationDistance = 5f;
    private float startX;
    private bool isEffector;
    private SurfaceEffector2D surfaceEffector2D;
    
    
    private void Start()
    {
        startX = transform.position.x;
        if (isAnimated)
        {
            StartCoroutine(AnimatorProccess());
        }

        isEffector = GetComponent<SurfaceEffector2D>();
        if (isEffector)
        {
            surfaceEffector2D = GetComponent<SurfaceEffector2D>();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        bool isMovedObject = other.GetComponent<CharacterController>();
        if (isMovedObject)
        {
            other.transform.parent = transform;
        }

        if (isEffector)
        {
            surfaceEffector2D.speed = Random.Range(-5f, 5f);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.transform.parent == transform)
        {
            other.transform.parent = null;
        }
    }

    private IEnumerator AnimatorProccess()
    {
        var rightMovement = true;
        var delta = 0f;

        while (true)
        {
            delta += Time.deltaTime;
            if (delta > 1f)
            {
                delta = 0;
                rightMovement = !rightMovement;
            }

            Vector3 pos = transform.position;
            var from = rightMovement ? startX : startX + animationDistance;
            var to = rightMovement ? startX + animationDistance : startX;
            pos.x = Mathf.Lerp(from, to, delta);
            transform.position = pos;
            yield return null;
        }
        // ReSharper disable once IteratorNeverReturns
    }
}