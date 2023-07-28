using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update

    private float Health;
    private float MaxHealth = 100;
    private float Oxygen;
    private float MaxOxygen = 100;

    public GameObject healthBar;
    public GameObject oxygenBar;
    public GameObject ship;
    public float time;

    public GameObject endGameObject;
    public GameObject restartGameObject;
    public GameObject text;

    public bool dead = false;

    void Start()
    {
        Health = MaxHealth;
        Oxygen = MaxOxygen;

        healthBar.GetComponent<Renderer>().material.SetFloat("_Cutoff", (Health*-1f / MaxHealth)+1f);
        oxygenBar.GetComponent<Renderer>().material.SetFloat("_Cutoff", (Oxygen*-1f / MaxOxygen)+1f);
        //set all values
    }

    public void updateHealth(float amount)
    {
        Health += amount;
        if (Health > MaxHealth)
        {
            Health = MaxHealth;
        }
        else if (Health < 0)
        {
            Health = 0;
            PauseGame();
        }
        healthBar.GetComponent<Renderer>().material.SetFloat("_Cutoff", (Health*-1f / MaxHealth)+1f);
    }

    public void updateOxygen(float amount)
    {

        Oxygen += amount;
        if (Oxygen > MaxOxygen)
        {
            Oxygen = MaxOxygen;
        }
        else if (Oxygen < 0)
        {
            Oxygen = 0;
            updateHealth(-0.2f);
        }
        oxygenBar.GetComponent<Renderer>().material.SetFloat("_Cutoff", (Oxygen*-1f / MaxOxygen)+1f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        time += Time.deltaTime;
        if (time > 1)
        {
            updateOxygen((-(10.1f-ship.GetComponent<ShipScript>().shipHealth/10f))/2f);
            updateHealth(0.1f);
    
            time = 0;
            //Debug.Log("Oxygen: " + Oxygen );

            
        }
    }

    public float getOxygen()
    {
        return Oxygen;
    }

    void PauseGame()
    {
        if(!dead){
            dead = true;
            Time.timeScale = 0;
            spawnGameObject(endGameObject,restartGameObject);
            //Debug.Log("Game Over");
            //Debug.Log("You Survived /n" + (int)Time.timeSinceLevelLoad + " Seconds");
            text.SetActive(true);
            text.GetComponent<TMP_Text>().text = "You Survived \n" + (int)Time.timeSinceLevelLoad + " Seconds";
            ResumeGame();
        }
        
    }
    void ResumeGame()
    {
        Time.timeScale = 1;
    }

    void spawnGameObject(GameObject endGameObject, GameObject restartGameObject)
    {
        Instantiate(endGameObject, new Vector3(-0.200399995f,1.04170001f,0.47299999f), Quaternion.identity);
        Instantiate(restartGameObject, new Vector3(0.161400005f,1.05900002f,0.463099986f), Quaternion.identity);
        
    }
    
     
    // {
    //         Vector3 vec1 = new Vector3(-0.589999974f,-1.54307687f,-0.00472009182f);
    //         Vector3 vec2 = new Vector3(0.49000001f,-1.54307687f,-0.00472009182f);

    //         GameObject newGameObj1 = (GameObject)Instantiate(prefab, vec1, Quaternion.identity);
    //         //newGameObj1.transform.SetParent(parent.transform, false);
    //         GameObject newGameObj2 = (GameObject)Instantiate(prefab, vec2, Quaternion.identity);
    //         //newGameObj2.transform.SetParent(parent.transform, false);

    // }
}
