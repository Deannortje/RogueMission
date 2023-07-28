using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolarFlare : MonoBehaviour
{
    // Start is called before the first frame update
    public float intensity = 0;
    public GameObject flare1;
    public GameObject flare2;
    public GameObject player;
    public GameObject ship;
    public GameObject shield;
    public float shieldLevel;
    public float damage;
    public bool isSolarFlare;
    public bool isSolarFlareStopping;

    public float radiationLevel;

    public float time;

    public AudioSource geiger;

    public GameObject warningText;
    public GameObject warningNoise;

    

    void Start()
    {
         intensity = 0;
         radiationLevel = 0.1f;
        //  isSolarFlare = true;
        //  StartCoroutine(solarFlare());
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        time += Time.deltaTime;
        if (time > 1)
        {
            // if(!isSolarFlare)
            // {
            //     int rand = Random.Range(1,100);
            //     if (rand == 1)
            //     {
            //         isSolarFlare = true;
            //         StartCoroutine(solarFlare());
            //     }
            // }

            time = 0;
            shieldLevel = shield.GetComponent<DeployShields>().getPosition();
            damage = (radiationLevel*((shieldLevel)/30f));
            
            //Debug.Log("Damage: " + damage);
            player.GetComponent<Player>().updateHealth(-damage);
            //Debug.Log("Radiation Level: " + radiationLevel);
            
            if(isSolarFlare)
            {
                ship.GetComponent<ShipScript>().shipRadiationLevelUpdate(Mathf.Clamp(radiationLevel,1f,0.1f), isSolarFlareStopping);
            }
            
        }

        float randomRange = Random.Range(0.12f,11f);
        float temp = Mathf.Clamp(damage,0f,10f);

        //temp = temp/(float) (temp.ToString().Length);
      
        if (randomRange < temp)
        {   
            geiger.Play();
        }

        
    }

    

    public IEnumerator solarFlare()
    {
        StartCoroutine(WarningLable());
        // int rand = Random.Range(1,3);
        // yield return new WaitForSecondsRealtime(rand);
        isSolarFlareStopping = false;
        for(int i = 0; i < 50; i++)
        {
            radiationLevel *= 1.2f;
            //player.GetComponent<Player>.updateHealth(-radiationLevel/100);
            flare1.GetComponent<Light>().intensity += 0.1f;
            flare2.GetComponent<Light>().intensity += 0.1f;
            
            //Debug.Log("Flare 1: " + flare1.GetComponent<Light>().intensity);
            //Debug.Log("Flare 2: " + flare2.GetComponent<Light>().intensity);
            yield return new WaitForSecondsRealtime(0.5f);
        }

        yield return new WaitForSecondsRealtime(10);
        isSolarFlareStopping = true;
        
        for(int i = 0; i < 50; i++)//Down
        {
            radiationLevel /= 1.2f;
            
            flare1.GetComponent<Light>().intensity -= 0.1f;
            flare2.GetComponent<Light>().intensity -= 0.1f;
            
            //Debug.Log("Flare 1: " + flare1.GetComponent<Light>().intensity);
            //Debug.Log("Flare 2: " + flare2.GetComponent<Light>().intensity);
            yield return new WaitForSecondsRealtime(0.5f);
        }
        radiationLevel = 0.1f;
        isSolarFlare = false;
    }


    IEnumerator WarningLable()
    {
        warningNoise.SetActive(true);
        for(int i = 0; i < 6; i++)
        {
            yield return new WaitForSecondsRealtime(0.5f);
            warningText.SetActive(true);
            yield return new WaitForSecondsRealtime(1);
            warningText.SetActive(false); 
        }
       warningNoise.SetActive(false);
    }
}
    
