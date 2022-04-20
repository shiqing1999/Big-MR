using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObjects : MonoBehaviourPun
{
    // Start is called before the first frame update
    void Awake()
    {
        if (photonView.IsMine && PhotonNetwork.IsConnected && PhotonNetwork.IsMasterClient)
        {
            GameObject obj1 = PhotonNetwork.Instantiate("Grabbable1", new Vector3(0.5f, 0.5f, 0.5f), Quaternion.identity);
            GameObject obj2 = PhotonNetwork.Instantiate("Grabbable2", new Vector3(-0.5f, 0.5f, 0.5f), Quaternion.identity);
            GameObject obj3 = PhotonNetwork.Instantiate("Grabbable3", new Vector3(0.5f, -0.5f, 0.5f), Quaternion.identity);
            GameObject obj4 = PhotonNetwork.Instantiate("Grabbable4", new Vector3(0.5f, 0.5f, -0.5f), Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
