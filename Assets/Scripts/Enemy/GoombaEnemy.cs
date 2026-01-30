using System.ComponentModel;
using UnityEngine;
using UnityEngine.Rendering.Universal.Internal;

public class GoombaEnemy : MonoBehaviour
{
    private static int pointsForDeath = 10;
    private ScoreManager scoreManager;
    private GameOverManager gameOverManager;

    private void Start()
    {
        scoreManager = FindFirstObjectByType<ScoreManager>();
        gameOverManager = FindFirstObjectByType<GameOverManager>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Player")) return;

        Rigidbody2D playerRb = collision.rigidbody;

        foreach (ContactPoint2D contact in collision.contacts)
        {
            // Player caiu EM CIMA do inimigo
            if (contact.normal.y < -0.5f && playerRb.linearVelocity.y < 0f)
            {
                Die();
                BouncePlayer(playerRb);
                scoreManager.AddScore(pointsForDeath);
                return;
            }
        }

        // Se n�o caiu por cima ? player morre
        KillPlayer(collision.gameObject);
    }

    void Die()
    {
        Destroy(gameObject);
    }

    void BouncePlayer(Rigidbody2D playerRb)
    {
        // for�a do pulo para o eixo Y ao matar inimigo
        playerRb.linearVelocity = new Vector2(playerRb.linearVelocity.x, 6f);
    }

    void KillPlayer(GameObject player)
    {
        Debug.Log("Player morreu");
        gameOverManager.ShowGameOver();
    }
}
