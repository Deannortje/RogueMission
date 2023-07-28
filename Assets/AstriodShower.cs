using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AstriodShower : MonoBehaviour
{
    public GameObject prefab;
    public GameObject parent;
    public float MeteoroidCount;
    // Start is called before the first frame update
    public bool isRunning;

    public GameObject warningText;
    public GameObject warningNoise;

    public float timeToWaitAfterSpawn;
    void Start()
    {
        
      //StartCoroutine(RunTimer());
    }

    void spawnGameObject(GameObject prefab)
    {
       
        float randz = Random.Range(-11.7f, -11.1f);
        float randy = Random.Range(-11.4f, -10.3f);
        Vector3 random = new Vector3(randz,randy,21f);
        GameObject go = (GameObject)Instantiate(prefab, random, Quaternion.identity);
        go.transform.SetParent(parent.transform, false);
        giveZVelocityToGameObject(go, -2);
        
    }

    public IEnumerator RunTimer()
    {
        StartCoroutine(WarningLable());
        for(int i = 0; i < MeteoroidCount; i++)
        {
            //print(Time.time);
            int rand = Random.Range(1, 5);
            yield return new WaitForSecondsRealtime(rand);
            spawnGameObject(prefab);
            //print(Time.time);
        }
        yield return new WaitForSecondsRealtime(timeToWaitAfterSpawn);
        isRunning = false;
    }

    void giveZVelocityToGameObject(GameObject go, float velocity)
    {
        Rigidbody rb = go.GetComponent<Rigidbody>();
        rb.velocity = new Vector3(0, 0, velocity);
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
