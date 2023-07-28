using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;
using UnityEngine.Events;

public class DeployShields : MonoBehaviour
{
    public float distanceCrank = 30;
    // public float rotationCheck;
    public int pointOfCollision;

    public GameObject noise;

    public GameObject shieldBadge;

    public float getPosition()
    {
        return distanceCrank;
    }

    void OnTriggerEnter (Collider collider) {
         
        if (collider.gameObject.tag == "crankPoint1") 
        {
            if (pointOfCollision == 3) {
                distanceCrank++;
            } else if (pointOfCollision == 2) {
                distanceCrank--;
            }
            pointOfCollision = 1;
        }
        else if (collider.gameObject.tag == "crankPoint2") {
            if (pointOfCollision == 1) {
                distanceCrank++;
            } else if (pointOfCollision == 3) {
                distanceCrank--;
            }
            pointOfCollision = 2;
        } else if (collider.gameObject.tag == "crankPoint3") {
            if (pointOfCollision == 2) {
                distanceCrank++;
            } else if (pointOfCollision == 1) {
                distanceCrank--;
            }
            pointOfCollision = 3;
        }

        distanceCrank = Mathf.Clamp(distanceCrank,0, 30);

        

    
        //Debug.Log("distanceCrank: " + (((distanceCrank/3)*10)-100) + "%");

        if(distanceCrank == 0) {
            //Debug.Log("Shields are at " +(((distanceCrank/3)*10)+ "%");
            shieldBadge.GetComponent<Renderer>().material.SetFloat("_Cutoff", 0);
            noise.SetActive(true);
        } else if (distanceCrank == 30 || distanceCrank > 0) {
            shieldBadge.GetComponent<Renderer>().material.SetFloat("_Cutoff", (distanceCrank/30));
            //Debug.Log(shieldBadge.GetComponent<Renderer>().material.GetFloat("_Cutoff"));
            //Debug.Log("Shields at " +(((distanceCrank/3)*10)) + "%");
            noise.SetActive(false);
        }
        
    }

   
}
