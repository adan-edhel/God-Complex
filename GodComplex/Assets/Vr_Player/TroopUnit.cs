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

    public new void OnSelectEnter(XRBaseInteractor interactor)
    {
        base.OnSelectEntered(interactor);
    }

    public new void OnSelectExit(XRBaseInteractor interactor)
    {
        base.OnSelectExited(interactor);
    }
}
