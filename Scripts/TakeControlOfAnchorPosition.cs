using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

[RequireComponent(typeof(PhotonView))]
public class TakeControlOfAnchorPosition : MonoBehaviourPun
{
    public bool masterControlling = false;
    GameObject worldGO;

    // Start is called before the first frame update
    void Start()
    {
        worldGO = GameObject.Find("World");   
    }

    // Update is called once per frame
    void Update()
    {
        if (masterControlling) 
        {
            Debug.Log("Updating " + photonView.Owner.NickName + "'s position to " + transform.position);
            photonView.RPC("UpdatePosition", RpcTarget.Others, photonView.Owner.NickName, transform.position);
        }
    }

    [PunRPC]
    void UpdatePosition(string ownerHeadset, Vector3 newPosition) 
    {
        if (photonView.Owner.NickName == ownerHeadset)
        {
            Vector3 positionDiff = newPosition - transform.position;
            worldGO.transform.position += positionDiff;
        }
    }
}
