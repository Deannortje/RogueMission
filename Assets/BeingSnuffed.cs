using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeingSnuffed : MonoBehaviour
{
    public GameObject thisObj;
    // Start is called before the first frame update
    void OnCollisionEnter(Collision collision)
    {
        //Debug.Log("OnCollisionEnter of Fire");
        if(collision.gameObject.tag == "Foam")
        {
            //Debug.Log("Fire DIE");
            Destroy(thisObj,0.5f);
            //thisObj.SetActive(false);
        }
        
    }
}
