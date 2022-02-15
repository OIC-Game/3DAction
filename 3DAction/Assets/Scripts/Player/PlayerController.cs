using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Action3D.InputProvider;

namespace Action3D.Player
{
    /// <summary>
    /// �v���C���[����p�N���X
    /// </summary>
    public class PlayerController : MonoBehaviour
    {
        private IInputProvider input;

        private Rigidbody rigidbody;

        //�X�e�[�W�������Ȃ肷�����ꍇ�X�N���[���ł���悤�ɂ��邽��
        private StageManager stageManager;
        private bool isStageScroll = false;

        [SerializeField] private float fowardSpeed = 15.0f;
        [SerializeField] private float lateralSpeed = 3.0f;

        private bool isLaneChenge = false;
        private int currentLane = 0;
        private float[] laneCenterPosArray = new float[]{ frontLane, backLane };

        //�e�X�g�p���[���̍��W�萔
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
            //�X���C�f�B���O����(�vCT)
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

            //�ڒn�m�F
            GroundCheck();
            //�W�����v�����A���[���ύX�Ăяo��
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
            //�ړ�����
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
                    //������
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

            //���[�����痎���Ȃ��悤�ɕ␳�i���j
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

        //���[���ړ��͍쐬�r��
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