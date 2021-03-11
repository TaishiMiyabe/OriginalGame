using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBGM : MonoBehaviour
{
    AudioManager audiomanager;

    UIController uicontroller;

    private bool isBGMstarted = false;

    // Start is called before the first frame update
    void Start()
    {
        audiomanager = AudioManager.Instance;

        uicontroller = GameObject.Find("UIController").GetComponent<UIController>();
    }

    // Update is called once per frame
    void Update()
    {
        var isgameover = uicontroller.IsGameOver;

        if (StartCutFlag.isOver && !isBGMstarted)
        {
            audiomanager.PlayBGMByName("GameBGM");
            isBGMstarted = true;
        }

        if (isgameover)
        {
            audiomanager.StopBGM();
        }
    }
}
