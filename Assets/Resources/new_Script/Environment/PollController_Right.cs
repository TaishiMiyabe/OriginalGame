using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PollController_Right : MonoBehaviour
{
    private Rigidbody pollRigidbody;

    private Vector3 force;
    // Start is called before the first frame update
    void Start()
    {
        pollRigidbody = GetComponent<Rigidbody>();

        force = new Vector3(1, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        this.pollRigidbody.isKinematic = true;
    }
}
