using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using Photon.Pun;

public class Troop : XRSocketInteractor
{
    public GameObject troopPrefab = null;
    private Vector3 attachOffset = Vector3.zero;

    [SerializeField] private float _troopCost;
    [SerializeField] private XRController _rightHand;

    private bool _canCreate = true;

    protected override void Awake()
    {
        base.Awake();
        //CreateAndSelectTroop(); //////////////////////////////////////////////////////////////////////////////////////// zorgt ervoor dat er gelijk troops spawnen
        SetAttachOffset();
        _rightHand = GameObject.Find("RightHand Controller").GetComponent<XRController>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S) && GetComponentInParent<VrPlayerUI>().GetGoldAmount() >= _troopCost)
        {
            CreateAndSelectTroop();
        }

        if (GetComponentInParent<VrPlayerUI>().GetGoldAmount() >= _troopCost)
        {
            GetComponent<MeshRenderer>().enabled = true;
        }
        else
        {
            GetComponent<MeshRenderer>().enabled = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.name == "RightHand Controller")
        {
            InputDevices.GetDeviceAtXRNode(_rightHand.controllerNode).TryGetFeatureValue(CommonUsages.gripButton, out bool gripping);
            if (gripping && _canCreate)
            {
                CreateAndSelectTroop();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.name == "RightHand Controller")
        {
            _canCreate = true;
        }
    }

    protected override void OnSelectExited(XRBaseInteractable interactable)
    {
        if (GetComponentInParent<VrPlayerUI>().GetGoldAmount() >= _troopCost)
        {
            base.OnSelectExited(interactable);
            CreateAndSelectTroop();
        }
    }

    private void CreateAndSelectTroop()
    {
        TroopUnit troop = CreateTroop();
        SelectTroop(troop);
        _canCreate = false;
    }

    private TroopUnit CreateTroop()
    {
        //Check geld van de speler voordat de troop gespawned wordt
        GetComponentInParent<VrPlayerUI>().RemoveGoldAmount(_troopCost);
        //GameObject troopUnit = Instantiate(troopPrefab, transform.position - attachOffset, transform.rotation);
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
