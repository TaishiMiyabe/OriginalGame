using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class titleScript : MonoBehaviour
{
    public static bool isSwitched = false;

    AudioManager audiomanager;

    // Start is called before the first frame update
    void Start()
    {
        //DontDestroyOnLoad(this);

        audiomanager = AudioManager.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void GetStartButton()
    {
        isSwitched = true;
        audiomanager.PlaySEByName("ButtonSE");
        SceneManager.LoadScene("SampleScene");
    }

    public void GetConfigButton()
    {
        audiomanager.PlaySEByName("ButtonSE");
        SceneManager.LoadScene("Config");
    }
}
