using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class ControlRelativePositioningOnQuest2 : MonoBehaviourPun
{
    public Transform leftHandAnchor;
    GameObject worldGO;

    Vector3 centerOfPlayArea = Vector3.zero;

    private void Start()
    {
        worldGO = GameObject.Find("World");
        //worldGO.transform.position = OVRManager.boundary.GetGeometry();

        ResetOffsetBasedOnPlayArea();
    }

    private void Update()
    {
        Debug.Log("WORLD - " + transform.position);
        Debug.Log("HAND - " + leftHandAnchor.position);

        float triggerPull = OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger);
        if (triggerPull != 0) 
        {
            ResetOffsetBasedOnPlayArea();
        }
    }

    [PunRPC]
    void SyncMe(string myName, Vector3 leftControllerAnchorPosition, Vector3 leftControllerAnchorEulerAngles) 
    {
        if (PhotonNetwork.NickName == myName)
        {
            this.leftControllerAnchorPosition = leftControllerAnchorPosition;
            this.leftControllerAnchorEulerAngles = leftControllerAnchorEulerAngles;
            StartCoroutine(UpdatePosition());
        }
    }

    Vector3 leftControllerAnchorPosition;
    Vector3 leftControllerAnchorEulerAngles;

    IEnumerator UpdatePosition()
    {
        yield return null;
        worldGO.transform.position = new Vector3(-leftControllerAnchorPosition.x, -leftControllerAnchorPosition.y, -leftControllerAnchorPosition.z);
        yield return null;
        worldGO.transform.RotateAround(Vector3.zero, Vector3.up, -leftControllerAnchorEulerAngles.y);
        yield return null;
    }

    public void ResetOffsetBasedOnPlayArea() 
    {
        Vector3 averagePoint = Vector3.zero;
        int vectorsAveraged = 0;
        var geometry = OVRManager.boundary.GetGeometry(OVRBoundary.BoundaryType.PlayArea);
        for (int i = 0; i < geometry.Length; i++)
        {
            averagePoint = (averagePoint * vectorsAveraged + geometry[i]) / (vectorsAveraged + 1);
            vectorsAveraged++;
        }
        Vector3 newCenterOfPlayArea = averagePoint;

        if (newCenterOfPlayArea != centerOfPlayArea) 
        {
            Vector3 difference = newCenterOfPlayArea - centerOfPlayArea;
            centerOfPlayArea = newCenterOfPlayArea;
            worldGO.transform.position += difference;
        }



    }
    
}
