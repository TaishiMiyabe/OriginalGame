using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoleController : MonoBehaviour
{
    private CapsuleCollider CC;

    // Start is called before the first frame update
    void Start()
    {
        CC = GetComponent<CapsuleCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //void OnTriggerStay(Collider other)
    //{
    //    other.gameObject.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + 2f, this.transform.position.z);
    //    HingeJoint HJ = gameObject.AddComponent<HingeJoint>();
    //    HJ.connectedBody = other.attachedRigidbody;
    //}
}
