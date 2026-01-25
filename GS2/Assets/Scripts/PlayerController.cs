using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    //private PlayerControls controls;
    private float speed = 3.0f;
    
    
    private PlayerInput input;
    private InputAction moveAction;
    

    private void Awake()
    {
        //controls = new PlayerControls();
        input = GetComponent<PlayerInput>();
        moveAction = input.actions.FindAction("Move");
    }

    private void Start()
    {
        
    }

    /*private void OnEnable()
    {
        controls.Enable();
        controls.Regular.Move.performed += move;
    }

    private void OnDisable()
    {
        controls.Disable();
        controls.Regular.Move.performed -= move;
    }*/

    private void Update()
    {
        //Vector2 move = controls.Regular.Move.ReadValue<Vector2>();
        //float jump = controls.Regular.Jump.ReadValue<float>();
        movePlayer();
    }

    private void movePlayer()
    {
        Vector2 movement = moveAction.ReadValue<Vector2>();
        transform.position += new Vector3(movement.x, 0, movement.y) * speed * Time.deltaTime;
        
    }
}
