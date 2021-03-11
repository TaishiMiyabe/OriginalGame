using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BGMVolumeSlider : MonoBehaviour
{
    Slider bgmSlider;

    AudioManager audiomanager;

    float max = 1;
    float min = 0;

    // Start is called before the first frame update
    void Start()
    {
        bgmSlider = GetComponent<Slider>();

        bgmSlider.maxValue = max;
        bgmSlider.minValue = min;

        Initialize();
    }

    void Initialize()
    {
        audiomanager = AudioManager.Instance;
        if (audiomanager)
        {
            bgmSlider.value = audiomanager.Volume;
        }
        else
        {
            Debug.LogError("AudioManagerがアタッチされたオブジェクトがシーンに存在しません(bgmvolumeslider)");
        }
    }

    // Update is called once per frame
    void Update()
    {
        audiomanager.Volume = bgmSlider.value;
    }
}
