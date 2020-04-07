using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteJoint : MonoBehaviour
{
    public SpriteJoint previous;
    public SpriteJoint next;
    public new SpriteRenderer renderer;

    private void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (previous != null)
        {
            Vector3 newLocation = previous.transform.localPosition;
            float radius = renderer.sprite.bounds.size.y; 
            Vector3 centerPosition = renderer.transform.localPosition; 
            float distance = Vector3.Distance(newLocation, centerPosition);

            if (distance > radius)
            {
                Vector3 fromOriginToObject = newLocation - centerPosition; //~GreenPosition~ - *BlackCenter*
                fromOriginToObject *= radius / distance; //Multiply by radius //Divide by Distance
                newLocation = centerPosition + fromOriginToObject; //*BlackCenter* + all that Math
                previous.transform.localPosition = newLocation;
            }
        }
        if (next != null)
        {
            Vector3 newLocation = next.transform.localPosition;
            float radius = renderer.sprite.bounds.size.y;
            Vector3 centerPosition = renderer.transform.localPosition;
            float distance = Vector3.Distance(newLocation, centerPosition);

            if (distance > radius)
            {
                Vector3 fromOriginToObject = newLocation - centerPosition; //~GreenPosition~ - *BlackCenter*
                fromOriginToObject *= radius / distance; //Multiply by radius //Divide by Distance
                newLocation = centerPosition + fromOriginToObject; //*BlackCenter* + all that Math
                next.transform.localPosition = newLocation;
            }
        }
    }
}
