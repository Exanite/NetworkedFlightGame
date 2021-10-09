using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using Source.Input;

public class Ship : MonoBehaviour, InputActions.IPlayerActions
{

    public Vector3 vflags;
    public Vector3 qflags;

    [Range(1f, 100f)]
    public float thrust;

    Rigidbody rb;

    float cursorLockTime = 0;
    private void Awake(){
        vflags = new Vector3(0f,0f,0f);
        rb = GetComponent<Rigidbody>();
        rb.maxAngularVelocity = 2f;
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
        vflags.z = -input;
    }

    public void OnRoll(InputAction.CallbackContext context)
    {
        var input = context.action.ReadValue<float>();
        qflags.z = -input;
    }

    public void OnLook(InputAction.CallbackContext context){
        var input = context.action.ReadValue<Vector2>();
        qflags.x = input.x;
        qflags.y = -input.y;
    }

    public void OnFire(InputAction.CallbackContext context)
    {
        Debug.Log("Fire");
    }

    public void OnPointer(InputAction.CallbackContext context)
    {
        Cursor.lockState = CursorLockMode.Locked;
        qflags = new Vector3(0f,0f,0f);
    }

    public void OnQUIT(InputAction.CallbackContext context)
    {
        Debug.Log("Quitting Unity");
        Application.Quit();
    }

    public void addImpulse(){
        Vector3 f = thrust * 100 * (transform.right * vflags.x 
                + transform.up * vflags.z 
                + transform.forward * vflags.y);
        rb.AddForce( f * Time.deltaTime );
    }

    public void addTorque(float scale, float a, Vector3 dir){
        // Vector3 t = new Vector3(qflags.x, qflags.y, 0);
        float maxTorque = 4;
        float t = a * scale;
        t = Mathf.Clamp(-maxTorque, t, maxTorque);
        rb.AddRelativeTorque( dir * t * Time.deltaTime );
    }

    public void addTorques(){
        addTorque(1.0f, qflags.x, Vector3.up); //left right
        addTorque(1.0f, qflags.y, Vector3.right); //up down
        addTorque(4.0f, qflags.z, Vector3.forward); //roll
    }

    public void Update(){
        if(Cursor.visible){ cursorLockTime += Time.deltaTime; }
        addImpulse();
        if(cursorLockTime > 2){ addTorques(); }
    }
}
