using Source.Client;
using Source.Input;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Source
{
    public class LocalShip : Ship, InputActions.IPlayerActions
    {
        [Header("Dependencies")]
        public Transform cameraTransform;
        public ClientProjectileManager projectileManager;
        public Projectile projectilePrefab;
        private Rigidbody rb;

        [Header("Runtime Values")]
        public Vector3 vflags;
        public Vector3 qflags;

        [Header("Configuration")]
        [Range(1f, 100f)]
        public float thrust;

        private float cursorLockTime;

        private int projectilePrefabId;

        private void Start()
        {
            rb = GetComponent<Rigidbody>();
            rb.maxAngularVelocity = 2f;

            projectilePrefabId = projectileManager.projectileRegistry.GetId(projectilePrefab);
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

        public void OnLook(InputAction.CallbackContext context)
        {
            // Mouse Move
            var input = context.action.ReadValue<Vector2>();
            qflags.x = input.x;
            qflags.y = -input.y;
        }

        public void OnFire(InputAction.CallbackContext context)
        {
            // Left Click
            if (cursorLockTime > 1 && context.performed)
            {
                Debug.Log("Fire");

                const int targetDistance = 1000;
                var targetPosition = cameraTransform.position + cameraTransform.forward * targetDistance;

                if (Physics.Raycast(cameraTransform.position, cameraTransform.forward, out var hit, targetDistance))
                {
                    targetPosition = hit.point;
                }

                var spawnPosition = transform.position + transform.forward * 4;
                var direction = (targetPosition - spawnPosition).normalized;

                var shipLocalVelocity = transform.TransformVector(rb.velocity);
                var projectileVelocity = direction * (200 + Mathf.Abs(shipLocalVelocity.z));

                projectileManager.CreateProjectile(projectilePrefabId, networkId, spawnPosition, transform.rotation, projectileVelocity);
            }
        }

        public void OnPointer(InputAction.CallbackContext context)
        {
            Cursor.lockState = CursorLockMode.Locked;
            qflags = new Vector3(0f, 0f, 0f);
        }

        public void OnReticle(InputAction.CallbackContext context) {}

        public void OnExit(InputAction.CallbackContext context) {}

        public void AddImpulse()
        {
            var f = thrust * 100 * (transform.right * vflags.x
                                    + transform.up * vflags.z
                                    + transform.forward * vflags.y);
            rb.AddForce(f * Time.deltaTime);
        }

        public void AddTorque(float scale, float a, Vector3 dir)
        {
            // Vector3 t = new Vector3(qflags.x, qflags.y, 0);
            var maxTorque = 4 * scale;
            var t = a * scale;
            t = Mathf.Clamp(-maxTorque, t, maxTorque);
            rb.AddRelativeTorque(dir * t * Time.deltaTime);
        }

        public void AddTorques()
        {
            var s = 10.0f;
            AddTorque(s * 1.0f, qflags.x, Vector3.up); //left right
            AddTorque(s * 1.0f, qflags.y, Vector3.right); //up down
            AddTorque(s * 16.0f, qflags.z, Vector3.forward); //roll
        }

        public void Update()
        {
            if (Cursor.visible)
            {
                cursorLockTime += Time.deltaTime;
            }

            AddImpulse();
            if (cursorLockTime > 2)
            {
                AddTorques();
            }
        }
    }
}