using System.Collections;
using System.Collections.Generic;
using Source.Input;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace Source 
{
    public class LocalShip : Ship, InputActions.IPlayerActions
    {

        [Header("Dependencies")]
        public BulletManager bulletManager;
        public GameObject bulletPrefab;
        private Rigidbody rb;
    
        [Header("Runtime Values")]
        public Vector3 vflags;
        public Vector3 qflags;

        [Header("Configuration")]
        [Range(1f, 100f)]
        public float thrust;
    
        float cursorLockTime = 0;
    
        private void Awake(){
            vflags = new Vector3(0f,0f,0f);
            rb = GetComponent<Rigidbody>();
            rb.maxAngularVelocity = 2f;
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            // W, A, S, D Keys
            var input = context.action.ReadValue<Vector2>();
            vflags.x = input.x;
            vflags.y = input.y;
        }

        public void OnThrust(InputAction.CallbackContext context)
        {
            // R, F Keys
            var input = context.action.ReadValue<float>();
            vflags.z = -input;
        }

        public void OnRoll(InputAction.CallbackContext context)
        {
            // Q, E Keys
            var input = context.action.ReadValue<float>();
            qflags.z = -input;
        }

        public void OnLook(InputAction.CallbackContext context){
            // Mouse Move
            var input = context.action.ReadValue<Vector2>();
            qflags.x = input.x;
            qflags.y = -input.y;
        }

        public void OnFire(InputAction.CallbackContext context)
        {
            // Left Click
            if(cursorLockTime > 2 && context.performed){ 
                Debug.Log("Fire");
                Vector3 p = transform.position + transform.forward*4;
                GameObject bullet = Instantiate(bulletPrefab, p, transform.rotation);
                Beam beamscript = bullet.GetComponent<Beam>();
                beamscript.spawnerID = 0;//gameObject.name;
                Rigidbody bulletRB = bullet.GetComponent<Rigidbody>();
                bulletRB.velocity = rb.velocity*1 + transform.forward*50;
                bullet.transform.parent = bulletManager.transform;
            }
        }

        public void OnPointer(InputAction.CallbackContext context)
        {
            Cursor.lockState = CursorLockMode.Locked;
            qflags = new Vector3(0f,0f,0f);
        }

        public void OnReticle(InputAction.CallbackContext context){}

        public void OnQUIT(InputAction.CallbackContext context)
        {
            // Escape Key
            // 
            // Debug.Log("Quitting Unity");
            // Application.Quit();
        }

        public void addImpulse(){
            Vector3 f = thrust * 100 * (transform.right * vflags.x 
                                        + transform.up * vflags.z 
                                        + transform.forward * vflags.y);
            rb.AddForce( f * Time.deltaTime );
        }

        public void addTorque(float scale, float a, Vector3 dir){
            // Vector3 t = new Vector3(qflags.x, qflags.y, 0);
            float maxTorque = 4 * scale;
            float t = a * scale;
            t = Mathf.Clamp(-maxTorque, t, maxTorque);
            rb.AddRelativeTorque( dir * t * Time.deltaTime );
        }

        public void addTorques(){
            float s = 10.0f;
            addTorque(s*1.0f, qflags.x, Vector3.up); //left right
            addTorque(s*1.0f, qflags.y, Vector3.right); //up down
            addTorque(s*4.0f, qflags.z, Vector3.forward); //roll
        }

        public void Update(){
            if(Cursor.visible){ cursorLockTime += Time.deltaTime; }
            addImpulse();
            if(cursorLockTime > 2){ addTorques(); }
        }
    }
}