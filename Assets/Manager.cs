using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using WireSnap;
public class Manager : MonoBehaviour
{
    //public void WireSnap;
    // Start is called before the first frame update

    //Manager class need to itterate through stages of the gameObject
    //30/60 seconds between stages
    //stage 1: slowly introduce
    //1.1 metorites and the healing process + damage and increase of oxygen loss
    //1.2 Wire tool 
    //1.3 Solar flare
    //Stage 2: When stage 2 starts - random the events but make sure they never overlap
    //Stage 3: random but 2 events can overlaps
    //Stage 4: Every event happens at once 

    // public bool MeteoroidShower;
    // public bool WirePuzzle;
    // public bool SolarFlare;

    public bool stage1;
    public bool stage2;
    public bool stage3;
    public bool stage4;

    public AstriodShower astriodShower;
    public WireManager wireManager;

    public SolarFlare solarFlare;

    public int stepCounter = 0;
  
    
    void Start()
    {
        StartCoroutine(RunTimer());
        stepCounter++;
    }
    IEnumerator RunTimer()
    {
        int rand = Random.Range(5,10);
        yield return new WaitForSecondsRealtime(rand);
        stage1 = true; 
    }

    // IEnumerator RunTimer2(bool thisStage, bool nextStage)
    // {
    
    //     yield return new WaitForSecondsRealtime(10);
    //     thisStage = false;
    //     nextStage = true;
    //     stepCounter = 1;
        
    // }

    
    void FixedUpdate()
    {
        
        if(stage1)
        {
            RunStage1();
        }
        if(stage2)
        {
            RunStage2();
        }
        if(stage3)
        {
            RunStage1();
        }
        if(stage4)
        {
            RunStage1();
        }
    }

    void RunStage1()
    {
        //Stage 1: slowly introduce
        //1.1 metorites and the healing process + damage and increase of oxygen loss
        //1.2 Wire tool 
        //1.3 Solar flare
        Debug.Log("Stage 1");
        if(!astriodShower.isRunning  && stepCounter == 1)
        {
            MeteorEvent();
            stepCounter++;
        }
        if(stepCounter == 2 && !wireManager.isRunning && !astriodShower.isRunning)
        {
            WireEvent();
            stepCounter++;
        }
        if(stepCounter == 3 && !solarFlare.isSolarFlare && !astriodShower.isRunning && !wireManager.isRunning)
        {
            SolarEvent();
            stepCounter++;
        }
        if(stepCounter == 4  && !astriodShower.isRunning && !wireManager.isRunning && !solarFlare.isSolarFlare)
        {
            
            stage1= false;
            stage2 = true;
            stepCounter = 1;
            
        }
       
        
    }

    void RunStage2()
    {
         
        Debug.Log("Stage 2");
        if(!astriodShower.isRunning  && stepCounter == 1)
        {
            
            MeteorEvent();
            stepCounter++;
        }
        if(stepCounter == 2 && !wireManager.isRunning && !astriodShower.isRunning)
        {
            SolarEvent();
            WireEvent();
            stepCounter++;
        }
        if(stepCounter == 3 && !solarFlare.isSolarFlare && !astriodShower.isRunning && !wireManager.isRunning)
        {
            SolarEvent();
            stepCounter++;
        }
        if(stepCounter == 4  && !astriodShower.isRunning && !wireManager.isRunning && !solarFlare.isSolarFlare)
        {
            stage2= false;
            stage1 = true;
            stepCounter = 1;
            
        }
       
        
    }

    
    void MeteorEvent()
    {
        astriodShower.isRunning = true;
        astriodShower.StartCoroutine(astriodShower.RunTimer());
        Debug.Log("Meteor Event Started");
    }
    
    void WireEvent()
    {
        wireManager.isRunning = true;
        wireManager.StartCoroutine(wireManager.RunTimer());
        Debug.Log("Wire Event Started");
    }

    void SolarEvent()
    {
        solarFlare.isSolarFlare = true;
        solarFlare.StartCoroutine(solarFlare.solarFlare());
        Debug.Log("Solar Event Started");
    }

}
