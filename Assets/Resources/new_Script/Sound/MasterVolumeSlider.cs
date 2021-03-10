using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MasterVolumeSlider : MonoBehaviour
{
    Slider masterSlider;

    AudioManager audiomanager;

    float max = 1;
    float min = 0;

    // Start is called before the first frame update
    void Start()
    {
        masterSlider = GetComponent<Slider>();

        masterSlider.maxValue = max;
        masterSlider.minValue = min;

        audiomanager = GameObject.Find("AudioManager").GetComponent<AudioManager>().Instance;
    }

    // Update is called once per frame
    void Update()
    {
        audiomanager.Volume = masterSlider.value;
    }
}
