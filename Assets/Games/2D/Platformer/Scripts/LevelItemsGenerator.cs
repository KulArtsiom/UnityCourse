using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelItemsGenerator : MonoBehaviour
{
    
    public int platformSize = 3;
    public GameObject firstPlatform;
    public GameObject midlePlatform;
    public GameObject lastPlatform;
    
    [ContextMenu("Generate Platform")]
    private void GeneratePlatform()
    {
        var first = Instantiate(firstPlatform);
        var pos = first.transform.position;
        pos.x += 4;
        first.transform.position = pos;
        var midle = Instantiate(midlePlatform);
        var posMidle = first.transform.position;
        posMidle.x += 1.25f;
        midle.transform.position = posMidle;
        var last = Instantiate(lastPlatform);
        var lastPos = midle.transform.position;
        lastPos.x += 1.25f;
        last.transform.position = lastPos;
    }
    
    
}
