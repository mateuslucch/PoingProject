using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameSession : MonoBehaviour
{
    [SerializeField] string player1Tag, player2Tag;
    [SerializeField] TextMeshProUGUI player1ScoreText, player2ScoreText = null;
    [SerializeField] float ballReleaseCountdown = 1f;
    BallMove ball = null;

    int player1Score, player2Score;
    float xDirection;

    private void Start()
    {
        ball = FindObjectOfType<BallMove>();
        xDirection = Random.Range(0f, 10f) / 10;
        if (xDirection > 5f)
        {
            xDirection = 1;
        }
        else { xDirection = -1; }

        StartCoroutine(ball.LaunchBallCountdown(ballReleaseCountdown, xDirection));

        ResetScore();
    }

    public void CountScore(string wallTag)
    {
        if (wallTag == player1Tag)
        {
            player1Score++;
            PrintScore(player1Score, player1ScoreText);
            xDirection = 1;
        }
        else if (wallTag == player2Tag)
        {
            player2Score++;
            PrintScore(player2Score, player2ScoreText);
            xDirection = -1;
        }
        else { Debug.LogError("Wrong tag! Check if the values of wall tags check with gamesession"); }

        StartCoroutine(ball.LaunchBallCountdown(3f, xDirection));
    }

    private void PrintScore(int score, TextMeshProUGUI textBox)
    {
        if (score <= 9) { textBox.text = $"0{score}"; }
        else { textBox.text = $"{score}"; }
    }

    public void ResetScore()
    {
        player1Score = 0;
        player2Score = 0;
        PrintScore(player1Score, player1ScoreText);
        PrintScore(player2Score, player2ScoreText);
    }


}
