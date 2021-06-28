using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class StickUnitToGround : MonoBehaviour
{
    public GameObject testobject;
    public bool placed = false;

    [SerializeField] private XRController _rightHand;

    // Start is called before the first frame update
    void Start()
    {
        _rightHand = GameObject.Find("RightHand Controller").GetComponent<XRController>();
        //GetComponent<Rigidbody>().useGravity = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        RaycastHit hit;
        Ray ray = new Ray(transform.position, Vector3.down);
        if (Physics.Raycast(ray, out hit) && !placed)
        {
            testobject.transform.position = hit.point;
        }


        if (Input.GetKey(KeyCode.Space) && !placed)
        {
            transform.position = testobject.transform.position;
            testobject.SetActive(false);
            GetComponent<Rigidbody>().useGravity = true;
            placed = true;
        }
    }

    public void setGround()
    {
        InputDevices.GetDeviceAtXRNode(_rightHand.controllerNode).TryGetFeatureValue(CommonUsages.gripButton, out bool gripping);
        if (!gripping && !placed)
        {
            transform.position = testobject.transform.position;
            testobject.SetActive(false);
            GetComponent<Rigidbody>().useGravity = true;
            GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
            GetComponent<Rigidbody>().angularVelocity = new Vector3(0, 0, 0);
            placed = true;
        }
    }
}
