using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyController : MonoBehaviour
{
    public Material morning;
    public Material evening_early;
    public Material evening_late;
    public Material night;

    private Material sky;

    //skyboxが切り替わるまでの時間
    private float second;

    //skyboxの回転角度
    private float rotation;
    //skybox回転スピード
    private float rotationSpeed = 0.01f;

    // Start is called before the first frame update
    void Start()
    {
        second = 0;
        sky = morning;
        RenderSettings.skybox = sky;
    }

    // Update is called once per frame
    void Update()
    {
        //空の回転
        rotation = Mathf.Repeat(sky.GetFloat("_Rotation") + rotationSpeed, 360f);

        sky.SetFloat("_Rotation", rotation);

        second += Time.deltaTime;

        if(second > 5f)//５秒立ったならば
        {
            //switch (sky)//現在の空に応じて次の空が決まる
            //{
            //    case morning:
            //        sky = evening_early;
            //        break;
            //    case evening_early:
            //        sky = evening_late;
            //        break;
            //    case evening_late:
            //        sky = night;
            //        break;
            //    case night:
            //        sky = morning;
            //        break;
            //    default:
            //        sky = morning;
            //        break;
            //}
            if(sky == morning)
            {
                sky = evening_early;
            }
            else if(sky == evening_early)
            {
                sky = evening_late;
            }
            else if(sky == evening_late)
            {
                sky = night;
            }
            else
            {
                sky = morning;
            }

            second = 0;
        }

        RenderSettings.skybox = sky;

    }
}
