using UnityEngine;

public class AbyssController : MonoBehaviour
{
    private GameOverManager gameOverManager;

    private void Start()
    {
        gameOverManager = FindFirstObjectByType<GameOverManager>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            gameOverManager.ShowGameOver();
        }
    }
}
