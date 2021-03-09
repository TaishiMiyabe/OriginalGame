using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionSound : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        AudioManager audiomanager = AudioManager.Instance;

        audiomanager.PlaySEByName("Explosion");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
