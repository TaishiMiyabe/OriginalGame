using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    bool isGameOver;

    UIController uiclass;

    // Start is called before the first frame update
    void Start()
    {
        uiclass = GameObject.Find("UIController").GetComponent<UIController>();
    }

    // Update is called once per frame
    void Update()
    {
        isGameOver = uiclass.IsGameOver;
    }

    public void GetRetryButton()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void GetReturnButton()
    {
        SceneManager.LoadScene("title");
    }
}
