using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Pause : MonoBehaviour
{
    [SerializeField] private GameObject pausePanel;

    [SerializeField] private PlayerInput input;
  
    
    private InputAction pauseAction;

    private bool isPaused = false;

    private void Awake()
    {
        pauseAction = input.actions.FindAction("Pause");
    }

    private void OnEnable()
    {
        pauseAction.performed += OnPause;
        pauseAction.Enable();
    }

    private void OnDisable()
    {
        pauseAction.performed -= OnPause;
        pauseAction.Disable();
    }

    private void OnPause(InputAction.CallbackContext context)
    {
        Debug.Log("Pause script received input");
        if (isPaused)
        {
            resume();
        }
        else
        {
            pause();
        }
    }

    //Pause Menu Functions
    public void pause()
    {
        pausePanel.SetActive(true);
        Time.timeScale = 0;
        isPaused = true;
    }

    public void resume()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1;
        isPaused = false;
    }
}
