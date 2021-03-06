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

    //同時トラック生成数
    private int numTruckGenerate = 3;

    // Start is called before the first frame update
    void Start()
    {

        //最初のラインは30 ボーダー間距離は50
        borderLine = 30f;
        distanceFromOldBorder = 40f;

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
            if (StartCutFlag.isOver)
            {
                this.ObsGenerate();
            }
            borderLine += distanceFromOldBorder;
        }


    }

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
            obsGeneratePos_y = 32.5f;
            obsGeneratePos_z = borderLine + distanceFromPlayer;

            throwedShip.transform.position = new Vector3(obsGeneratePos_x, obsGeneratePos_y, obsGeneratePos_z);
        }
        if (num <= 10)
        {
            for (int i = 0; i < numTruckGenerate; i++)
            {
                GameObject throwedtruck = Instantiate(throwedTruckPrefab);

                distanceFromPlayer = 200f;

                obsGeneratePos_x = Random.Range(-8, -3);
                obsGeneratePos_y = Random.Range(4, 5);
                obsGeneratePos_z = borderLine + distanceFromPlayer + Random.Range(0, 30);
                throwedtruck.transform.position = new Vector3(obsGeneratePos_x, obsGeneratePos_y, obsGeneratePos_z);
            }
        }
    }
}
