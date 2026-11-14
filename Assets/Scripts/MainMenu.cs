using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string gameSceneName = "MainLevel";

    public void PlayGame()
    {
        SceneManager.LoadScene(gameSceneName);
    }

    public void ResetBestTime()
    {
        PlayerPrefs.DeleteKey("BestTime");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}