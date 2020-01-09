using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelItemsGenerator : MonoBehaviour
{
    
    public int platformSize;
    public GameObject firstPlatform;
    public GameObject midlePlatform;
    public GameObject lastPlatform;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    
    [ContextMenu("Generate Platform")]
    private void GeneratePlatform()
    {
        
        var first = Instantiate(firstPlatform);
        var pos = first.transform.position;
        pos.x += 4;
        first.transform.position = pos;
        for (int i = 0; i <= platformSize; i++)
        {
            var midle = Instantiate(midlePlatform);
            var posMidle = first.transform.position;
            posMidle.x += 1.25f;
            midle.transform.position = posMidle;
            if (i == platformSize)
            {
                var last = Instantiate(lastPlatform);
                var lastPos = midle.transform.position;
                lastPos.x += 1.25f;
                last.transform.position = lastPos;
            }
        }
    }
    
    
}
