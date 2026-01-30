using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public GameObject goomba;
    public float respawnDelay = 3f;

    private GameObject currentGoomba;
    private float timer;
    private bool waitingRespawn;

    void Start()
    {
        SpawnGoomba();
    }

    private void Update()
    {
        if (currentGoomba == null && !waitingRespawn)
        {
            waitingRespawn = true;
            timer = respawnDelay;
        }

        CoolDownSpawn();
    }

    void CoolDownSpawn()
    {

        if (waitingRespawn)
        {
            timer -= Time.deltaTime;

            if (timer <= 0f)
            {
                SpawnGoomba();
                waitingRespawn = false;
            }
        }
    }

    void SpawnGoomba()
    {
        currentGoomba = Instantiate(goomba, goomba.transform.position, Quaternion.identity);
    }
}