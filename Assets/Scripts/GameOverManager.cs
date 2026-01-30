using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    [SerializeField] GameObject gameOverUI;

    public void ShowGameOver()
    {
        gameOverUI.SetActive(true);

        PauseGame();
    }

    void Update()
    {
        if (gameOverUI.activeSelf && Input.GetKeyDown(KeyCode.R))
        {
            Restart();
        }
    }

    void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void PauseGame()
    {
        Time.timeScale = 0f;
    }
}
