using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class WireManager : MonoBehaviour
{
    //public GameObject wire;
    public float time;
    public float totalTime;
    public bool isChanging;
    public Color[] colours;
    public GameObject[] wires;
    public GameObject[] wireSlots;

    public bool isRunning;

    public SpawnFire fire;
    public GameObject electricity;

    public int pluggedOut = 0;
    
    void startChanging()
    {
        //enable the animation of the Light child object
        //Debug.Log("Changing");
        //Then for a random time between 10-20 seconds, change the color of the light to another colour from the colour array
        Color[] tempColorArr = colours;
        int counter = 0;
        foreach(GameObject i in wires)
        {
            GetChildWithName(i, "Light").GetComponent<Animator>().enabled = true;
            int rand = UnityEngine.Random.Range(counter,4);
            GetChildWithName(i, "Light").GetComponent<Light>().color = colours[rand];
            Color temp = colours[rand];
            colours[rand] = colours[counter];
            colours[counter] = temp;
            counter++;
        }
        StartCoroutine(FlashingRunTimer());
        //disable the animation of the Light child object
        //change color of the object to the colour of the light
        //continue with fixed update checking if the cable is in the right position and spark if in wrong position
        //if in wrong position for more than 1 min - start fire
    }
    void changeColour()
    {
        foreach(GameObject i in wires)
        {
            GetChildWithName(i, "Light").GetComponentInChildren<Animator>().enabled = false;
            GetChildWithName(i, "pPipe1").GetComponent<Renderer>().material.color = GetChildWithName(i, "Light").GetComponent<Light>().color;
        }
    }
    bool checkIfAllPluggedIn()
    {
        foreach(GameObject i in wires)
        {
            if(!i.GetComponent<WireScript>().IsConnected)
            {
                //Debug.Log("Something Isnt connected");
                return false;
            }
        }
        //Debug.Log("All Plugged In");
        pluggedOut = 0;
        return true;
    }

    bool checkIfPluggedIntoCorrectSlot()
    {
        foreach(GameObject wire in wires)
        { 
            if(wire.GetComponent<WireScript>()==null){
                ///Debug.Log("All Plugged in InCorrectly NULL");
                return false;
                
            }
            //yourFloat = Mathf.Round(yourFloat * 100f) / 100f;
            double wireSlotr = Math.Ceiling(wire.GetComponent<WireScript>().wireSlot.GetComponent<Renderer>().material.color.r * 100f);
            double wireSlotg = Math.Ceiling(wire.GetComponent<WireScript>().wireSlot.GetComponent<Renderer>().material.color.g * 100f);
            double wireSlotb = Math.Ceiling(wire.GetComponent<WireScript>().wireSlot.GetComponent<Renderer>().material.color.b * 100f);

            double wireColorr = Math.Ceiling(GetChildWithName(wire, "pPipe1").GetComponent<Renderer>().material.color.r * 100f);
            double wireColorg = Math.Ceiling(GetChildWithName(wire, "pPipe1").GetComponent<Renderer>().material.color.g * 100f);
            double wireColorb = Math.Ceiling(GetChildWithName(wire, "pPipe1").GetComponent<Renderer>().material.color.b * 100f);

            if(wireSlotr != wireColorr || wireSlotb != wireColorb || wireSlotg != wireColorg)
            {
                Debug.Log(wireSlotr + " == " + wireColorr);
                Debug.Log(GetChildWithName(wire, "pPipe1").GetComponent<Renderer>().material.color);
                Debug.Log(wire.GetComponent<WireScript>().wireSlot.GetComponent<Renderer>().material.color);
                //Debug.Log("All Plugged in InCorrectly WRONG COLOR");
                return false;
            }
        }
        //Debug.Log("All Plugged In Correctly");
        return true;
        
    }

    void startCoroutineTimerForFire()
    {
        StartCoroutine(FireRunTimer());
    }

    void FixedUpdate()
    {
        time += Time.deltaTime;
        if(isChanging)
        {
            totalTime += Time.deltaTime;
        }
        
        if (time > 1)
        {
            
            if(checkIfAllPluggedIn())
            {

                if(!checkIfPluggedIntoCorrectSlot())
                {
                    electricity.SetActive(true);
                    startCoroutineTimerForFire();
                }
                else
                {
                    electricity.SetActive(false);
                    //Debug.Log("HERE");
                    if(!isChanging && isRunning)
                    {
                        //Debug.Log("Changing Again");
                        startChanging();
                        isChanging = true;
                    }
                }
            }
            else
            {
                
                if(pluggedOut == 40){
                    //Debug.Log("Plugged out for 40 seconds");
                    startCoroutineTimerForFire();
                }
                
                pluggedOut++;
            }
            time = 0;
        }

        if(totalTime > 50 && isRunning)
        {
            isChanging = false;
            isRunning = false;
            //Debug.Log(totalTime + " Event ending");
            totalTime = 0;
        }
    }

    IEnumerator FlashingRunTimer()
    {
        int rand = UnityEngine.Random.Range(5,10);
        yield return new WaitForSecondsRealtime(rand);
        changeColour();
        yield return new WaitForSecondsRealtime(10);
        isChanging = false;
        //Debug.Log("false is changing");
    }
    
    IEnumerator FireRunTimer()
    {
        yield return new WaitForSecondsRealtime(7);
        if(!checkIfPluggedIntoCorrectSlot() && !fire.isRunning)
        {
            //Debug.Log("Fire Started");
            fire.StartCoroutine(fire.RunTimer());
        }
    }
    public IEnumerator RunTimer()
    {
        int rand = UnityEngine.Random.Range(0,2);
        yield return new WaitForSecondsRealtime(rand);
        isChanging = true;
        totalTime = 0;
        startChanging();
    }
    GameObject GetChildWithName(GameObject obj, string name) 
    {
        Transform trans = obj.transform;
        Transform childTrans = trans.Find(name);
        if (childTrans != null) {
            return childTrans.gameObject;
        } else {
            return null;
        }
    }


}
