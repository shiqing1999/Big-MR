using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bucket : MonoBehaviour
{
    [SerializeField]
    GameObject water;
    [SerializeField] private bool isPouring = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (this.gameObject.transform.eulerAngles.z>90.0 )
        {
            
            //isPouring = true;
            water.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "River")
        {
            water.SetActive(true);
        }
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Bowl")
        {
            Debug.Log("Pour");
            isPouring = true;
        }
    }
}
