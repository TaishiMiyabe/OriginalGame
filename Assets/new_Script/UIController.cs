using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    private GameObject player;
    private int distance;
    private float score_display = 0;
    private float scoreDisplaySpeed = 200f;

    //ゲームオーバー時のフェードインパネルに関する部分
    private GameObject gameoverPanel;
    private float red, green, blue;
    private float alpha;

    private GameObject gameoverScoreText_1;
    private GameObject gameoverScoreText_2;
    private GameObject gameoverScoreText_3;
    private GameObject distanceText;
    private GameObject retryButton;
    private GameObject returnButton;
    private GameObject startButton;

    private float second;

    //StartCut終了時点でのプレイヤー位置を起点として距離を測る
    private float playerStartLine;
    private bool MeasuringStarted = false;

    //フェードインフェードアウトのスピード
    private float fadeSpeed = 0.002f;

    //ゲームオーバー判定
    private bool isGameOver = false;
    public bool IsGameOver
    {
        get { return this.isGameOver; }//値取得用
        private set { this.isGameOver = value; }//入力用
    }


    // Start is called before the first frame update
    void Start()
    {
        this.player = GameObject.Find("Stickman_heads_sphere");

        this.gameoverPanel = GameObject.Find("GameOverFadeInPanel");

        this.gameoverScoreText_1 = GameObject.Find("GameOverScoreText_1");
        this.gameoverScoreText_2 = GameObject.Find("GameOverScoreText_2");
        this.gameoverScoreText_3 = GameObject.Find("GameOverScoreText_3");
        this.distanceText = GameObject.Find("DistanceText");

        distanceText.SetActive(false);

        //開始ボタン
        this.startButton = GameObject.Find("StartButton");

        //ゲームオーバー時のリトライボタン
        this.retryButton = GameObject.Find("ReGameButton");
        retryButton.SetActive(false);

        this.returnButton = GameObject.Find("ToStartGamenButton");
        returnButton.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        if (StartCutFlag.isOver)
        {
            DistanceDisplay();
        }

        if (this.player.transform.position.y <= -20)
        {
            GameOverDisplay();
        }
    }

    void DistanceDisplay()
    {
        if (!MeasuringStarted)//プレイヤーの走行距離計測開始地点を一度だけ取得
        {
            playerStartLine = this.player.transform.position.z;
            MeasuringStarted = true;
        }

        distanceText.SetActive(true);

        distance = (int)Mathf.Floor(this.player.transform.position.z - playerStartLine);

        this.distanceText.GetComponent<Text>().text = $"{distance}m";
    }

    void GameOverDisplay()
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

            if (second >= 1.5)
            {
                this.gameoverScoreText_2.GetComponent<Text>().text = "DISTANCE";
            }

            if (second >= 2)
            {
                if (score_display <= distance)
                {
                    score_display += scoreDisplaySpeed * Time.deltaTime;
                }
                else
                {
                    //スコア表示が終わったらゲームオーバーフラグを立てる。

                    isGameOver = true;

                    StartCoroutine("ActivateButton");

                }
                this.gameoverScoreText_3.GetComponent<Text>().text = $"{Mathf.Floor(score_display)}pt";

            }
        }
    }

    IEnumerator ActivateButton()
    {
        yield return new WaitForSeconds(1.5f);

        retryButton.SetActive(true);
        returnButton.SetActive(true);
    }
}
