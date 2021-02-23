﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Animator playerAnimator;

    private Rigidbody playerRigidbody;

    private float secondsFromCollided;

    //プレイヤーの速度(通常)
    private float velocityZ_normal = 16f;
    private float velocityX = 0;
    private float velocityY;

    //プレイヤーの速度(スロー)
    private float velocityZ_slow = 5f;
    //オブジェクトにぶつかったかどうか
    private bool isCollided = false;
    //接地しているかどうか
    private bool isGrounded;

    //プレイヤーへの追加速度(横方向)
    private float velocityX_move = 30f;
    //左右へ移動するかどうかの判定用
    private bool goLeft = false;
    private bool goRight = false;

    //プレイヤーの左右方向位置限界
    private float movablePos_left = -11f;
    private float movablePos_right = -1f;
    private float XPos_start = -6f;

    //プレイヤーの位置判定用
    private string CENTER = "centerPos";
    private string RIGHT = "rightPos";
    private string LEFT = "leftPos";
    //プレイヤーの初期位置
    private string playerPos;

    //スクリーンタッチ時の、タッチ開始ポジションと終了ポジション
    private Vector3 touchStartPos;
    private Vector3 touchEndPos;
    //フリック方向
    private string flickDirection;


    // Start is called before the first frame update
    void Start()
    {
        this.playerAnimator = GetComponent<Animator>();

        this.playerRigidbody = GetComponent<Rigidbody>();

        playerPos = CENTER;
    }

    // Update is called once per frame
    void Update()
    {
        //何も操作されていないときは0
        velocityX = 0;

        if (Input.touchCount > 0)//タッチされているならば
        {
            Touch touch = Input.GetTouch(0);

            //どういうフリックがされたかを設定
            Flick(touch);

            //フリックに応じたアクション
            switch (flickDirection)
            {
                case "right":
                    if(playerPos != RIGHT && !goLeft)
                    {
                        goRight = true;
                    }
                    break;

                case "left":
                    if(playerPos != LEFT && !goRight)
                    {
                        goLeft = true;
                    }
                    break;
            }
        }
        #region pcから操作できるようにするための部分
        if (Input.GetKey(KeyCode.LeftArrow) && playerPos != LEFT && !goRight)
        {
            goLeft = true;
        }
        if (Input.GetKey(KeyCode.RightArrow) && playerPos != RIGHT && !goLeft)
        {
            goRight = true;
        }
        #endregion



        //右方向に移動している＆左側から移動している＆真ん中に達した場合
        if (goRight && (playerPos == LEFT) && (this.transform.position.x >= XPos_start))
        {
            goRight = false;
            playerPos = CENTER;

            //位置調整？
            //this.transform.position.x = XPos_start;←エラー
            //Vector3 tmpPos = this.transform.position;
            //this.transform.position = new Vector3(XPos_start, tmpPos.y, tmpPos.z);
            this.transform.position = new Vector3(XPos_start, this.transform.position.y, this.transform.position.z);
        }
        //右方向に移動している＆真ん中から移動している＆右側に達した場合
        if (goRight && (playerPos == CENTER) && (this.transform.position.x >= movablePos_right))
        {
            goRight = false;
            playerPos = RIGHT;

            //位置調整？
            //Vector3 tmpPos = this.transform.position;
            //this.transform.position = new Vector3(movablePos_right, tmpPos.y, tmpPos.z);
            this.transform.position = new Vector3(movablePos_right, this.transform.position.y, this.transform.position.z);
        }
        //左方向に移動している＆右側から移動している＆真ん中に達した場合
        if (goLeft && (playerPos == RIGHT) && (this.transform.position.x <= XPos_start))
        {
            goLeft = false;
            playerPos = CENTER;

            //位置調整？
            //Vector3 tmpPos = this.transform.position;
            //this.transform.position = new Vector3(XPos_start, tmpPos.y, tmpPos.z);
            this.transform.position = new Vector3(XPos_start, this.transform.position.y, this.transform.position.z);
        }
        //左方向に移動している＆真ん中から移動している＆左側に達した場合
        if (goLeft && (playerPos == CENTER) && (this.transform.position.x <= movablePos_left))
        {
            goLeft = false;
            playerPos = LEFT;

            //位置調整？
            //Vector3 tmpPos = this.transform.position;
            //this.transform.position = new Vector3(movablePos_left, tmpPos.y, tmpPos.z);
            this.transform.position = new Vector3(movablePos_left, this.transform.position.y, this.transform.position.z);
        }

    }


    void FixedUpdate()
    {
        //地面との接地判定
        isGrounded = CheckGrounded();
        if (isGrounded)
        {
            velocityY = 0;
        }
        else
        {
            velocityY = -5;
        }

        //物理演算部分だけをここに記述。
        if (goRight)//goRight = trueなら、右方向に速度を与える
        {
            velocityX = velocityX_move;
        }

        if (goLeft)//goLeft = trueなら、左方向に速度を与える。
        {
            velocityX = -velocityX_move;
        }
        //通常時のプレイヤーの速度を与える
        this.playerRigidbody.velocity = new Vector3(velocityX, velocityY, this.velocityZ_normal);

        if (isCollided)
        {
            secondsFromCollided += Time.deltaTime;
            this.playerRigidbody.velocity = new Vector3(velocityX, velocityY, this.velocityZ_slow);

        }

        if(secondsFromCollided >= 2)
        {
            isCollided = false;
            secondsFromCollided = 0;
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag !="Road")
        {
            isCollided = true; 
        }
    }

    //地面と接地しているかの判定
    bool CheckGrounded()
    {
        var controller = this.GetComponent<CharacterController>();
        //isGroundedならtrueだが、判定が厳しすぎるので、これは十分条件
        if (controller.isGrounded) { return true; }
        
        //放つ光線の初期位置と姿勢
        var ray = new Ray(this.transform.position + Vector3.up * 0.1f, Vector3.down);
        //探索距離
        var tolerance = 0.3f;

        return Physics.Raycast(ray, tolerance);
    }

    //フリックの開始点と終点を取得して、後のメソッドに渡す
    void Flick(Touch touch)
    {
        if(touch.phase == TouchPhase.Began)
        {
            touchStartPos = touch.position;
        }
        if(touch.phase == TouchPhase.Ended)
        {
            touchEndPos = touch.position;
        }

        GetFlickDirection(touchStartPos, touchEndPos);
    }

    //フリックされている方向を決める為のメソッド
    void GetFlickDirection(Vector3  touchStartPos, Vector3 touchEndPos)
    {
        float flickDirection_x = touchEndPos.x - touchStartPos.x;
        float flickDirection_y = touchEndPos.y - touchStartPos.y;

        //フリックされたと認識される最低移動長
        float flickMinDistance = 30f;

        if(Mathf.Abs(flickDirection_x) > Mathf.Abs(flickDirection_y))//x方向へのフリックのほうが長いとき
        {
            if(flickMinDistance < flickDirection_x)
            {
                flickDirection = "right";
            }
            else if(-flickMinDistance > flickDirection_x)
            {
                flickDirection = "left";
            }
        }
        else if(Mathf.Abs(flickDirection_x) < Mathf.Abs(flickDirection_y))//y方向へのフリックのほうが長いとき
        {
            if(flickMinDistance < flickDirection_y)
            {
                flickDirection = "up";
            }
            else if(-flickMinDistance> flickDirection_y)
            {
                flickDirection = "down";
            }
        }
        else
        {
            //設定した長さ以上のフリックがなされていなかった場合にはタッチという判定
            flickDirection = "touch";
        }
    }

}
