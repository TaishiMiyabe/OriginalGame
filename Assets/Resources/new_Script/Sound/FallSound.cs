using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallSound : MonoBehaviour
{
    AudioManager audiomanager;

    // Start is called before the first frame update
    void Start()
    {
        audiomanager = AudioManager.Instance;

        audiomanager.PlayBGMByName_spatial("RoadFall");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
