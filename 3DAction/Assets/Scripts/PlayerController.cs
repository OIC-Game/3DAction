using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 3;
    [SerializeField] private float jumpPower = 3;
    private CharacterController _characterController;

    private Transform _transform;
    private Vector3 _moveVelocity;

    //[SerializeField] private Animator animator;


    // Start is called before the first frame update
    void Start()
    {
        _characterController = GetComponent<CharacterController>();
        _transform = transform;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(_characterController.isGrounded ? "地上にいます" : "空中にいます");

        _moveVelocity.x = Input.GetAxis("Horizontal") * moveSpeed;
        _moveVelocity.z = Input.GetAxis("Vertical") * moveSpeed;

        _transform.LookAt(_transform.position + new Vector3(_moveVelocity.x, 0, _moveVelocity.z));
        if(_characterController.isGrounded)
        {
            if(Input.GetButtonDown("Jump"))
            {
                Debug.Log("ジャンプ！");
                _moveVelocity.y = jumpPower;
            }
        }
        else
        {
            _moveVelocity.y += Physics.gravity.y * Time.deltaTime;
        }

        _characterController.Move(_moveVelocity * Time.deltaTime);

        //animator.SetFloat("MoveSpeed", new Vector3(_moveVelocity.x, 0, _moveVelocity.z).magnitude);
    }
}
