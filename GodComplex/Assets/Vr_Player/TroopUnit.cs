using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class TroopUnit : XRGrabInteractable
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "RightHand Controller")
        {
            GetComponent<Rigidbody>().useGravity = true;
            GetComponent<StickUnitToGround>().setGround();
        }
    }

    public new void OnSelectEnter(XRBaseInteractor interactor)
    {
        base.OnSelectEntered(interactor);
    }

    public new void OnSelectExit(XRBaseInteractor interactor)
    {
        base.OnSelectExited(interactor);
    }
}
