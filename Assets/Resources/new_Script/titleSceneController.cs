using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class titleSceneController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GetReturnToStartButton()
    {
        titleScript.isSwitched = false;
        SceneManager.LoadScene("title");
    }
}
