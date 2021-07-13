using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMovement : MonoBehaviour
{
    [SerializeField] private float ballSpeed;
    [SerializeField] private float extraSpeedPerHit;
    [SerializeField] private float maxBallSpeed;
    [SerializeField] private GameObject wallPrefab;
    [SerializeField] private RacketPlayer1 player1;
    [SerializeField] private RacketPlayer2 player2;

    private Vector2 ballLeftPosition = new Vector2(-91, 13);
    private Vector2 ballRightPosition = new Vector2(91, 13);
    private int hitCounter = 0;
    [SerializeField] private bool gameRestart = false;
    private Rigidbody2D rigid;

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        StartCoroutine(this.StartBall());
        StartCoroutine(CheckBall());
    }

    private IEnumerator CheckBall()
    {

        float oldSpeed = rigid.velocity.x;
        yield return new WaitForSeconds(5);
        float newSpeed = rigid.velocity.x;
        float treshold = 10f;

        if (Mathf.Abs(newSpeed - oldSpeed) < treshold && rigid.velocity.x <= 5 && rigid.velocity.x >= -5 && !gameRestart)
        {
            Debug.LogWarning("Ball Stuck, Spawning a Wall!");
            StartCoroutine(SpawnWall());
        }

        yield return new WaitForSeconds(3);
        StartCoroutine(CheckBall());
    }

    private IEnumerator SpawnWall()
    {

        yield return new WaitForSeconds(1);
        float zAngle = Random.Range(20, 45);
        Quaternion angle = Quaternion.Euler(0, 0, zAngle);
        Vector2 position = this.transform.position + new Vector3(0, 40, 0);

        // Spawn wall.
        GameObject wall = Instantiate(wallPrefab, position, angle);
        wall.AddComponent<DestroyWall>().waitUntilDestroy = 2;
    }

    public IEnumerator StartBall(bool isStartingPlayer1 = true)
    {
        gameRestart = true;

        this.hitCounter = 0;
        yield return new WaitForSeconds(2);
        if (isStartingPlayer1)
        {
            this.MoveBall(new Vector2(-1, 0));
        }
        else
        {
            this.MoveBall(new Vector2(1, 0));
        }

        gameRestart = false;
    }

    public float GetMaxSpeed()
    {
        return maxBallSpeed;
    }

    public void MoveBall(Vector2 dir)
    {
        dir = dir.normalized;
        float speed = this.ballSpeed + this.hitCounter * this.extraSpeedPerHit;
        // add force to the rigid body.

        if (speed > this.maxBallSpeed)
        {
            speed = this.maxBallSpeed;
        }

        if (speed < -this.maxBallSpeed)
        {
            speed = -this.maxBallSpeed;
        }

        rigid.velocity = dir * speed;
    }

    public void IncreaseHitCounter()
    {
        if (this.hitCounter * this.extraSpeedPerHit <= this.maxBallSpeed)
        {
            this.hitCounter++;
        }
    }

    public void PositionBall(bool isStartingPlayer1 = true)
    {
        rigid.velocity = new Vector2(0, 0);                     // Velocity set to 0.

        if (isStartingPlayer1)
        {
            this.transform.position = ballLeftPosition;
        }
        else
        {
            this.transform.position = ballRightPosition;
        }

        StartCoroutine(this.StartBall(isStartingPlayer1));

    }

}
