using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spoon : MonoBehaviour
{


    public List<GameObject> dragonEggs = new List<GameObject>();
    private GameObject egg;
    public bool getIceEgg = false;
    public bool getFireEgg = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (getIceEgg || getFireEgg)
        {
            egg.transform.SetParent(this.gameObject.transform, true);
            egg.transform.localPosition = new Vector3(0.0f, 0.1f, -0.15f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!getFireEgg && !getIceEgg)
        {
            if (other.tag == "IceEgg")
            {
                egg = other.gameObject;
                getIceEgg = true;
            }
            if (other.tag == "FireEgg")
            {
                egg = other.gameObject;
                getFireEgg = true;
            }
        }
        if (getIceEgg)
        {
            if (other.tag == "IceIncubator")
            {
                egg.transform.SetParent(other.gameObject.transform, true);
                egg.transform.position = other.gameObject.transform.position + new Vector3(0.0f, 0.36f, 0.0f);
                getIceEgg = false;
                //StartCoroutine(Incubate());
            }
            
        }
        if (getFireEgg)
        {
            if (other.tag == "FireIncubator")
            {
                egg.transform.SetParent(other.gameObject.transform, true);
                egg.transform.position = other.gameObject.transform.position + new Vector3(0.0f, 0.36f, 0.0f);
                getFireEgg = false;
            }

        }

    }

    //public IEnumerator Incubate()
    //{

    //}
}
