using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarImpactSound : MonoBehaviour
{
    AudioManager audiomanager;

    // Start is called before the first frame update
    void Start()
    {
        audiomanager = AudioManager.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "Road")
        {
            audiomanager.PlaySEByName("MetalImpact");
        }
    }
}
