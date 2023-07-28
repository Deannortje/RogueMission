using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnFire : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject prefab;
    public GameObject parent;

    public bool isRunning = false;


    void spawnGameObject(GameObject prefab)
    {
       
            
            Vector3 vec1 = new Vector3(-0.589999974f,-1.54307687f,-0.00472009182f);
            Vector3 vec2 = new Vector3(0.49000001f,-1.54307687f,-0.00472009182f);
            Vector3 vec3 = new Vector3(-0.0130000003f,-2.14299989f,0.116999999f);

            GameObject newGameObj1 = (GameObject)Instantiate(prefab, vec1, Quaternion.identity);
            newGameObj1.transform.SetParent(parent.transform, false);
            GameObject newGameObj2 = (GameObject)Instantiate(prefab, vec2, Quaternion.identity);
            newGameObj2.transform.SetParent(parent.transform, false);
            GameObject newGameObj3 = (GameObject)Instantiate(prefab, vec3, Quaternion.identity);
            newGameObj3.transform.SetParent(parent.transform, false);
            
        
    }

    public IEnumerator RunTimer()
    {
        isRunning = true;
        int rand = Random.Range(2,3);
        yield return new WaitForSecondsRealtime(rand);
        spawnGameObject(prefab);

    }
}
