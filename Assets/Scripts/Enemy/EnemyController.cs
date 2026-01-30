using System;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed = 2f;
    public LayerMask groundLayer;

    private Rigidbody2D rb;
    private bool movingRight = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        float dir = movingRight ? 1f : -1f;
        rb.linearVelocity = new Vector2(dir * speed, rb.linearVelocity.y);
    }

    void OnCollisionEnter2D(Collision2D collision) => HandleCollision(collision);

    void OnCollisionStay2D(Collision2D collision) => HandleCollision(collision);

    void HandleCollision(Collision2D collision)
    {
        // Checa layer Ground (use .value)
        if (((1 << collision.gameObject.layer) & groundLayer.value) == 0) return;

        // Pega o contato mais "lateral" (maior |normal.x|)
        float bestAbsX = 0f;
        float bestX = 0f;

        foreach (var c in collision.contacts)
        {
            float ax = Mathf.Abs(c.normal.x);
            if (ax > bestAbsX)
            {
                bestAbsX = ax;
                bestX = c.normal.x;
            }
        }

        // Agora decide (0.2f é mais tolerante pra quinas/diagonais)
        if (bestAbsX > 0.2f)
        {
            if (bestX < 0f)
            {
                // parede está à direita do inimigo -> vai pra esquerda
                movingRight = false;
                FlipSprite();
                // opcional: dá um "empurrãozinho" pra não grudar
                rb.position += Vector2.left * 0.01f;
            }
            else
            {
                // parede está à esquerda do inimigo -> vai pra direita
                movingRight = true;
                FlipSprite();
                rb.position += Vector2.right * 0.01f;
            }
        }
    }

    void FlipSprite()
    {
        Vector3 scale = transform.localScale;
        scale.x = Mathf.Abs(scale.x) * (movingRight ? 1 : -1);
        transform.localScale = scale;
    }
}
