using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SEVolumeSlider : MonoBehaviour
{
    Slider seSlider;

    AudioManager audiomanager;

    float max = 1;
    float min = 0;

    // Start is called before the first frame update
    void Start()
    {
        seSlider = GetComponent<Slider>();

        seSlider.maxValue = max;
        seSlider.minValue = min;

        audiomanager = AudioManager.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        audiomanager.Volume = seSlider.value;
    }
}
