using UnityEngine;
using UnityEngine.InputSystem;

namespace Source.Input
{
    public class InputManager : MonoBehaviour
    {
        public Ship ship;
        
        private InputActions actions;

        private void Awake()
        {
            actions = new InputActions();
            actions.Player.SetCallbacks(ship);

            
        }

        private void OnEnable()
        {
            actions.Enable();
        }

        private void OnDisable()
        {
            actions.Disable();
        }
    }

    public class Ship : MonoBehaviour, InputActions.IPlayerActions
    {
        public Vector3 velocity;
        
        public void OnMove(InputAction.CallbackContext context)
        {
            var input = context.action.ReadValue<Vector2>();

            velocity = transform.right * input.x + transform.forward * input.y;
        }

        public void OnFire(InputAction.CallbackContext context)
        {
            throw new System.NotImplementedException();
        }

        public void OnPointer(InputAction.CallbackContext context)
        {
            throw new System.NotImplementedException();
        }
    }
}