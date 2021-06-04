using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Troop : XRSocketInteractor
{
    public GameObject troopPrefab = null;
    private Vector3 attachOffset = Vector3.zero;

    protected override void Awake()
    {
        base.Awake();
        CreateAndSelectTroop();
        SetAttachOffset();
    }

    protected override void OnSelectExited(XRBaseInteractable interactable)
    {
        base.OnSelectExited(interactable);
        CreateAndSelectTroop();
    }

    private void CreateAndSelectTroop()
    {
        TroopUnit troop = CreateTroop();
        SelectTroop(troop);
    }

    private TroopUnit CreateTroop()
    {
        GameObject troopUnit = Instantiate(troopPrefab, transform.position - attachOffset, transform.rotation);
        return troopUnit.GetComponent<TroopUnit>();
    }

    private void SelectTroop(TroopUnit troop)
    {
        OnSelectEntered(troop);
        troop.OnSelectEnter(this);
    }

    private void SetAttachOffset()
    {
        if (selectTarget is XRGrabInteractable interactable)
            attachOffset = interactable.attachTransform.localPosition;
    }
}
