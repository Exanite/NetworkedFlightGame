using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Source.Input
{
    public class InputManager : MonoBehaviour
    {
        public LocalShip localShip;
        public Reticle reticle;
        
        private InputActions actions;

        private void Awake()
        {
            actions = new InputActions();
            // actions.Player.SetCallbacks(ship);
            actions.Player.SetCallbacks(
                new InputDistributor(
                    new List<InputActions.IPlayerActions>() { 
                        localShip, reticle
                }));
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