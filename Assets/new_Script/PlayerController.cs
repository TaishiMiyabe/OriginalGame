using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Animator playerAnimator;

    private Rigidbody playerRigidbody;

    //プレイヤーの速度(通常)
    private float velocityZ_normal = 16f;
    private float velocityX = 0;

    //プレイヤーへの追加速度(横方向)
    private float velocityX_move = 20f;

    //プレイヤーの左右方向位置限界
    private float movablePos_left = -12f;
    private float movablePos_right = 0;

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
    }

    // Update is called once per frame
    void Update()
    {
        //横移動が入力された際の速度
        float sideVelocityX = 0;

        if(Input.touchCount > 0)//タッチされているならば
        {
            Touch touch = Input.GetTouch(0);

            //どういうフリックがされたかを設定
            Flick(touch);

            //フリックに応じたアクション
            switch (flickDirection)
            {
                case "right":
                    if(this.transform.position.x < movablePos_right)
                    {
                        velocityX = velocityX_move;
                    }
                    break;

                case "left":
                    if(this.transform.position.x > movablePos_left)
                    {
                        velocityX = - velocityX_move;
                    }
                    break;
            }
        }

        //通常時のプレイヤーの速度を与える
        this.playerRigidbody.velocity = new Vector3(velocityX, 0, this.velocityZ_normal);
    }


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
