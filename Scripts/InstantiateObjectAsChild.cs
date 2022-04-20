using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

[RequireComponent(typeof(PhotonView))]
public class InstantiateObjectAsChild : MonoBehaviourPun
{
    public string prefabName;

    // Start is called before the first frame update
    void Start()
    {
        GameObject instantiatedObject = PhotonNetwork.Instantiate(prefabName, transform.position, transform.rotation);
        instantiatedObject.transform.parent = transform;
    }

}
