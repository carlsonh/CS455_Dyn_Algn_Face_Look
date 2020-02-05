using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Separation : Seek
{
    public GameObject[] neighborhood;
    float neighborhoodSizeUnits;
    float decayCoeff = 1.0f;
    float maxAcceleration = 5.0f;

    public override SteeringOutput getSteering()
    {
        SteeringOutput result = new SteeringOutput();

        neighborhood = GameObject.FindGameObjectsWithTag("neighbor");

        foreach (GameObject neighbor in neighborhood)
        {
            Vector3 direction = neighbor.transform.position - character.transform.position;
            float distance = direction.magnitude;


            if(distance < neighborhoodSizeUnits)
            {
                float strength = Mathf.Min(decayCoeff / (distance * distance), maxAcceleration);

                direction.Normalize();
                result.linear += strength * direction;
            }
        }

        return result;
    }

}
