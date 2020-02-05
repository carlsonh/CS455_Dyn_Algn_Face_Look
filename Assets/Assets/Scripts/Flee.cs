using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flee
{
    public Kinematic character;
    public GameObject target;

    public float maxAcceleration;

    public SteeringOutput getSteering()
    {
        SteeringOutput result = new SteeringOutput();

        //Get dir to target
        result.linear = character.transform.position - target.transform.position;

        //Give full accel
        result.linear.Normalize();
        //result.linear *= maxAcceleration;

        return result;
    }
    
}
