using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMovement : MonoBehaviour
{

    [Header("Ball Variables")]
    public float speed = 5f;
    public float startAngle = 90f;
    public float maxMultiplier = 20f;

    public PlayerScore pScore;

    public Transform platform;

    private Rigidbody rb;
    private Vector3 lastVel;
    private bool ballStarted;

    private float brickCount = 1;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        ballStarted = false;
    }

    void Update()
    {
        if (!ballStarted)
        {
            rb.MovePosition(new Vector3(platform.position.x, -18f, 0f));
        }

        if (Input.GetKeyDown(KeyCode.Mouse0) && !ballStarted)
        {
            ballStarted = true;
            Movement();
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        lastVel = rb.velocity;
    }

    void Movement()
    {
        // Generate a random angle for the ball's initial movement
        float randomAngle = Random.Range(30f, 150f); // Example range, adjust as needed

        // Convert the angle to radians
        float radians = startAngle * Mathf.Deg2Rad;

        // Calculate the x and y components of the velocity based on the angle
        float xVelocity = Mathf.Cos(radians) * speed;
        float yVelocity = Mathf.Sin(radians) * speed;

        // Apply the velocity to the Rigidbody
        rb.velocity = new Vector3(xVelocity, yVelocity, 0f);
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Platform") || col.gameObject.CompareTag("Wall") || col.gameObject.CompareTag("Brick"))
        {
            Vector3 reflection = Vector3.Reflect(lastVel, col.contacts[0].normal);
            Vector3 nReflection = reflection.normalized;

            Debug.Log(nReflection);

            if (col.gameObject.CompareTag("Wall"))
            {
                // brickCount = 1;

                if (Mathf.Abs(nReflection.x) == 1)
                {
                    nReflection.x = Random.Range(0.5f, 0.9f);

                    float rand = Random.Range(0.5f, 0.9f);

                    if (rand > nReflection.x)
                    {
                        nReflection.y = 1 - nReflection.x;
                    }
                    else
                    {
                        nReflection.y = -1 * (1 - nReflection.x);
                    }
                }
            }

            if (col.gameObject.CompareTag("Platform"))
            {
                brickCount = 1;

                if (Mathf.Abs(nReflection.y) == 1)
                {
                    nReflection.y = Random.Range(0.5f, 0.9f);

                    float rand = Random.Range(0.5f, 0.9f);

                    if (rand > nReflection.y)
                    {
                        nReflection.x = 1 - nReflection.y;
                    }
                    else
                    {
                        nReflection.x = -1 * (1 - nReflection.y);
                    }
                }
            }

            Debug.Log(nReflection);

            rb.velocity = nReflection * (speed + brickCount);

            if (col.gameObject.CompareTag("Brick"))
            {
                brickCount += 1;

                if (brickCount > maxMultiplier)
                {
                    brickCount = maxMultiplier;
                }

                CamShake.Shake(brickCount);

                pScore.AddScore((int)brickCount);

                Destroy(col.gameObject);
            }
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Loss"))
        {
            Debug.Log("Game Over");
            pScore.gameOver = true;
        }
    }
}
