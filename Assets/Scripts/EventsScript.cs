using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EventsScript : MonoBehaviour, IPointerClickHandler
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        float red = Random.Range(.0f, 1f);
        float green = Random.Range(.0f, 1f);
        float blue = Random.Range(.0f, 1f);
        
        Color color = new Color(red, green, blue);
        gameObject.GetComponent<Renderer>().material.color = color;

        Vector3 force = Camera.main.transform.forward  * 100;
        Vector3 target = eventData.pointerPressRaycast.worldPosition;
        
        gameObject.GetComponent<Rigidbody>().AddForceAtPosition(force,target);
    }
}
