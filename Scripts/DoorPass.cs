using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorPass : MonoBehaviour
{
    public GameObject headset;
    public GameObject glass;
    [SerializeField] bool glassOn= false;
    public AudioClip out_bgm;
    public AudioClip in_bgm;
    static AudioSource audiosrc;

    
    
    // Start is called before the first frame update
    void Start()
    {
        audiosrc = GetComponent<AudioSource> ();
        audiosrc.clip = out_bgm;
        audiosrc.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "MainCamera"){
            Debug.Log("1");
            if (!glassOn)
            {
                glass.SetActive(true);
                glassOn = true;
                audiosrc.clip = in_bgm;
                audiosrc.Play();
            }
            else
            {
                glass.SetActive(false);
                glassOn = false;
                audiosrc.clip = out_bgm;
                audiosrc.Play();
            }
            
        }
    }
}
