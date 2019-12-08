using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HopCamera : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Transform m_target;
    [SerializeField] private float m_distance = 2f;
    [SerializeField] private float m_height = 2f;

    void Update()
    {
        Vector3 pos = new Vector3(0f, m_height, m_target.position.z - m_distance);
        transform.position = Vector3.Lerp(transform.position, pos, Time.deltaTime * 5f);
        
    }
}
