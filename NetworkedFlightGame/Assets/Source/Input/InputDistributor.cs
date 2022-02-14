using System.Collections.Generic;
using UnityEngine.InputSystem;

namespace Source.Input
{
    public class InputDistributor : InputActions.IPlayerActions
    {
        public List<InputActions.IPlayerActions> listeners;

        public InputDistributor(List<InputActions.IPlayerActions> listeners)
        {
            this.listeners = listeners;
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            foreach (var listener in listeners)
            {
                listener.OnMove(context);
            }
        }

        public void OnFire(InputAction.CallbackContext context)
        {
            foreach (var listener in listeners)
            {
                listener.OnFire(context);
            }
        }

        public void OnPointer(InputAction.CallbackContext context)
        {
            foreach (var listener in listeners)
            {
                listener.OnPointer(context);
            }
        }

        public void OnRoll(InputAction.CallbackContext context)
        {
            foreach (var listener in listeners)
            {
                listener.OnRoll(context);
            }
        }

        public void OnThrust(InputAction.CallbackContext context)
        {
            foreach (var listener in listeners)
            {
                listener.OnThrust(context);
            }
        }

        public void OnLook(InputAction.CallbackContext context)
        {
            foreach (var listener in listeners)
            {
                listener.OnLook(context);
            }
        }

        public void OnExit(InputAction.CallbackContext context)
        {
            foreach (var listener in listeners)
            {
                listener.OnExit(context);
            }
        }

        public void OnReticle(InputAction.CallbackContext context)
        {
            foreach (var listener in listeners)
            {
                listener.OnReticle(context);
            }
        }
    }
}