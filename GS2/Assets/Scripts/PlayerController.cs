using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    //private PlayerControls controls;
    [SerializeField]
    private float speed = 5.0f;
    
    
    private PlayerInput input;
    private InputAction moveAction;
    private InputAction jumpAction;
    

    private void Awake()
    {
        //controls = new PlayerControls();
        input = GetComponent<PlayerInput>();
        moveAction = input.actions.FindAction("Move");
        jumpAction = input.actions.FindAction("Jump");
    }

    private void Update()
    {
        movePlayer();
    }

    private void movePlayer()
    {
        Vector2 movement = moveAction.ReadValue<Vector2>();
        transform.position += new Vector3(movement.x, 0, movement.y) * speed * Time.deltaTime;
    }

    private void jump()
    {
        //dont know if we need this
    }
}
