using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadFallSoundMove : MonoBehaviour
{
    Rigidbody rb;

    private float velocityZ = 13.3f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        this.rb.velocity = new Vector3(0, 0, velocityZ);
    }
}
