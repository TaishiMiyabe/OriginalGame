using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    private GameObject player;

    //ゲームオーバー時のフェードインパネルに関する部分
    private GameObject gameoverPanel;
    private float red, green, blue;
    private float alpha;

    private GameObject gameoverScoreText_1;
    private GameObject gameoverScoreText_2;
    private float second;

    //フェードインフェードアウトのスピード
    private float fadeSpeed = 0.002f;


    // Start is called before the first frame update
    void Start()
    {
        this.player = GameObject.Find("Stickman_heads_sphere");

        this.gameoverPanel = GameObject.Find("GameOverFadeInPanel");

        this.gameoverScoreText_1 = GameObject.Find("GameOverScoreText_1");
        this.gameoverScoreText_2 = GameObject.Find("GameOverScoreText_2");




    }

    // Update is called once per frame
    void Update()
    {
        if (this.player.transform.position.y <= -20)
        {
            gameoverPanel.GetComponent<Image>().color = new Color(red, green, blue, alpha);
            alpha += fadeSpeed;

            if (alpha >= 1)
            {
                second += Time.deltaTime;

                if (second >= 1)
                {
                    this.gameoverScoreText_1.GetComponent<Text>().text = "GAME OVER";
                }

                if (second >= 2)
                {
                    this.gameoverScoreText_2.GetComponent<Text>().text = "DISTANCE";
                }
            }
        }

    }
}
