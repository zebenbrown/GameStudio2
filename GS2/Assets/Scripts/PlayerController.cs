using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    //private PlayerControls controls;
    [SerializeField]
    private float speed = 5.0f;

    private float health = 150;

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

    private void movePlayer()
    {
        Vector2 movement = moveAction.ReadValue<Vector2>();
        transform.position += new Vector3(movement.x, 0, movement.y) * speed * Time.deltaTime;

        if ((movement.x != 0) || (movement.y != 0))
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
}
