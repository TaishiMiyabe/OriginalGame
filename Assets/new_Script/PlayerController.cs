using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Animator playerAnimator;

    private Rigidbody playerRigidbody;

    //プレイヤーの速度(通常)
    private float velocityZ_normal = 16f; 

    // Start is called before the first frame update
    void Start()
    {
        this.playerAnimator = GetComponent<Animator>();

        this.playerRigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //通常時のプレイヤーの速度を与える
        this.playerRigidbody.velocity = new Vector3(0, 0, this.velocityZ_normal);
    }
}
