using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HopTrack : MonoBehaviour
{
    [SerializeField] private GameObject m_Platform;
    [SerializeField] private bool m_useRandomSeed;
    [SerializeField] private int m_seed = 123456;

    private List<GameObject> platforms = new List<GameObject>();

    public float speed = 1f;
    public float height = 1f;

    void Start()
    {
        platforms.Add(m_Platform);

        if (m_useRandomSeed)
        {
            Random.InitState(m_seed);
        }

        for (int i = 0; i < 25; i++)
        {
            GameObject obj = Instantiate(m_Platform, transform);
            Vector3 pos = Vector3.zero;

            pos.z = 2 * (i + 1);
            pos.x = Random.Range(-1, 2);
            obj.transform.position = pos;
            obj.name = $"Platform_{i + 1}";
            int rand = Random.Range(1, 30);
            if (rand > 15)
            {
                obj.GetComponent<HopPlatform>().setPurple();
            }
            else
            {
                obj.GetComponent<HopPlatform>().SetTurquoise();
            }

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

            if (platformZ + 0.5f < position.z)
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

        if (position.x > minX && position.x < maxX)
        {
            var platform = nearestPlatform.GetComponent<HopPlatform>();
            platform.SetGreen();
            switch (platform.platformName)
            {
                case "Purple:":
                    speed = 3f;
                    height = 0.5f;
                    break;
                case "Turquoise":
                    speed = 0.3f;
                    height = 2.5f;
                    break;
                default:
                    speed = 1f;
                    height = 1f;
                    break;
            }
        }
        else
        {
            var platform = nearestPlatform.GetComponent<HopPlatform>();
            platform.SetRed();
        }


        return position.x > minX && position.x < maxX;
    }
}