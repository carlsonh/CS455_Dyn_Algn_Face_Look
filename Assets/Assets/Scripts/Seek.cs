using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seek
{
    public Kinematic character;
    public GameObject target;

    public float maxAcceleration = 1f;

    public virtual SteeringOutput getSteering()
    {
        SteeringOutput result = new SteeringOutput();

        //Get dir to target
        result.linear = target.transform.position - character.transform.position;

        //Give full accel
        result.linear.Normalize();
        result.linear *= maxAcceleration;

        return result;
    }
    
}
