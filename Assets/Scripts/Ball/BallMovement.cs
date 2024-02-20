using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMovement : MonoBehaviour
{

    [Header("Ball Variables")]
    public float speed = 5f;
    public float startAngle = 90f;

    public PlayerScore pScore;

    private Rigidbody rb;
    private Vector3 lastVel;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Movement();
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
        float radians = randomAngle * Mathf.Deg2Rad;

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
            rb.velocity = reflection.normalized * speed;

            if (col.gameObject.CompareTag("Brick"))
            {
                pScore.AddScore();
                Destroy(col.gameObject);
            }
        }
    }
}
