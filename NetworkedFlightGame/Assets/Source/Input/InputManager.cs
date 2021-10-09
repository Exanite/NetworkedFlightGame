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
            Debug.Log(ship);
            actions.Player.SetCallbacks(ship);
            Debug.Log("awake");
        }

        private void OnEnable()
        {
            Debug.Log("Enable");
            actions.Enable();
            ship.sanity();
        }

        private void OnDisable()
        {
            Debug.Log("Disable");
            actions.Disable();
        }
    }
}