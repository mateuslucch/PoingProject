using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMove : MonoBehaviour
{

    [SerializeField] float xPush = 0f;
    [SerializeField] float yPush = 15f;

    [Header("Range Release")]
    [Range(1, 8)]
    [SerializeField] float yMinRange = 1f;
    [Range(8, 10)]
    [SerializeField] float yMaxRange = 10f;   

    [SerializeField] float xVel = 5f;
    [SerializeField] float yVel = 12f;

    float randomFactorY;
    float randomFactorX;
    float myVelocity;
    Vector2 ballStartPosition;
    Vector2 velocityTweak;
    Rigidbody2D myRigidBody2D;

    void Start()
    {
        ballStartPosition = new Vector2(transform.position.x, transform.position.y);
        myVelocity = Mathf.Sqrt((xVel * xVel) + (yVel * yVel));
        myRigidBody2D = GetComponent<Rigidbody2D>();
    }

    public void LaunchBall(float direction)
    {
        // rather than y, x direction depends on who get scored
        float randomXDirection = Random.Range(1f, 10f) / 10;        
        if (direction == 0)
        {
            if (randomXDirection > 0.5f) { randomXDirection = 1f; }
            else { randomXDirection = -1f; }
        }
        else { randomXDirection = direction; }
        xPush = xVel * randomXDirection;

        float randomYDirection = Random.Range(1f, 10f) / 10;

        if (randomYDirection > 0.5f) { randomYDirection = 1f; }
        else { randomYDirection = -1f; }
        yPush = (Random.Range(yMinRange, yMaxRange) / 10) * yVel * randomYDirection;

        GetComponent<Rigidbody2D>().velocity = new Vector2(xPush, yPush);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //angulo colisao corrigido, parte comunidade, parte minha solução
        float speedY = myRigidBody2D.velocity.y;
        float speedX = myRigidBody2D.velocity.x;

        if (speedX == 0) { randomFactorX = 0.4f; }
        else { randomFactorX = 0f; }

        if (speedY >= -1.6f && speedY <= 1.6f)
        {
            if (speedY >= 0) { randomFactorY = 0.8f; }
            else { randomFactorY = -0.8f; }
        }
        else { randomFactorY = 0f; }

        Vector2 velocityTweak = new Vector2(Random.Range(-randomFactorX, randomFactorX), randomFactorY);

        myRigidBody2D.velocity += velocityTweak;
        myRigidBody2D.velocity = myRigidBody2D.velocity.normalized * myVelocity;

        ResetBallPosition(collision);
    }

    private void ResetBallPosition(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<GoalWall>())
        {
            myRigidBody2D.velocity = new Vector2(0f, 0f);
            transform.position = new Vector2(ballStartPosition.x, ballStartPosition.y);
        }
    }

    public IEnumerator LaunchBallCountdown(float timeToLaunch, float direction)
    {
        yield return new WaitForSecondsRealtime(timeToLaunch);
        LaunchBall(direction);
    }
}
