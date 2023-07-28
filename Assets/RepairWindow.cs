using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class RepairWindow : MonoBehaviour
{

    public GameObject animation;
    private XRGrabInteractable grabbable;

    public GameObject arrow;
    
    // Start is called before the first frame update
    void Start()
    {
        XRGrabInteractable grabbable = GetComponent<XRGrabInteractable>();
        grabbable.activated.AddListener(Repair);
        grabbable.deactivated.AddListener(StopRepair);
    }

    public void Repair(ActivateEventArgs args)
    {

        //Debug.Log(args);
        animation.SetActive(true);
    }

    public void StopRepair(DeactivateEventArgs args)
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
