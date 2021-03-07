using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingDestroyer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(this.transform.position.y <= -20)
        {
            Destroy(this.gameObject);
        }
    }
}
