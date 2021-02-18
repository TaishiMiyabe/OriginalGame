﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObsController : MonoBehaviour
{

    public GameObject throwedShipPrefab;

    public GameObject throwedTruckPrefab;

    //プレイヤーがこのラインを越えたら障害物が生成される
    private float borderLine;
    //ボーダーラインを超えた後の次の新しいボーダーラインまでの距離
    private float distanceFromOldBorder;
    //障害物が生成されるx軸y軸z軸位置
    private float obsGeneratePos_x;
    private float obsGeneratePos_y;
    private float obsGeneratePos_z;


    //プレイヤーからどれだけ離れたところで障害物が生成されるか
    private float distanceFromPlayer;

    // Start is called before the first frame update
    void Start()
    {

        //最初のラインは30 ボーダー間距離は50
        borderLine = 30f;
        distanceFromOldBorder = 50f;

    }

    // Update is called once per frame
    void Update()
    {
        //方針
        //左中央右のいずれかに2つの障害物が生成されるのが上限
        //前から迫ってくるものと、上から降ってくるもの
        //上から降ってくるものに関しては、プレイヤーの進行に合わせたところに落とすようにする
        //画面から出たものはdestroy
        //衝突した時にもdestroy
        //衝突してdestroyする際には何かしらのエフェクトをつける

        Vector3 playerPos = GameObject.Find("Stickman_heads_sphere").transform.position;

        if (playerPos.z >= borderLine) 
        {
            this.ObsGenerate();
            borderLine += distanceFromOldBorder;
        }


    }

    //最大で同時に１つの障害物を生成
    void ObsGenerate()
    {

        int num = Random.Range(1, 11);

        //船生成
        if(num <= 10)
        {
            GameObject throwedShip = Instantiate(throwedShipPrefab);

            distanceFromPlayer = 400f;

            //x軸方向の生成位置は-11,-6,-1のいずれかにしたい。
            int offsetX = -6;
            int coefficientX = 5;
            obsGeneratePos_x = offsetX + Random.Range(-1, 2) * coefficientX;
            //y,z軸生成位置
            obsGeneratePos_y = 27.7f;
            obsGeneratePos_z = borderLine + distanceFromPlayer;

            throwedShipPrefab.transform.position = new Vector3(obsGeneratePos_x, obsGeneratePos_y, obsGeneratePos_z);
        }
        //if (3 < num && num <= 10)//これを入れない状態なら船が上手く生成されるのに、これが入ると船の生成される位置がおかしくなる。
        //{
        //    GameObject throwedTruck = Instantiate(throwedTruckPrefab);

        //    distanceFromPlayer = 200f;

        //    obsGeneratePos_x = Random.Range(-8, -3);
        //    obsGeneratePos_y = 5;
        //    obsGeneratePos_z = borderLine + distanceFromPlayer;
        //    throwedTruck.transform.position = new Vector3(obsGeneratePos_x, obsGeneratePos_y, obsGeneratePos_z);
        //}
    }
}
