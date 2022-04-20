using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine.UI;
using UnityEngine;

public class ControlRelativePositioningOnPC : MonoBehaviourPun
{
    public Text syncText;
    public ControlRelativePositioningOnQuest2 quest2;

    GameObject tracker1;
    GameObject worldGO;

    // Start is called before the first frame update
    void Start()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            tracker1 = GameObject.Find("Tracker1");
            worldGO = GameObject.Find("World");

            Debug.Log("Starting coroutine.");
            StartCoroutine(SyncHeadsetPositioning());
        }
    }

    string requestedHeadsetNickname = "";
    bool lastHeadsetSynced = false;
    Dictionary<string, (Vector3, Vector3)> locRotOffsetsByHeadsetNickname;
    GameObject[] leftHandAnchors;

    IEnumerator SyncHeadsetPositioning() 
    {
        Debug.Log("In coroutine with " + PhotonNetwork.PlayerList.Length + " players.");
        locRotOffsetsByHeadsetNickname = new Dictionary<string, (Vector3, Vector3)>();
        for (int headsetNumber = 1; headsetNumber < PhotonNetwork.PlayerList.Length; headsetNumber++) 
        {
            requestedHeadsetNickname = "Quest" + headsetNumber;
            syncText.text = "Press space when player " + headsetNumber + " has their controllers set up.";
            yield return new WaitUntil(() => lastHeadsetSynced);
            syncText.text = "Player " + headsetNumber + " synced.";
            yield return new WaitForSeconds(2);
            lastHeadsetSynced = false;
        }

        foreach (string headsetNickname in locRotOffsetsByHeadsetNickname.Keys) 
        {
            Debug.Log(headsetNickname + ": (" +
                locRotOffsetsByHeadsetNickname[headsetNickname].Item1.x + ",  " +
                locRotOffsetsByHeadsetNickname[headsetNickname].Item1.y + ",  " +
                locRotOffsetsByHeadsetNickname[headsetNickname].Item1.z + ")  (" +
                locRotOffsetsByHeadsetNickname[headsetNickname].Item2.x + ",  " +
                locRotOffsetsByHeadsetNickname[headsetNickname].Item2.y + ",  " +
                locRotOffsetsByHeadsetNickname[headsetNickname].Item2.z + ")");

            //quest2.photonView.RPC("SyncMe", RpcTarget.Others, headsetNickname, locRotOffsetsByHeadsetNickname[headsetNickname].Item1, locRotOffsetsByHeadsetNickname[headsetNickname].Item2);
        }

        requestedHeadsetNickname = "PC";
        syncText.text = "Press space when Vive tracker 1 set up.";
        yield return new WaitUntil(() => lastHeadsetSynced);
        syncText.text = "Vive trackers synced.";
        yield return new WaitForSeconds(2);
        lastHeadsetSynced = false;

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            Debug.Log("Pressed Space.");
            leftHandAnchors = GameObject.FindGameObjectsWithTag("LeftHandAnchor");
            foreach (GameObject go in leftHandAnchors) 
            {
                Debug.Log(go.GetComponent<PhotonView>().Owner.NickName + "  " + requestedHeadsetNickname);
                if (go.GetComponent<PhotonView>().Owner.NickName == requestedHeadsetNickname)
                {
                    quest2.photonView.RPC("SyncMe", RpcTarget.Others, requestedHeadsetNickname, go.transform.position, go.transform.eulerAngles);
                    locRotOffsetsByHeadsetNickname[requestedHeadsetNickname] = (go.transform.position, go.transform.eulerAngles);
                    lastHeadsetSynced = true;
                }
            }

            if (requestedHeadsetNickname == "PC") 
            {
                worldGO.transform.position = new Vector3(-tracker1.transform.position.x, -tracker1.transform.position.y, -tracker1.transform.position.z);
                worldGO.transform.RotateAround(Vector3.zero, Vector3.up, -tracker1.transform.eulerAngles.y);
                worldGO.transform.position += new Vector3(0.4f, 0, -0.2f);
                lastHeadsetSynced = true;
            }
        }
    }

    /*[PunRPC]
    void SpacePressed(string requestedHeadsetNickname) 
    {
        GameObject leftHandAnchor = GameObject.Find("LeftHandAnchor");
        photonView.RPC("SyncHeadset", RpcTarget.MasterClient, PhotonNetwork.NickName, leftHandAnchor.transform.position, leftHandAnchor.transform.eulerAngles);
    }

    [PunRPC]
    void SyncHeadset(string nickname, Vector3 positionOffset, Vector3 rotationOffset)
    {
        Debug.Log("Got a message back from " + nickname);
        if (nickname == requestedHeadsetNickname)
        {
            Debug.Log("Calibrating " + nickname);
            locRotOffsetsByHeadsetNickname[nickname] = (positionOffset, rotationOffset);

            lastHeadsetSynced = true;
        }
    }*/

}
