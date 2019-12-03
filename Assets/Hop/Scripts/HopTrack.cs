using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HopTrack : MonoBehaviour
{
    [SerializeField] private GameObject m_Platform;
    [SerializeField] private GameObject m_GreenPlatform;
    
    private List<GameObject> platforms = new List<GameObject>();
    
    void Start()
    {
        
        platforms.Add(m_Platform);

        for (int i = 0; i < 25; i++)
        {
            GameObject obj = Instantiate(m_Platform, transform);
            Vector3 pos = Vector3.zero;

            pos.z = 2 * (i + 1);
            pos.x = Random.Range(-1, 2);
            obj.transform.position = pos;
            obj.name = $"Platform_{i + 1}";
            platforms.Add(obj);
        }
    }

    public bool isBallOnPlatform(Vector3 position)
    {
        position.y = 0f;

        var nearestPlatform = platforms[0];
        
        /*
         * NOTE!!! Method for find nearestPlatform!!! 
         */
        
        for (int i = 1; i < platforms.Count; i++)
        {
            var platformZ = platforms[i].transform.position.z;
            
            if (platformZ+ 0.5f < position.z)
            {
                continue;
            }

            if (platformZ - position.z > 0.5f)
            {
                continue;
            }

            nearestPlatform = platforms[i];
            
            break;
        }
        
        
        
        float minX = nearestPlatform.transform.position.x - 0.5f;
        float maxX = nearestPlatform.transform.position.x + 0.5f;

        var platform = nearestPlatform.GetComponent<HopPlatform>();
        platform.SetGreen();
        
        return position.x > minX && position.x < maxX;
    }
    
    
    
}
