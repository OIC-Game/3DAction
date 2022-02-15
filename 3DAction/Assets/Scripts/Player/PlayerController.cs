using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Action3D.InputProvider;

namespace Action3D.Player
{
    /// <summary>
    /// プレイヤー制御用クラス
    /// </summary>
    public class PlayerController : MonoBehaviour
    {
        private IInputProvider input;

        private Rigidbody rigidbody;

        //ステージが長くなりすぎた場合スクロールできるようにするため
        private StageManager stageManager;
        private bool isStageScroll = false;

        [SerializeField] private float fowardSpeed = 15.0f;
        [SerializeField] private float lateralSpeed = 3.0f;

        private bool isLaneChenge = false;
        private int currentLane = 0;
        private float[] laneCenterPosArray = new float[]{ frontLane, backLane };

        //テスト用レーンの座標定数
        private const float frontLane = 4.0f;
        private const float backLane = -4.0f;

        private float rayDistance = 1.2f;
        private bool isGrounded = false;
        private bool isJumped = false;

        private bool isSliding = false;
        private float slidingTime = 0;
        private const float slidingDefaultTime = 1;

        public void SetInputProvider(IInputProvider input)
        {
            this.input = input;
        }

        void Start()
        {
            rigidbody = GetComponent<Rigidbody>();
            stageManager = FindObjectOfType<StageManager>();
            SetInputProvider(new UnityInputProvider());
            Vector3 pos = transform.position;
            pos.x = laneCenterPosArray[currentLane];
            transform.position = pos;
        }

        void Update()
        {
            //スライディング処理(要CT)
            if (input.GetSliding() && slidingTime == 0)
            {
                isSliding = true;
                slidingTime = slidingDefaultTime;
            }
            if (isSliding)
            {
                slidingTime -= Time.deltaTime;
                if (slidingTime <= 0)
                {
                    slidingTime = 0;
                    isSliding = false;
                }
            }

            //接地確認
            GroundCheck();
            //ジャンプ処理、レーン変更呼び出し
            if (input.GetJump())
            {
                if (isGrounded && !isJumped)
                {
                    if (input.GetLateralAxis() == 0)
                    {
                        Jump();
                    }
                    else 
                    {
                        LaneChenge();
                    }
                }
            }
        }

        void FixedUpdate()
        {
            //移動処理
            Move();
        }

        void Move()
        {
            Vector3 moveValue = new Vector3(0, rigidbody.velocity.y, 0);

            moveValue.x += input.GetLateralAxis() * lateralSpeed;

            if (input.GetForwardMove())
            {
                if (isStageScroll)
                {
                    //未実装
                }
                else
                {
                    moveValue.z += fowardSpeed;
                    if (isSliding)
                    {
                        moveValue.z += 5;
                    }
                }
            }

            rigidbody.velocity = moveValue;

            //レーンから落ちないように補正（仮）
            if (transform.position.x < laneCenterPosArray[currentLane] - 2)
            {
                Vector3 pos = transform.position;
                pos.x = laneCenterPosArray[currentLane] - 2;
                transform.position = pos;
            }
            else if(transform.position.x > laneCenterPosArray[currentLane] + 2)
            {
                Vector3 pos = transform.position;
                pos.x = laneCenterPosArray[currentLane] + 2;
                transform.position = pos;
            }
        }

        void Jump()
        {
            rigidbody.velocity = new Vector3(rigidbody.velocity.x, 10, rigidbody.velocity.z);
        }

        void GroundCheck()
        {
            Vector3 rayPosition = transform.position + new Vector3(0.0f, 0.1f, 0.0f);
            Ray ray = new Ray(rayPosition, Vector3.down);
            isGrounded = Physics.Raycast(ray, rayDistance);
            if(isJumped && isGrounded)
            {
                isJumped = false;
            }
        }

        //レーン移動は作成途中
        void LaneChenge()
        {
            currentLane = -(int)input.GetLateralAxis();
            if (currentLane < 0)
            {
                currentLane = 0;
            }
            else if(currentLane > laneCenterPosArray.Length - 1)
            {
                currentLane = laneCenterPosArray.Length - 1;
            }
        }
    }
}