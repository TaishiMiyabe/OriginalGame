using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private GameObject player;

    //カメラとプレイヤーの距離
    private float distance;

    //カメラの初期位置
    private Vector3 startPosition;

    //最初にカメラが回転する最大角度
    private float maxRotation = 180f;

    //最初にカメラが開店する速度
    private float rotateSpeed = 0.5f;

    //gameover時のカメラのx角度
    private float cameraRotate = 90;

    //ゲームシーンにタイトルからスイッチしてからの時間
    private float secondFromSwitched = 0;
    //ゲームシーン移行後、一度半回転を終えてからの時間
    private float secondFromMaxRotated = 0;

    // Start is called before the first frame update
    void Start()
    {
        this.player = GameObject.Find("Stickman_heads_sphere");

        this.distance = player.transform.position.z - this.transform.position.z;

        startPosition = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //落下し始めの時のカメラの動き
        if (player.transform.position.y <= -1)
        {
            //Debug.Log("aaaaaa");
            var target = Quaternion.Euler(new Vector3(cameraRotate, 0, 0));
            var nowRotation = this.transform.rotation;

            if (Quaternion.Angle(nowRotation, target) <= 1)
            {
                this.transform.rotation = target;
            }
            else
            {
                this.transform.Rotate(1, 0, 0);
            }

            //落下時のカメラの動き
            if (player.transform.position.y >= -10)
            {
                this.transform.position = new Vector3(this.player.transform.position.x, this.player.transform.position.y + 5, this.player.transform.position.z);
            }
            else
            {
                this.transform.position = new Vector3(this.player.transform.position.x, this.transform.position.y, this.player.transform.position.z);
            }
        }
        else
        {
            if (titleScript.isSwitched)//ゲーム画面のカメラの動作
            {
                secondFromSwitched += Time.deltaTime; //ゲーム画面遷移後の時間を計測しておく

                //プレイヤーを中心として、スタート地点方向ベクトルと、カメラ方向ベクトルのなす角度
                float currentAngle = Vector3.Angle(new Vector3(this.player.transform.position.x - this.transform.position.x, 0, this.player.transform.position.z - this.transform.position.z), new Vector3(this.player.transform.position.x - startPosition.x, 0, this.player.transform.position.z - startPosition.z));
              
                if (secondFromSwitched < 3)//ゲーム画面遷移後3秒は通常動作
                {
                    this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.player.transform.position.z - this.distance);
                }
                else//３秒立ったらプレイヤーを中心としてカメラが１８０度回転する
                {
                    if (currentAngle < maxRotation)
                    {
                        this.transform.RotateAround(this.player.transform.position, Vector3.up, rotateSpeed);
                    }
                    else//１８０度回転したら、一定時間そのままで、その後元の位置に戻る
                    {
                        //Debug.Log("aaaaa");
                        secondFromMaxRotated += Time.deltaTime;
                        if(secondFromMaxRotated < 2)
                        {
                            this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.player.transform.position.z + this.distance);
                        }
                        else
                        {
                            if (currentAngle > 0)
                            {
                                this.transform.RotateAround(this.player.transform.position, Vector3.down, rotateSpeed);
                            }
                            else
                            {
                                titleScript.isSwitched = false;
                                StartSwitch.isStarted = true;
                            }
                        }

                    }
                }
            }
            else//title画面のカメラの動作 およびゲームスタート後のゲーム画面カメラの動作
            {
                //Debug.Log("aaaaaaa");
                //プレイヤーの走りに合わせてカメラを移動
                this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.player.transform.position.z - this.distance);
            }
        }
    }
}
