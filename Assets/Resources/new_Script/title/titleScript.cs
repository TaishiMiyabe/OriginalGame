using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class titleScript : MonoBehaviour
{
    public static bool isSwitched = false;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void GetStartButton()
    {
        isSwitched = true;
        SceneManager.LoadScene("SampleScene");
    }
}
