using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SpawnPlayers : MonoBehaviour
{
    public GameObject PC_PLayerPrefab;
    public GameObject VR_PLayerPrefab;
    public GameObject VR_PLayerPrefabToSync;      ////////// kijk Valem video, zodat de handen etc gesynced kunnen worden. Verder moet de vr speler lokaal dubbel gespawned worden

    public CheckHMD checkHMD;

    public float minX;
    public float maxX;
    public float minY;
    public float maxY;
    public float minZ;
    public float maxZ;

    private void Start()
    {
        StartCoroutine(CheckVR());
    }

    public IEnumerator CheckVR()
    {
        Vector3 randomPosition = new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY), Random.Range(minZ, maxZ));
        yield return new WaitForSeconds(2);
        if (checkHMD.vrEnabled)
        {
            PhotonNetwork.Instantiate(VR_PLayerPrefab.name, randomPosition, Quaternion.identity);
        }
        else
        {
            PhotonNetwork.Instantiate(PC_PLayerPrefab.name, randomPosition, Quaternion.identity);
        }
    }
}
