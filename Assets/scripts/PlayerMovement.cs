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
    public float slideSpeed = 2f;
    private Vector3 _lastVelocity;

    public Animator _animatorSkin;
 
    public Transform cameraTransform;

    public Animator _animator;
    private bool isRunning = false;
    void Start()
    {
        _characterController = GetComponent<CharacterController>();
        _input = GetComponent<InputController>();
        _animator = GetComponent<Animator>();
        _lastVelocity = Vector3.zero;
        _animator.SetBool("Jump", false);
        _animator.SetBool("Slide", false);
        _animatorSkin.SetBool("Vanish", false) ;

      
    }

    void Update()
    {
        

        Move();

        Vector3 horizontalSpeed = new Vector3(_lastVelocity.x, 0, _lastVelocity.z);
        
        
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
            if (ShouldSlide())
            {
                velocity.y = slideSpeed;
             
                _animator.SetBool("Slide", true);
               
            }
            if (ShouldJump())
            {
                velocity.y = jumpSpeed;
                _animator.SetBool("Jump", true);
            }

            if (ShouldVanish())
            {
                _animatorSkin.SetBool("Vanish",true);
            }
        }
        
        else
        {
            velocity.y = _lastVelocity.y + Physics.gravity.y * Time.deltaTime;
            _animator.SetBool("Jump", false);
            _animator.SetBool("Slide", false);
            _animatorSkin.SetBool("Vanish", false );
            
           
        }


        _characterController.Move(velocity * Time.deltaTime);


        _lastVelocity = velocity;

    }
    private bool ShouldSlide()
    {
        return _input.Slide;
    }

    private bool ShouldJump()
    {
        return _input.Jump && _characterController.isGrounded;
    }

    private bool ShouldVanish()
    {
        return _input.Vanish;
    }
    private float GetGravity()
    {
        return _lastVelocity.y + Physics.gravity.y * Time.deltaTime;
    }
}


