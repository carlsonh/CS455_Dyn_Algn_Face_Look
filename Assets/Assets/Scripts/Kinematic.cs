using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kinematic : MonoBehaviour
{

    //pos comes from attached go tx

    public Vector3 linearVelocity;
    public float angularVelocity; //Deg
    public GameObject target;

    public bool bIsSeeking;
    private Seek mySeek;
    private SteeringOutput seekSteer;
    private Flee myFlee;
    private SteeringOutput fleeSteer;

    public bool bCanArrive;
    private Arrive myArrv;
    private SteeringOutput arrvSteer;

    public bool bDoesAlign;
    private Align myAlgn;
    private SteeringOutput algnSteer;

    public bool bDoesFace;
    private Face myFace;
    private SteeringOutput faceSteer;

    public bool bDoesLookWhereGoing;
    private LookWhereGoing myLWYG;
    private SteeringOutput lwygSteer;


    public bool bDoesFollowPath;
    public GameObject[] waypointList;
    private FollowPathDemo myFPth;
    private SteeringOutput fPthSteer;


    // Start is called before the first frame update
    void Start()
    {

        mySeek = new Seek();
        mySeek.character = this;
        mySeek.target = target;
        myFlee = new Flee();
        myFlee.character = this;
        myFlee.target = target;

        myArrv = new Arrive();
        myArrv.character = this;
        myArrv.target = target;

        myAlgn = new Align();
        myAlgn.character = this;
        myAlgn.target = target;

        myFace = new Face();
        myFace.character = this;
        myFace.target = target;

        myLWYG = new LookWhereGoing();
        myLWYG.character = this;
        myLWYG.target = target;

        myFPth = new FollowPathDemo();
        myFPth.character = this;
        myFPth.path = waypointList;
        //myFPth.target = target;
    }


    // Update is called once per frame
    void Update()
    {
        //Update pos rot
        transform.position += linearVelocity * Time.deltaTime;
        Vector3 angularIncrement = new Vector3(0, angularVelocity * Time.deltaTime, 0);
        if (float.IsNaN(angularIncrement.y))
        { } else
        {
            transform.eulerAngles += angularIncrement;
        }
        //Debug.Log(angularIncrement);

        SteeringOutput steering = new SteeringOutput();
        //Get Steerings
       if(bIsSeeking)
        {
            steering = mySeek.getSteering();
        } else
        {
            //steering = myFlee.getSteering();
        }
        if(bCanArrive)
        {
            
            SteeringOutput _arrvSteering = myArrv.getSteering();
            if(_arrvSteering != null)
            {
                linearVelocity += _arrvSteering.linear * Time.deltaTime;
                angularVelocity += _arrvSteering.angular * Time.deltaTime;
            }
            else
            {
                linearVelocity = Vector3.zero;
            }
        }
        if(bDoesAlign)
        {
            SteeringOutput _algnSteering = myAlgn.getSteering();
            if(_algnSteering != null)
            {
                steering.angular += _algnSteering.angular * Time.deltaTime;
            }
            else
            {
                angularVelocity = 0f;
            }
        }

        if(bDoesFace)
        {
            SteeringOutput _faceSteering = myFace.getSteering();
            if(_faceSteering != null)
            {
                angularVelocity += _faceSteering.angular * Time.deltaTime;
            }
        }
        if(bDoesLookWhereGoing)
        {
            SteeringOutput _lwygSteering = myLWYG.getSteering();
            if(_lwygSteering != null)
            {
                angularVelocity += _lwygSteering.angular * Time.deltaTime;
            }
        }
        if(bDoesFollowPath)
        {
            SteeringOutput _fpthSteering = myFPth.getSteering();
            if(_fpthSteering != null)
            {
                linearVelocity += _fpthSteering.linear * Time.deltaTime;
            }
        }

        //update lin ang vel
        
        linearVelocity += steering.linear * Time.deltaTime;
        angularVelocity += steering.angular * Time.deltaTime;

    }
}
