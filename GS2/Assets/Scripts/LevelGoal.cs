using UnityEngine;

public class LevelGoal : MonoBehaviour
{
    private SceneManagement sceneManager;

    private void Start()
    {
        sceneManager = FindAnyObjectByType<SceneManagement>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            sceneManager.loadScene(SceneManagement.SceneNames.LevelComplete);
        }
    }
}
