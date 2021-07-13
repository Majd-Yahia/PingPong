using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionController : MonoBehaviour
{

    public BallMovement ballMovement;
    public GameObject sphere;
    public ScoreManager scoreManager;
    void BounceFromRacket(Collision2D other)
    {
        Vector3 ballPosition = this.transform.position;
        Vector3 racketPosition = other.gameObject.transform.position;

        // get the racket height
        float racketHeight = other.collider.bounds.size.y;
        float x;

        if (other.gameObject.name == "RacketPlayer1")
        {
            x = 1;
        }
        else
        {
            x = -1;
        }

        float y = (ballPosition.y - racketPosition.y) / racketHeight;

        this.ballMovement.IncreaseHitCounter();
        this.ballMovement.MoveBall(new Vector2(x, y));
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.name == "RacketPlayer1" || other.gameObject.name == "RacketPlayer2")
        {
            this.BounceFromRacket(other);
        }
        else if (other.gameObject.name == "WallLeft")
        {
            this.scoreManager.Score("Player2");
            this.ballMovement.PositionBall(true);
        }
        else if (other.gameObject.name == "WallRight")
        {
            scoreManager.Score("Player1");
            this.ballMovement.PositionBall(false);
        }

    }

}
