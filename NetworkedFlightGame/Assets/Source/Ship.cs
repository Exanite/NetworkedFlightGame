using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Source.Input;

public class Ship : MonoBehaviour, InputActions.IPlayerActions
{
    public Vector3 velocity;

    private void Awake(){
        Debug.Log("Awake");
    }
    
    public void sanity(){
        Debug.Log("sanity check");
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        var input = context.action.ReadValue<Vector2>();
        Debug.Log(input);

        // velocity = transform.right * input.x + transform.forward * input.y;
    }

    public void OnFire(InputAction.CallbackContext context)
    {
        Debug.Log("Fire");
    }

    public void OnPointer(InputAction.CallbackContext context)
    {
        Debug.Log("Pointer");
    }
}
