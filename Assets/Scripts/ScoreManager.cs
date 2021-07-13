using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;


public class ScoreManager : MonoBehaviour
{
    public GameObject ball;

    [SerializeField] private RacketPlayer2 player2;
    [SerializeField] private GameData gameData;
    [SerializeField] private float wait = 10;
    [SerializeField] private float walls = 3;
    [SerializeField] private RandomWallSpawner randomWallSpawner;
    [SerializeField] private TextMeshProUGUI BallSpeed;
    [SerializeField] private TextMeshProUGUI MaxBallSpeed;
    [SerializeField] private TextMeshProUGUI playerOneScoreText;
    [SerializeField] private TextMeshProUGUI playerTwoScoreText;

    private int scoreToWin;
    private int playerOneScore = 0;
    private int playerTwoScore = 0;
    private Rigidbody2D ballRigid;

    void Start()
    {
        scoreToWin = gameData.scoreToWin;
        Debug.Log("scoreTowIn: " + scoreToWin);

        // Make decision if player/AI controller on racket 2.
        RacketPlayer2 player2Controller = player2.GetComponent<RacketPlayer2>();
        AIRacket player2AI = player2.GetComponent<AIRacket>();
        if (gameData.againstPlayer)
        {
            player2Controller.enabled = true;
            player2AI.enabled = false;
        }
        else
        {
            player2Controller.enabled = false;
            player2AI.enabled = true;
        }

        // prepare scores and other values.
        ballRigid = ball.GetComponent<Rigidbody2D>();
        playerOneScoreText.text = playerOneScore.ToString();
        playerTwoScoreText.text = playerTwoScore.ToString();
        MaxBallSpeed.text = ball.GetComponent<BallMovement>().GetMaxSpeed().ToString();
        StartCoroutine(SpawnWall());
    }

    void FixedUpdate()
    {
        BallSpeed.text = Mathf.Floor(ball.gameObject.GetComponent<Rigidbody2D>().velocity.x).ToString();

    }

    private IEnumerator SpawnWall()
    {
        yield return new WaitForSeconds(wait + 2);
        for (int i = 0; i < walls; i++)
        {
            randomWallSpawner.SpawnWall(wait);
        }

        StartCoroutine(SpawnWall());
    }

    public void Score(string player)
    {
        if (player == "Player1")
        {
            playerOneScore++;
            if (playerOneScore >= scoreToWin)
            {
                SceneManager.LoadScene(2);
            }
        }
        else if (player == "Player2")
        {
            playerTwoScore++;
            if (playerTwoScore >= scoreToWin)
            {
                SceneManager.LoadScene(2);
            }
        }
        CheckScore();
    }

    private void CheckScore()
    {

        if (playerOneScore > playerTwoScore)
        {
            if ((scoreToWin - playerOneScore) < 2)
            {
                playerOneScoreText.color = Color.red;
            }
            else
            {
                playerOneScoreText.color = Color.green;
            }

            playerTwoScoreText.color = Color.white;

        }
        else if (playerOneScore == playerTwoScore)
        {
            playerOneScoreText.color = Color.yellow;
            playerTwoScoreText.color = Color.yellow;
        }
        else
        {
            {
                if ((scoreToWin - playerTwoScore) < 2)
                {
                    playerTwoScoreText.color = Color.red;
                }
                else
                {
                    playerTwoScoreText.color = Color.green;
                }
                playerOneScoreText.color = Color.white;
            }
        }


        playerOneScoreText.text = playerOneScore.ToString();
        playerTwoScoreText.text = playerTwoScore.ToString();
    }

}
