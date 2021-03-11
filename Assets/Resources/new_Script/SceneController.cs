using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    bool isGameOver;

    UIController uiclass;

    AudioManager audiomanager;

    // Start is called before the first frame update
    void Start()
    {
        uiclass = GameObject.Find("UIController").GetComponent<UIController>();

        audiomanager = AudioManager.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        isGameOver = uiclass.IsGameOver;
    }

    public void GetRetryButton()
    {
        StartCutFlag.isOver = false;
        audiomanager.PlaySEByName("ButtonSE");
        SceneManager.LoadScene("SampleScene");
    }

    public void GetReturnButton()
    {
        StartCutFlag.isOver = false;
        titleScript.isSwitched = false;
        Debug.Log(audiomanager.Volume);
        audiomanager.PlaySEByName("ButtonSE");
        SceneManager.LoadScene("title");
    }
}
