using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;


public class Follower : DeployShields
{
    public PathCreator pathCreator;
    public float speed = 5;
    float distanceTravelled;
    public EndOfPathInstruction end;
    public EndOfPathInstruction reverse;

    public DeployShields crankObj;
    
    private Vector3 offSet;
    void Start()
    {
        transform.position = pathCreator.path.GetPointAtDistance(2, end);
        distanceTravelled = 2;
    }

    // Update is called once per frame
    void Update()
    {
        //distanceTravelled += speed * Time.deltaTime;
        //transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled, end);
        Crank(crankObj.distanceCrank);
    }

    public void Crank(float distance)
    {
        //Debug.Log(distance);
        transform.position = pathCreator.path.GetPointAtDistance(distance/15, end);
    }

  

}
