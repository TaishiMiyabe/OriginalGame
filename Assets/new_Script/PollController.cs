using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PollController : MonoBehaviour
{
    private Rigidbody pollRigidbody;

    // Start is called before the first frame update
    void Start()
    {
        pollRigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(this.transform.position.y <= 1 &&(this.transform.localEulerAngles.x <= 270))
        {
            Debug.Log(this.transform.localEulerAngles.x);
            pollRigidbody.isKinematic = true;
        }
    }
}
