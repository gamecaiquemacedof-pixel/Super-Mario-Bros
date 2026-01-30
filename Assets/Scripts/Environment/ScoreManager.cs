using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public TMP_Text contador_;

    private int score;

    void Start()
    {
        score = 0;
    }

    void Update()
    {
        contador_.text = score.ToString();
    }

    public void AddScore()
    {
        score++;
    }

    public void AddScore(int score)
    {
        this.score = this.score + score;
    }
}
