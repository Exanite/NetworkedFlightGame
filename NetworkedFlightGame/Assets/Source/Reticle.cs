using System.Collections.Generic;
using Source.Input;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Reticle : MonoBehaviour, InputActions.IPlayerActions
{
    private readonly Dictionary<string, float> keysPressed = new Dictionary<string, float>();
    private readonly float reticleAlpha = 0.117f;

    private void updateDict(InputAction.CallbackContext context)
    {
        var s = context.control.displayName;
        if (context.performed)
        {
            // Debug.Log(s);
            keysPressed[s] = 0.8f;
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        updateDict(context);
    }

    public void OnThrust(InputAction.CallbackContext context)
    {
        updateDict(context);
    }

    public void OnRoll(InputAction.CallbackContext context)
    {
        updateDict(context);
    }

    public void OnLook(InputAction.CallbackContext context) {}
    public void OnFire(InputAction.CallbackContext context) {}
    public void OnPointer(InputAction.CallbackContext context) {}
    public void OnExit(InputAction.CallbackContext context) {}

    public void OnReticle(InputAction.CallbackContext context)
    {
        // Y Key
        // Debug.Log("toggle reticle");
        foreach (Transform child in transform)
        {
            var on = new Color(1f, 1f, 1f, reticleAlpha);
            var off = new Color(1f, 1f, 1f, 0.0f);
            if (child.GetComponent<RawImage>())
            {
                var ri = child.GetComponent<RawImage>();
                ri.color = ri.color.a < 0.1f ? on : off;
            }

            if (child.GetComponent<Text>())
            {
                var t = child.GetComponent<Text>();
                t.color = t.color.a < 0.1f || t.name == "Y" ? on : off;
            }
        }

        updateDict(context);
    }

    public void keyHighlighter()
    {
        foreach (Transform child in transform)
        {
            if (child.GetComponent<Text>())
            {
                var t = child.GetComponent<Text>();
                var name = child.name.ToLower();
                if (keysPressed.ContainsKey(name) && t.color.a >= 0.1f)
                {
                    var a = Mathf.Max(keysPressed[name], reticleAlpha);
                    t.color = new Color(1f, 1f, 1f, a);
                    keysPressed[name] = keysPressed[name] > reticleAlpha
                        ? keysPressed[name] - Time.deltaTime * 1f
                        : reticleAlpha;
                }
            }
        }
    }

    public void Update()
    {
        keyHighlighter();
    }
}