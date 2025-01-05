using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputController : MonoBehaviour
{
    Vector2 movementInput;

    public Vector2 Move => movementInput;
    private bool _jump;
    private bool _slide;
    private bool _vanish;
    private bool _IsVanish;
    public bool Jump => _jump;
    public bool Slide => _slide;
    public bool Vanish => _vanish;
    public bool IsVanish => _IsVanish;
    private bool _canVanish;
    public bool CanVanish => _canVanish;

    

    private void OnMove(InputValue value)
    {
        Debug.Log("hola");
        movementInput = value.Get<Vector2>();


    }
    private void OnJump()
    {
        _jump = true;
    }
    private void OnSlide()
    {
        _slide = true;
    }
    private void OnVanish()
    {
       
        if (_canVanish)
        {
            _vanish = !_vanish; 
            
        }
        else
        {
            Debug.Log("Player cannot vanish yet.");
        }
        _canVanish = false;
    }

    private void OnTriggerEnter(Collider other)
    {
     
        if (other.CompareTag("Chip"))
        {
            _canVanish = true;
            Debug.Log("Player can now vanish.");
        }
    }
    private void LateUpdate()
    {
        _jump = false;
        _slide = false;
        _vanish = false;
        if (movementInput.magnitude == 0)
        {
            movementInput = Vector2.zero;
        }
    }

}






