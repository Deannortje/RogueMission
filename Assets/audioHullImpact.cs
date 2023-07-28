using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audioHullImpact : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioSource impactSound;

    public GameObject Ship;
    public float speed = 0.1f; //how fast it shakes
    public float amplitude = 0.1f; //how much it shakes

    private Vector3 originalPos;
    private bool shakeFlag;
    
    void Shake(bool flag)
    {
        if(flag == true)
        {
            Vector3 pos = Ship.transform.position;
            pos.x += Mathf.Sin(Time.time * speed) * amplitude;
            pos.y += Mathf.Sin(Time.time * speed) * amplitude;
            Ship.transform.position = pos;
            
        }
        else
        {
            Ship.transform.position = originalPos;
        }
        
    }

    void Start()
    {
        originalPos = Ship.transform.position;
    }

    void Update()
    {
        if(shakeFlag == true)
        {
            Shake(true);
        }
        else
        {
            Shake(false);
        }
        
    
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Astriod")
        {
            impactSound.Play();
            Ship.GetComponent<ShipScript>().shipHealthUpdate(-10f);
            StartCoroutine(RunTimer());
            
            
        }
        
    }

    IEnumerator RunTimer()
    {
        shakeFlag = true;
        yield return new WaitForSecondsRealtime(2f);
        shakeFlag = false;
    }

}
