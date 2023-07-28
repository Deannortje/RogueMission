using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeingRepaired : MonoBehaviour
{
    public GameObject thisObj;
    // Start is called before the first frame update
    void OnCollisionEnter(Collision collision)
    {
        //Debug.Log("OnCollisionEnter");
        if(collision.gameObject.tag == "RepairJuice")
        {
            thisObj.SetActive(false);
        }
        
    }
}
