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
}