using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PC_Player : MonoBehaviour
{

    PhotonView view;

    // Start is called before the first frame update
    void Start()
    {
        view = GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {
        if (view.IsMine)
        {
            float x = Input.GetAxisRaw("Horizontal");
            float z = Input.GetAxisRaw("Vertical");
            gameObject.transform.Translate(new Vector3(x, 0, z) * 5 * Time.deltaTime);
        }
    }
}
