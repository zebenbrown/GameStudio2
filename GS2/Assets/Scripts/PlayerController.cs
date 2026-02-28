using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    //private PlayerControls controls;
    public float health = 150;

    //to have the camera follow the player
    [SerializeField] private new Camera camera;
    
    [SerializeField] private TextMeshProUGUI healthText;
    
    
    private PlayerInput input;
    private InputAction moveAction;
    private InputAction jumpAction;

    private AudioSource audioSource;
    private bool isPlaying;

    [SerializeField] private float maxSpeed = 6f;
    [SerializeField] private float rotationSpeed = 10f;
    [SerializeField] private float acceleration = 20f;
    [SerializeField] private float deceleration = 25f;
    private Vector3 currentVelocity = Vector3.zero;

    private Vector3 cameraOffset;

    private void Awake()
    {

        //controls = new PlayerControls();
        input = GetComponent<PlayerInput>();
        moveAction = input.actions.FindAction("Move");
        jumpAction = input.actions.FindAction("Jump");

        audioSource = GetComponent<AudioSource>();

        isPlaying = false;
        healthText.text = "Health: " + health;
        
        var pause = GetComponent<PlayerInput>()
            .actions.FindAction("Pause");

    }

    private void Start()
    {
        cameraOffset = transform.position - camera.transform.position;
        PlaySwimSound();
    }

    private void Update()
    {
        movePlayer();
        rotatePlayer();

        audioSource.volume = currentVelocity.magnitude / maxSpeed;

        healthText.text = "Health: " + health;
    }
    
    private void movePlayer()
    {
        
        Vector2 movementValues = moveAction.ReadValue<Vector2>();
        Vector3 movement = new Vector3(movementValues.x, 0f, movementValues.y);
        Vector3 inputDirection = new Vector3(movementValues.x, 0f, movementValues.y);
        
        if (movement.sqrMagnitude > 0.001f)
        {
            //GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            //GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
            inputDirection.Normalize();

            //Accelerate to target speed
            Vector3 targetVelocity = inputDirection * maxSpeed;
            currentVelocity = Vector3.MoveTowards(currentVelocity, targetVelocity, acceleration * Time.deltaTime);
        }
        else
        {
            //GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            //Decelerate to stop
            currentVelocity = Vector3.MoveTowards(currentVelocity, Vector3.zero, deceleration * Time.deltaTime);
        }

        transform.position += currentVelocity * Time.deltaTime;

        camera.transform.position = transform.position - cameraOffset;
        camera.transform.position += currentVelocity * Time.deltaTime;
    }

    private void rotatePlayer()
    {
        Vector2 mousePosition = Mouse.current.position.ReadValue();
        
        Ray ray = Camera.main.ScreenPointToRay(mousePosition);
        
        Plane plane = new Plane(Vector3.up, transform.position);

        if (plane.Raycast(ray, out float distance))
        {
            Vector3 hitPoint = ray.GetPoint(distance);
            
            Vector3 hitDirection = hitPoint - transform.position;
            hitDirection.y = 0.0f;
            
            Quaternion targetRotation = Quaternion.LookRotation(hitDirection);
            
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
        }
    }

    private void PlaySwimSound()
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

    public void takeDamage(float damage)
    {
        health -= damage;
    }

    public float getPlayerHealth() { return health; }
}
