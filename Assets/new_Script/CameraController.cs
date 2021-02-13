using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private GameObject player;

    //カメラとプレイヤーの距離
    private float distance;

    // Start is called before the first frame update
    void Start()
    {
        this.player = GameObject.Find("Stickman_heads_sphere");

        this.distance = player.transform.position.z - this.transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        //プレイヤーの走りに合わせてカメラを移動
        this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.player.transform.position.z - this.distance);
    }
}
