using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObsController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //方針
        //左中央右のいずれかに１つの障害物が生成されるのが上限
        //前から迫ってくるものと、上から降ってくるもの
        //上から降ってくるものに関しては、プレイヤーの進行に合わせたところに落とすようにする
        //画面から出たものはdestroy
        //衝突した時にもdestroy
        //衝突してdestroyする際には何かしらのエフェクトをつける
    }
}
