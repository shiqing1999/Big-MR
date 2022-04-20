using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableOculusOrSteamVRBasedOnPlatform : MonoBehaviour
{
    public bool isSteamVR = false;
    public bool isOculusVR = false;

    // Start is called before the first frame update
    void Start()
    {
        if (isOculusVR && Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.WindowsPlayer)
            Destroy(this.gameObject);

        if (isSteamVR && Application.platform == RuntimePlatform.Android)
            Destroy(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
