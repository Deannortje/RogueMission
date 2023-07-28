using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ButtonVR : MonoBehaviour
{
    public GameObject animation;
    public GameObject oxygenDumpLevel;
    public GameObject player;
    public GameObject alarm;
    public float oxygenDumpLevelFloat;
    //Time that the button is set inactive after release
    public float deadTime = 1.0f;
    //Bool used to lock down button during its set dead time
    private bool _deadTimeActive = false;

    //public Unity Events we can use in the editor and tie other functions to.
    public UnityEvent onPressed, onReleased;

    //Checks if the current collider entering is the Button and sets off OnPressed event.
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Button" && !_deadTimeActive)
        {
            onPressed?.Invoke();
            //Debug.Log("I have been pressed");
            
        
        }
    }

    //Checks if the current collider exiting is the Button and sets off OnReleased event.
    //It will also call a Coroutine to make the button inactive for however long deadTime is set to.
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Button" && !_deadTimeActive)
        {
            onReleased?.Invoke();
            //Debug.Log("I have been released");
            StartCoroutine(WaitForDeadTime());
        }
    }

    //Locks button activity until deadTime has passed and reactivates button activity.
    IEnumerator WaitForDeadTime()
    {
        _deadTimeActive = true;
        yield return new WaitForSeconds(deadTime);
        _deadTimeActive = false;
    }

    public void oxygenDump()
    {
        
        alarm.SetActive(false);
        StartCoroutine(dump());
    }

    IEnumerator dump()
    {
        if(oxygenDumpLevel.GetComponent<Renderer>().material.GetFloat("_Cutoff") < 0.75)
        {
            animation.SetActive(true);
            float temp = (1-oxygenDumpLevel.GetComponent<Renderer>().material.GetFloat("_Cutoff")) * 10;
            if(temp ==0)
            {
                temp = 1*10;
            }
            oxygenDumpLevelFloat =  0;
            alarm.SetActive(false);
            for(int i  = 0; i < temp; i++)
            {
                yield return new WaitForSeconds(0.2f);
                //0.9
                oxygenDumpLevelFloat += 0.1f;
                oxygenDumpLevel.GetComponent<Renderer>().material.SetFloat("_Cutoff", oxygenDumpLevelFloat);
            }
            //Debug.Log("Oxygen Dumped" + oxygenDumpLevelFloat*100);
            player.GetComponent<Player>().updateOxygen(oxygenDumpLevelFloat*100);
            alarm.SetActive(false);

            animation.SetActive(false);
            yield return new WaitForSeconds(10f);

            for (int i = 0; i < 100; i++)
            {
                yield return new WaitForSeconds(1f);
                oxygenDumpLevelFloat -= 0.01f;
                oxygenDumpLevel.GetComponent<Renderer>().material.SetFloat("_Cutoff", oxygenDumpLevelFloat);
            }
        }
    }

}
