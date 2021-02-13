using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadGenerator : MonoBehaviour
{

    public GameObject standardRoadPrefab;

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

        startLine = endLine;
        endLine = endLine + generateWidth;
    }

    // Update is called once per frame
    void Update()
    {
        //プレイヤーがラインから一定手前に達したら、道生成をかける。
        if(player.transform.position.z > startLine - playerPos_fromstartLine)
        {
            RoadGenerate(startLine, endLine);
            startLine = endLine;
            endLine = endLine + generateWidth;
        }
    }

    private void RoadGenerate(float startLine, float endLine)
    {
        for (float i = startLine; i < endLine; i += 21)
        {
            GameObject road = Instantiate(standardRoadPrefab);
            road.transform.position = new Vector3(road.transform.position.x, road.transform.position.y, i);
        }
    }
}
