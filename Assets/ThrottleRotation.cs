using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit.Utilities;
using UnityEngine.XR.Interaction.Toolkit.Utilities.Pooling;



public class ThrottleRotation : MonoBehaviour
{
    // Start is called before the first frame update

    public GripButtonWatcher Watcher;
    public Transform throttleCol; //this collider
    public Transform RightHand;
    public Transform LeftHand;

    public bool rightHandGrabed = false;
    public bool leftHandGrabed = false;

    public void OnGripButtonEvent(bool pressed)
    {
        if(pressed){
            //Debug.Log("Grip button pressed");
            if(GrabRange(throttleCol,RightHand))//collidedWithHand
            {
                Debug.Log("collided with Right hand");
                rightHandGrabed = true;//rotate throttle
            }
            else if(GrabRange(throttleCol,LeftHand))//collidedWithHand
            {
                Debug.Log("collided with Left hand");
                leftHandGrabed = true;
                //rotate throttle
            }
            else
            {
               Debug.Log("Ya missed it YA DINGUS"); 
            }
        }
        else{
            Debug.Log("Grip button released");
            
            rightHandGrabed = false;
            leftHandGrabed = false;
        }
    }

    void Start()
    {
        Watcher.GripButtonPress.AddListener(OnGripButtonEvent);
        throttleCol = this.GetComponent<Transform>();
        RightHand = GameObject.Find("RightHand").GetComponent<Transform>();
        LeftHand = GameObject.Find("LeftHand").GetComponent<Transform>();
    }


    // Update is called once per frame
    void Update()
    {
        if(rightHandGrabed)
        {
            //throttleCol.LookAt(RightHand);
            throttleCol.rotation = getQuaternion(throttleCol,RightHand);
            
        }
        else if(leftHandGrabed)
        {
            
        }
      



    }

    public bool GrabRange(Transform a,Transform b){
        if (Vector3.Distance(a.position,b.position)<0.1){
            return true;
        }
        else{
            return false;
        }
    }
    public Quaternion getQuaternion(Transform A, Transform B){
        
        Quaternion q = Quaternion.FromToRotation(A.position,B.position);

        Quaternion rotation = Quaternion.Euler(0, q.x, 0);
        //q.x = clampRotation(q.x);
        // q.y = 0;
        // q.z = -90;
        return q;
    }
    public float clampRotation(float i)
    {
        if(i > -13 ){return -13;}
        if(i<-180 ){return -180;}

        return i;
    }
}
