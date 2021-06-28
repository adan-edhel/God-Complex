using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class ConnectToServer : MonoBehaviourPunCallbacks
{
    public CheckHMD HMDCheck;

    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        base.OnJoinedLobby();
        StartCoroutine(CheckVR());
    }

    public IEnumerator CheckVR()
    {
        yield return new WaitForSeconds(4);
        if (HMDCheck.vrEnabled)
        {
            SceneManager.LoadScene("Vr_Lobby");
        }
        else
        {
            SceneManager.LoadScene("Pc_Lobby");
        }
    }
}
