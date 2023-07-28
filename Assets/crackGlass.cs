using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class crackGlass : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject WindowObject;
    public AudioSource impactSound;



    void Start()
    {
         SetAllChilderenGameObjectsToNotActive();
    }

    void SetAllChilderenGameObjectsToNotActive()
    {
        foreach (Transform child in WindowObject.transform)
        {
            child.gameObject.SetActive(false);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Astriod")
        {
            impactSound.Play();
            int rand = Random.Range(0, 7);
            WindowObject.transform.GetChild(rand).gameObject.SetActive(true);
        }
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
