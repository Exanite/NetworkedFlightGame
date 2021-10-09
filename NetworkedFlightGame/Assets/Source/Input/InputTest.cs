using UnityEngine;
using UnityEngine.InputSystem;

namespace Source.Input
{
    public class InputTest : MonoBehaviour, InputActions.IPlayerActions
    {
        private InputActions actions;

        private void Awake()
        {
            actions = new InputActions();
            actions.Player.SetCallbacks(this);
        }

        private void OnEnable()
        {
            actions.Enable();
        }

        private void OnDisable()
        {
            actions.Disable();
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            Debug.Log(context.action.ReadValue<Vector2>());
        }

        public void OnFire(InputAction.CallbackContext context)
        {
            Debug.Log("Fired");
        }

        public void OnPointer(InputAction.CallbackContext context) { }
    }
}