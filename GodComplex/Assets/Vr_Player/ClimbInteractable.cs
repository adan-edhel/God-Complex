using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ClimbInteractable : XRBaseInteractable
{
    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);
        
    }

    protected override void OnSelectEntered(XRBaseInteractor interactor)
    {
        base.OnSelectEntered(interactor);

        if(interactor is XRDirectInteractor)
        VrPlayerMovement.climbingHand = interactor.GetComponent<XRController>();
    }

    protected override void OnSelectExited(XRBaseInteractor interactor)
    {
        base.OnSelectExited(interactor);

        if (interactor is XRDirectInteractor)
        {
            if (VrPlayerMovement.climbingHand && VrPlayerMovement.climbingHand.name == interactor.name)
            {
                VrPlayerMovement.climbingHand = null;
            }
        }
    }

}
