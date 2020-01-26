using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class LevelItemsGenerator : MonoBehaviour
{
    public int platformSize = 10;

    public GameObject firstPlatform;
    //  public GameObject midlePlatform;
    // public GameObject lastPlatform;
    

    [ContextMenu("Generate Platform")]
    private void GeneratePlatform()
    {
        var listPlatforms = new List<GameObject>();
        for (int i = 0; i < 10; i++)
        {
            var clone = Instantiate(firstPlatform);
            listPlatforms.Add(clone);
            
            
        }

        Debug.Log("List count: " + listPlatforms.Count);
        
        var randomY = Random.Range(-7.13f, -2);

       
        
        if (listPlatforms.Count > 0)
        {
            var item = listPlatforms[listPlatforms.Count - 1];
            var pos = item.transform.position;
            var randomX = Random.Range(pos.x + 2f, 5f);
            pos.y = randomY;
            pos.x = randomX;
            item.transform.position = pos;
            listPlatforms.Add(item);
        }
        else
        {
            var clone = Instantiate(firstPlatform);
            var pos = clone.transform.position;
            pos.y = randomY;
            var randomX = Random.Range(pos.x + 5f, 10f);
            pos.x = randomX;
            clone.transform.position = pos;
            listPlatforms.Add(clone);
        }


        // var midle = Instantiate(midlePlatform);
        // var posMidle = first.transform.position;
        // posMidle.x += 1.25f;
        // midle.transform.position = posMidle;
        // var last = Instantiate(lastPlatform);
        // var lastPos = midle.transform.position;
        // lastPos.x += 1.25f;
        // last.transform.position = lastPos;
    }
}