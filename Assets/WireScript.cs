using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WireScript : MonoBehaviour
{
    public bool IsConnected;
    public string currentColour;

    public AudioSource click;

    public GameObject wireSlot;
    
    void OnTriggerEnter(Collider collision)
    {
        //Debug.Log("Trigger entered");
        if(collision.GetComponent<Collider>().gameObject.tag == "wireSlot")
        {
            wireSlot = collision.GetComponent<Collider>().gameObject;
            IsConnected = true;
            click.Play();
        }
        
        
        //Debug.Log(wireSlot.name);
        
    }

    void OnTriggerExit(Collider collision)
    {
        //Debug.Log(collision.GetComponent<Collider>().tag);
        if(collision.GetComponent<Collider>().gameObject.tag == "wireSlot")
        {
            wireSlot = collision.GetComponent<Collider>().gameObject;
            IsConnected = false;
            click.Play();
        }
        
        //Debug.Log(wireSlot.name);
        
    }

    
}
