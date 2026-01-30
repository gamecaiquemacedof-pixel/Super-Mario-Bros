using System.Collections;
using UnityEngine;

public class BlockHit : MonoBehaviour
{
    public float moveUpDistance = 0.15f; // quanto o bloco sobe
    public float moveSpeed = 6f;         // velocidade do movimento

    private ScoreManager scoreManager;
    private Vector3 startPosition;
    private bool isMoving = false;

    void Start()
    {
        scoreManager = FindFirstObjectByType<ScoreManager>();

        startPosition = transform.position;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Player")) return;

        foreach (ContactPoint2D contact in collision.contacts)
        {
            // só reage se o player bater POR BAIXO
            if (contact.normal.y > 0.5f && !isMoving)
            {
                Recoil(collision);
                StartCoroutine(HitAnimation());
                scoreManager.AddScore();
            }
        }
    }

    IEnumerator HitAnimation()
    {
        isMoving = true;

        Vector3 upPosition = startPosition + Vector3.up * moveUpDistance;

        // sobe
        while (transform.position.y < upPosition.y)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                upPosition,
                moveSpeed * Time.deltaTime
            );
            yield return null;
        }

        // desce
        while (transform.position.y > startPosition.y)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                startPosition,
                moveSpeed * Time.deltaTime
            );
            yield return null;
        }

        transform.position = startPosition;
        isMoving = false;
    }

    void Recoil(Collision2D collision)
    {
        collision.transform.position = new Vector3(
            collision.transform.position.x,
            collision.transform.position.y - 0.2f,
            collision.transform.position.z);
    }
}
