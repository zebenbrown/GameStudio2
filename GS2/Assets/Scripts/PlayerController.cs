using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    //private PlayerControls controls;
    [SerializeField]
    private float speed = 5.0f;

    private static float health = 150;

    //to have the camera follow the player
    [SerializeField] private Camera camera;
    
    [SerializeField] private TextMeshProUGUI healthText;
    
    
    private PlayerInput input;
    private InputAction moveAction;
    private InputAction jumpAction;

    private AudioSource audioSource;

    private bool isPlaying;
    

    private bool isMoving = false;

    private void Awake()
    {

        //controls = new PlayerControls();
        input = GetComponent<PlayerInput>();
        moveAction = input.actions.FindAction("Move");
        jumpAction = input.actions.FindAction("Jump");

        audioSource = GetComponent<AudioSource>();

        isPlaying = false;
        healthText.text = "Health: " + health;
    }

    private void Update()
    {
        movePlayer();
    }

    /*private void movePlayer()
    {
        Vector2 inputVector = moveAction.ReadValue<Vector2>();
        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);

        if (moveDir.sqrMagnitude > 0.001f)
        {
            // Move
            transform.position += moveDir * speed * Time.deltaTime;

            // Rotate to face movement direction
            Quaternion targetRotation = Quaternion.LookRotation(moveDir, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 15f * Time.deltaTime);
        }
    }*/

    
    private void movePlayer()
    {
        /*Vector2 movementValues = moveAction.ReadValue<Vector2>();
        Vector3 movement = new Vector3(movementValues.x, 0f, movementValues.y);
        transform.position += new Vector3(movement.x, 0, movement.z) * speed * Time.deltaTime;
        /*Quaternion rotation = Quaternion.LookRotation(movement, Vector3.up);
        Debug.Log(rotation);
        transform.rotation = rotation;#1#
        //transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 0.5f);*/
        
        Vector2 movementValues = moveAction.ReadValue<Vector2>();
        Vector3 movement = new Vector3(movementValues.x, 0f, movementValues.y);

        if (movement.sqrMagnitude > 0.001f)
        {
            // Move
            transform.position += movement * speed * Time.deltaTime;

            // Rotate to face movement direction
            Quaternion targetRotation = Quaternion.LookRotation(movement, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 2f * Time.deltaTime);
        }
        
        

        if ((movement.x != 0) || (movement.z != 0))
        {
            if (!isPlaying)
            {
                PlayWalkingSound();
                isPlaying = true;
            }
        }
        else
        {
            if (isPlaying)
            {
                StopWalkingSound();
                isPlaying = false;
            }
        }

        camera.transform.position += new Vector3(movement.x, 0, movement.z) * speed * Time.deltaTime;

        /*if ((movement.x != 0) || (movement.y != 0))
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
        }*/
    }

    private void PlayWalkingSound()
    {
        audioSource.Play();
    }

    private void StopWalkingSound()
    {
        audioSource.Stop();
    }
    

    private void jump()
    {
        //dont know if we need this
    }

    public static void takeDamage(float damage)
    {
        health -= damage;
    }
}
