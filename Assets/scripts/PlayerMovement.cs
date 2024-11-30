using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    CharacterController _characterController;
    InputController _input;
    public float Speed = 5f;
    public float jumpSpeed = 5f;
    private Vector3 _lastVelocity;

 
    public Transform cameraTransform;

    Animator _animator;
    private bool isRunning = false;
    void Start()
    {
        _characterController = GetComponent<CharacterController>();
        _input = GetComponent<InputController>();
        _animator = GetComponent<Animator>();
        _lastVelocity = Vector3.zero;
        _animator.SetBool("Jump", false);
    }

    void Update()
    {
        

        Move();

        Vector3 horizontalSpeed = new Vector3(_lastVelocity.x, 0, _lastVelocity.z);
        _animator.SetFloat("speed", horizontalSpeed.magnitude);
    }

    private void Move()
    {

        Vector3 direction = new Vector3(_input.Move.x, 0, _input.Move.y);
        //_characterController.SimpleMove(direction * Speed);

        Vector3 velocity = direction * Speed;
        velocity.x = direction.x * Speed;
        velocity.y = _lastVelocity.y;
        velocity.z = direction.z * Speed;

        velocity.y = GetGravity();

        Vector3 playerRotation = transform.rotation.eulerAngles;
        playerRotation.y = 0;
        transform.rotation = Quaternion.Euler(playerRotation);

        if (_characterController.isGrounded)
        {
            velocity.y = GetGravity(); 
            if (ShouldJump())
            {
                velocity.y = jumpSpeed;
                _animator.SetBool("Jump", true);
            }
        }
        else
        {
            velocity.y = _lastVelocity.y + Physics.gravity.y * Time.deltaTime;
            _animator.SetBool("Jump", false);
        }



        _characterController.Move(velocity * Time.deltaTime);


        _lastVelocity = velocity;


    }

    private bool ShouldJump()
    {
        return _input.Jump && _characterController.isGrounded;
    }
    private float GetGravity()
    {
        return _lastVelocity.y + Physics.gravity.y * Time.deltaTime;
    }
}


