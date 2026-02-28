using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void loadScene(string scene)
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(scene);
    }

    public void loadScene(SceneNames scene)
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(scene.ToString());
    }
    public void loadScene(SceneNames scene, int enemiesKilled)
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(scene.ToString());
    }

    public void reloadCurrentScene()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void quit()
    {
        Application.Quit();
    }
    public enum SceneNames
    {
        Main,
        Level1,
        GameOver,
        LevelComplete,
        MainMenu,
    }
}

