using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using Source.Input;

public class Ship : MonoBehaviour, InputActions.IPlayerActions
{
    public Vector3 velocity;
    public Quaternion qvelocity;
    public Vector3 vflags;
    public Vector3 qflags;

    [Range(0.0001f, 0.001f)]
    public float speed;

    [Range(0.001f, 0.1f)]
    public float drag;

    private void Awake(){
        vflags = new Vector3(0f,0f,0f);
    }
    
    public void sanity(){
        Debug.Log("sanity check");
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        var input = context.action.ReadValue<Vector2>();
        vflags.x = input.x;
        vflags.y = input.y;
    }

    public void OnThrust(InputAction.CallbackContext context)
    {
        var input = context.action.ReadValue<float>();
        vflags.z = input;
    }

    public void OnRoll(InputAction.CallbackContext context)
    {
        Debug.Log("Roll");
    }

    public void OnFire(InputAction.CallbackContext context)
    {
        Debug.Log("Fire");
    }

    public void OnPointer(InputAction.CallbackContext context)
    {
        Debug.Log("Pointer");
    }

    public void vdt(){
        transform.position += velocity;
        velocity += speed * (transform.right * vflags.x 
                            + transform.up * vflags.y 
                            + transform.forward * vflags.z);
        velocity *= (1-drag);
    }

    public void Update(){
        vdt();
    }
}
