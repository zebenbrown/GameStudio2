using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    //private PlayerControls controls;
    [SerializeField]
    private float speed = 5.0f;

    //to have the camera follow the player
    [SerializeField] private Camera camera;
    
    
    private PlayerInput input;
    private InputAction moveAction;
    private InputAction jumpAction;

    private bool isMoving = false;
    private AudioSource audioSource;


    private void Awake()
    {
        //controls = new PlayerControls();
        input = GetComponent<PlayerInput>();
        moveAction = input.actions.FindAction("Move");
        jumpAction = input.actions.FindAction("Jump");

        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        movePlayer();
    }

    private void movePlayer()
    {
        Vector2 movement = moveAction.ReadValue<Vector2>();
        transform.position += new Vector3(movement.x, 0, movement.y) * speed * Time.deltaTime;
        camera.transform.position += new Vector3(movement.x, 0, movement.y) * speed * Time.deltaTime;

        if ((movement.x != 0) || (movement.y != 0))
        {
            if (!isMoving)
            {
                audioSource.Play();
                isMoving = true;
            }
        }
        else
        {
            if (isMoving)
            {
                audioSource.Stop();
                isMoving = false;
            }
        }
    }

    private void jump()
    {
        //dont know if we need this
    }
}
