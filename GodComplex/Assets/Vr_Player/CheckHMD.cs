using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Management;
using UnityEngine.SceneManagement;

public class CheckHMD : MonoBehaviour
{

    private void Start()
    {
        var inputDevices = new List<UnityEngine.XR.InputDevice>();
        UnityEngine.XR.InputDevices.GetDevices(inputDevices);

        int count = 0;

        /*foreach (var device in inputDevices)
        {
            count += 1;
            Debug.Log(string.Format("Device found with name '{0}' and role '{1}'", device.name, device.role.ToString()));
        }

        if(count <= 0)
        {
            Debug.Log("Geen VR");
        }
        else
        {
            Debug.Log("VR");
        }*/

        List<XRDisplaySubsystem> displaySubsystems = new List<XRDisplaySubsystem>();
        XRGeneralSettings.Instance.Manager.InitializeLoader();
        XRGeneralSettings.Instance.Manager.StartSubsystems();
        SubsystemManager.GetInstances<XRDisplaySubsystem>(displaySubsystems);
        foreach (var subsystem in displaySubsystems)
        {
            count += 1;
            Debug.Log("VR");
        }

        if (count <= 0)
        {
            XRGeneralSettings.Instance.Manager.StopSubsystems();
            XRGeneralSettings.Instance.Manager.DeinitializeLoader();
            Debug.Log("Geen VR");
            SceneManager.LoadScene("Pc_Player");
        }
        else
        {
            SceneManager.LoadScene("Vr_Player");
        }
    }
}