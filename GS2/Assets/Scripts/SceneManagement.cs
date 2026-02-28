using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    public static int enemiesKilled = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (scoreText != null)
        {
            scoreText.text = "Enemies\nKilled: " + enemiesKilled;
        }
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

