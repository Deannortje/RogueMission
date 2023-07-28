using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipScript : MonoBehaviour
{

    public float MaxHealth = 100;
    public float shipHealth;
    public float shipRadiationLevel;
    public float oxygenDumpLevel;
    public float time;

    public GameObject healthBar;
    public GameObject radiationBar;
    public GameObject player;

    public GameObject alarm;

    public GameObject space;

    public GameObject throttle1;
    public GameObject throttle2;

    public GameObject warningText;
    public GameObject warningNoise;
    
    // Start is called before the first frame update
    void Start()
    {

        shipHealth = MaxHealth;
        shipRadiationLevel = 1;

        healthBar.GetComponent<Renderer>().material.SetFloat("_Cutoff", (shipHealth*-1f / MaxHealth)+1f);
        radiationBar.GetComponent<Renderer>().material.SetFloat("_Cutoff", (shipRadiationLevel));

        space.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, -1);
        
    }

    public void shipHealthUpdate(float amount)
    {
        shipHealth += amount;
        if (shipHealth > MaxHealth)
        {
            shipHealth = MaxHealth;
        }
        else if (shipHealth < 0)
        {
            shipHealth = 0;
        }
        healthBar.GetComponent<Renderer>().material.SetFloat("_Cutoff", (shipHealth*-1f / MaxHealth)+1f);
    }

    public void shipRadiationLevelUpdate(float amount, bool stopping)
    {
        
       if(shipRadiationLevel > 0.07f)
       {
            if(!stopping)
            {
                shipRadiationLevel -= 0.1f;
            }
            radiationBar.GetComponent<Renderer>().material.SetFloat("_Cutoff", (shipRadiationLevel));
            //Debug.Log(shipRadiationLevel);
       }

       if(shipRadiationLevel <1.1f){
            if(stopping)
            {
            shipRadiationLevel += 0.1f;
            }
            radiationBar.GetComponent<Renderer>().material.SetFloat("_Cutoff", (shipRadiationLevel));
            //Debug.Log(shipRadiationLevel);
       }
        
        
       
    }
    // Update is called once per frame


    void FixedUpdate()
    {
        time += Time.deltaTime;
        if (time > 1)
        {
            if(player.GetComponent<Player>().getOxygen() < 30)
            {
                //set alarm active
                alarm.SetActive(true);
            }

            shipHealthUpdate(0.5f);

            if(throttle1.GetComponent<Transform>().localEulerAngles.x>300 || throttle1.GetComponent<Transform>().localEulerAngles.x>300)
            {
                //Debug.Log("Fast"+ throttle1.GetComponent<Transform>().localEulerAngles.x);
                space.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, -30);
            }
            else if(throttle1.GetComponent<Transform>().localEulerAngles.x<-250 || throttle1.GetComponent<Transform>().localEulerAngles.x<-250)
            {
                //Debug.Log("Slow");
                space.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, -0.25f);
            }
            else
            {
                //Debug.Log("Normal" + throttle1.GetComponent<Transform>().localEulerAngles.x);
                space.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, -1);
            }
//TODO Chnage that both throttles are needed to be on max to get max speed

            time = 0;
        }
    }

    // IEnumerator WarningLable()
    // {
    //     warningNoise.SetActive(true);
    //     for(int i = 0; i < 6; i++)
    //     {
    //         yield return new WaitForSecondsRealtime(0.5f);
    //         warningText.SetActive(true);
    //         yield return new WaitForSecondsRealtime(1);
    //         warningText.SetActive(false); 
    //     }
    //    warningNoise.SetActive(false);
    // }


}
