using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class titleSceneController : MonoBehaviour
{
    AudioManager audiomanager;

    // Start is called before the first frame update
    void Start()
    {
        audiomanager = AudioManager.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GetReturnToStartButton()
    {
        titleScript.isSwitched = false;
        audiomanager.PlaySEByName("ButtonSE");
        SceneManager.LoadScene("title");
    }
}
