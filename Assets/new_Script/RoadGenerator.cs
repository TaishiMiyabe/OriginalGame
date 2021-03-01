﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadGenerator : MonoBehaviour
{

    [SerializeField] GameObject[] objList_right;
    [SerializeField] GameObject[] objList_left;

    public GameObject standardRoadPrefab;
    
    //生成された道Prefabを、生成された順にこのリストに格納していく。
    private List<GameObject> roadList = new List<GameObject>();

    private float secondsFromStart;
    //この時間経過を基準に道を落とす。
    private float seconds;

    private Vector3 stagePosition_right;
    private Vector3 stagePosition_left;
    //サイド部分に時々できるスペース幅
    private Vector3 sideSpace = new Vector3(10, 0, 0);

    //プレイヤー
    private GameObject player;

    //道生成の範囲の始まり線
    private float startLine = 0;

    //道生成の範囲の終わり線
    private float endLine = 220;

    //道生成の範囲の幅
    private float generateWidth = 220;

    //startLineからどの程度手前で新たに道生成をかけるか
    private float playerPos_fromstartLine = 150;

    // Start is called before the first frame update
    void Start()
    {
        this.player = GameObject.Find("Stickman_heads_sphere");

        //ゲームの初めにstartLineからendLineまでの間の道を生成
        RoadGenerate(startLine, endLine);
        setBuildings();
        startLine = endLine;
        endLine = endLine + generateWidth;
    }

    // Update is called once per frame
    void Update()
    {
        secondsFromStart += Time.deltaTime;
        seconds += Time.deltaTime;

        if(secondsFromStart >= 3)
        {
            if (seconds >= 1.5f) 
            {
                RoadFall();
                seconds = 0;
            }
        }

        //プレイヤーがラインから一定手前に達したら、道生成をかける。
        if(player.transform.position.z > startLine - playerPos_fromstartLine)
        {
            RoadGenerate(startLine, endLine);
            setBuildings();
            startLine = endLine;
            endLine = endLine + generateWidth;
        }
    }

    private void RoadFall()
    {
        if (roadList.Count > 0) //ArgumentOutOfRangeException対策。RoadGenerateはゲームオーバーなったら止まるけどこいつは止まらない。
        {
            var childRoadRigid = roadList[0].GetComponentsInChildren<Rigidbody>();//ここ
            foreach (var rigidbody in childRoadRigid)
            {
                rigidbody.isKinematic = false;
            }
            
        roadList.RemoveAt(0);
            
        }
    }

    private void RoadGenerate(float startLine, float endLine)
    {
        for (float i = startLine; i < endLine; i += 21)
        {
            GameObject road = Instantiate(standardRoadPrefab);
            road.transform.position = new Vector3(road.transform.position.x, road.transform.position.y, i);
            //roadListに生成された道Prefabを追加していく(後で落とすため)
            roadList.Add(road);
        }
    }

    private void setBuildings()
    {
        for (int i = 0; i < 50; i++)
        {
            //建物を作るかどうか(時々スペースを作る)
            int generateFlag = Random.Range(0, 10);

            //右側
            int number1 = Random.Range(0, objList_right.Length);
            if (generateFlag <= 8) {
                GameObject building_right = Instantiate(objList_right[number1]);

                //Prefabによって微妙に位置調整
                if (building_right.tag == "Building_Shop")
                {
                    building_right.transform.position = new Vector3(building_right.transform.position.x, building_right.transform.position.y, stagePosition_right.x);
                }
                else if (building_right.tag == "Lamp")
                {
                    building_right.transform.position = new Vector3(building_right.transform.position.x, building_right.transform.position.y, stagePosition_right.x-5.5f);
                }
                else
                {
                    building_right.transform.position = new Vector3(building_right.transform.position.x, building_right.transform.position.y, stagePosition_right.x);
                }
                stagePosition_right += building_right.GetComponent<PrefabSize>().size;
                //作られた道の長さを配置した建物の長さが超えてしまったら、その建物はなかったことにする。
                if (stagePosition_right.x > endLine)
                {
                    stagePosition_right -= building_right.GetComponent<PrefabSize>().size;
                    Destroy(building_right);
                    break;
                }
            }
            else
            {
                //何も作らないスペース
                stagePosition_right += sideSpace;
            }
        }
        for(int j = 0; j <50; j++) 
        {
            //時々建物を作らない
            int generateFlag2 = Random.Range(0, 10);

            //左側
            int number2 = Random.Range(0, objList_left.Length);
            if (generateFlag2 <= 8) 
            {
                GameObject building_left = Instantiate(objList_left[number2]);
    
                //Prefabによって微妙に位置調整
                if (building_left.tag == "Building_OfficeStepped")
                {
                    building_left.transform.position = new Vector3(building_left.transform.position.x, building_left.transform.position.y, stagePosition_left.x + 3);
                }
                else if (building_left.tag == "Lamp")
                {
                    building_left.transform.position = new Vector3(building_left.transform.position.x, building_left.transform.position.y, stagePosition_left.x -6f);
                }
                else
                {
                    building_left.transform.position = new Vector3(building_left.transform.position.x, building_left.transform.position.y, stagePosition_left.x);
                }

                stagePosition_left += building_left.GetComponent<PrefabSize>().size;
                //作られた道の長さを配置した建物の長さが超えてしまったら、その建物はなかったことにする。
                if (stagePosition_left.x > endLine)
                {
                    stagePosition_left -= building_left.GetComponent<PrefabSize>().size;
                    Destroy(building_left);
                    break;
                }
            }
            else
            {
                stagePosition_left += sideSpace;
            }
        }
    }
}
