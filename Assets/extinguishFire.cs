using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class extinguishFire : MonoBehaviour
{

    public GameObject animation;
    private XRGrabInteractable grabbable;
    
    public GameObject arrow;

    
    // Start is called before the first frame update
    void Start()
    {
        XRGrabInteractable grabbable = GetComponent<XRGrabInteractable>();
        grabbable.activated.AddListener(ExtinguishFire);
        grabbable.deactivated.AddListener(StopExtinguishFire);
    }

    public void ExtinguishFire(ActivateEventArgs args)
    {

        //Debug.Log(args);
        animation.SetActive(true);
    }

    public void StopExtinguishFire(DeactivateEventArgs args)
    {
        animation.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void showArrow(){
        StartCoroutine(RunArrow());
    }

    public IEnumerator RunArrow()
    {
        arrow.SetActive(true);
        yield return new WaitForSecondsRealtime(3);
        arrow.SetActive(false);
    }
}
