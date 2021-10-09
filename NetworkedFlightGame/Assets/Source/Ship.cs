using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Ship : MonoBehaviour
{
    // https://www.youtube.com/watch?v=m5WsmlEOFiA&ab_channel=samyam
    private PlayerInput playerInput;
    private InputAction moveAction;

    private void Awake(){
        playerInput = GetComponent<PlayerInput>();
        moveAction = playerInput.actions["Movement"];
        Vector2 m = moveAction.ReadValue<Vector2>();
    }
    // private PlayerActions playerActions;
    // // private InputAction movement;
    // private void Awake(){
    //     playerActions = new PlayerActions();
    // }

    // private void OnEnable(){
    //     playerActions.Enable();
    //     // movement = playerActions.Player.Movement;
    //     // movement.Enable();
    // }

    // private void OnDisable(){
    //     playerActions.Disable();
    //     // movement = playerActions.Player.Movement;
    //     // movement.Enable();
    // }

    // public void OnMove(InputAction.CallbackContext context){
    //     input = context.ReadValue<Vector2>();
    //     Debug.Log(input)
    // }

    // // Start is called before the first frame update
    // void Start()
    // {
        
    // }

    // // public void OnMovement(InputAction.CallbackContext value){
    // //     Vector 2 inputMovement = value.ReadValue<Vector2>();
    // //     rawInputMovement = new Vector3(inputMovement.x, 0, inputMovement.y);
    // // }

    // // Update is called once per frame
    void Update()
    {
        Vector2 m = moveAction.ReadValue<Vector2>();
        // transform.position += new Vector3(move.x, 0f, move.y);
        Debug.Log(m);
        // Vector2 move = playerActions.PlayerControls.Movement.ReadValue<Vector2>();
        // transform.position += new Vector3(move.x, 0f, move.y);
        // Debug.Log(move);
    }
}
