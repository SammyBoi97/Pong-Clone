using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    public float startingSpeed = 5f;
    public float startingAngleMax = 45;
    private Vector2 randomDirection;

    private Vector2 velVector;

    private Vector2 contactPoint;

    private Vector2 paddleMiddleVector;
    private Vector2 paddleEdgeVector;
    private Vector2 paddleVector;

    public float angleAdjustmentfactor;
    public float speedIncrease = 0.1f;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.anyKeyDown)
        {
            if (GameManager.Instance.gamePlaying)
            {
                return;
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (GameManager.Instance.winnerText.activeInHierarchy)
                {
                    GameManager.Instance.NewGame();
                    return;
                }

                randomDirection = new Vector2(Random.value < 0.5f ? -1 : 1, Random.Range(-Mathf.Tan(startingAngleMax * Mathf.Deg2Rad), Mathf.Tan(startingAngleMax * Mathf.Deg2Rad))).normalized;
                StartBall(randomDirection);
            }
            else if (Input.GetKeyDown(KeyCode.Keypad1))
            {
                randomDirection = new Vector2(-1, -1);
                StartBall(randomDirection);
            }
            else if (Input.GetKeyDown(KeyCode.Keypad4))
            {
                randomDirection = new Vector2(-1, 0);
                StartBall(randomDirection);
            }
            else if (Input.GetKeyDown(KeyCode.Keypad7))
            {
                randomDirection = new Vector2(-1, 1);
                StartBall(randomDirection);
            }
            else if (Input.GetKeyDown(KeyCode.Keypad3))
            {
                randomDirection = new Vector2(1, -1);
                StartBall(randomDirection);
            }
            else if (Input.GetKeyDown(KeyCode.Keypad6))
            {
                randomDirection = new Vector2(1, 0);
                StartBall(randomDirection);
            }
            else if (Input.GetKeyDown(KeyCode.Keypad9))
            {
                randomDirection = new Vector2(1, 1);
                StartBall(randomDirection);
            }
        }
        

    }

    public void StartBall(Vector2 direction)
    {
        GetComponent<Rigidbody2D>().AddForce(direction * startingSpeed, ForceMode2D.Impulse);
        GameManager.Instance.StartGame();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Score Zone"))
        {
            gameObject.transform.position = Vector2.zero;
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;

            if (collision.gameObject.tag == "Player 1 Zone")
            {
                ScoreController.Instance.UpdateScore(false);
            }
            else if (collision.gameObject.tag == "Player 2 Zone")
            {
                ScoreController.Instance.UpdateScore(true);
            }
        }
    }

    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag != "Player")
        {
            return;
        }

        velVector = -collision.relativeVelocity;
        contactPoint = collision.transform.InverseTransformPoint(collision.GetContact(0).point);

        paddleMiddleVector = Vector2.right * Mathf.Sign(contactPoint.x);
        paddleEdgeVector = Vector2.up * Mathf.Sign(contactPoint.y);

        float fractionOfContactPoint = Mathf.Abs(contactPoint.y) * 2;

        paddleVector = Vector2.Lerp(paddleMiddleVector, paddleEdgeVector, fractionOfContactPoint);

        Vector2 resultVector = new Vector2(-velVector.x, velVector.y) + new Vector2(paddleVector.x, paddleVector.y * angleAdjustmentfactor);

        GetComponent<Rigidbody2D>().velocity = resultVector.normalized * velVector.magnitude * (1 + speedIncrease);
    }
}
