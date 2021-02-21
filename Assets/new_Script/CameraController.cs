using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private GameObject player;

    //カメラとプレイヤーの距離
    private float distance;

    //gameover時のカメラのx角度
    private float cameraRotate = 90;

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

        if(player.transform.position.y <= -1)
        {
            var target = Quaternion.Euler(new Vector3(cameraRotate, 0, 0));
            var nowRotation = this.transform.rotation;

            if(Quaternion.Angle(nowRotation, target) <= 1)
            {
                this.transform.rotation = target;
            }
            else
            {
                this.transform.Rotate(1, 0, 0);
                this.transform.position = new Vector3(this.player.transform.position.x, this.player.transform.position.y + 5, this.player.transform.position.z);
            }
        }
    }
}
