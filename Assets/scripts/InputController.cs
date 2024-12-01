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
    public bool Jump => _jump;
    public bool Slide => _slide;

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
    

    private void LateUpdate()
    {
        _jump = false;
        _slide = false;
        if (movementInput.magnitude == 0)
        {
            movementInput = Vector2.zero;
        }
    }

}

