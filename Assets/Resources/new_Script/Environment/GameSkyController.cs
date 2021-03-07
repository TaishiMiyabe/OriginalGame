using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSkyController : MonoBehaviour
{
    public Material cloudy;

    private Material sky;

    //skyboxの回転
    private float rotation;
    private float rotationSpeed = 0.001f;

    // Start is called before the first frame update
    void Start()
    {
        sky = cloudy;

        RenderSettings.skybox = sky;
    }

    // Update is called once per frame
    void Update()
    {
        rotation = Mathf.Repeat(sky.GetFloat("_Rotation") + rotationSpeed, 360f);

        sky.SetFloat("_Rotation", rotation);
    }
}
