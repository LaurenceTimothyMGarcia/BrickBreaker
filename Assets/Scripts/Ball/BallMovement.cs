using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMovement : MonoBehaviour
{

    [Header("Ball Variables")]
    public float speed = 5f;
    public float startAngle = 90f;

    private Rigidbody rb;
    private float currAngle;
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
        currAngle = Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg;
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

        lastVel = rb.velocity;
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Platform") || col.gameObject.CompareTag("Brick"))
        {
            Debug.Log("Collision with: " + col.gameObject.name);
            Debug.Log("Collision normal: " + col.contacts[0].normal);

            // Calculate reflection angle
            Debug.Log(lastVel);
            Vector3 reflection = Vector3.Reflect(lastVel, col.contacts[0].normal);
            Debug.Log(reflection);
            Debug.Log(reflection.normalized);
            rb.velocity = reflection.normalized * speed;
        }
    }
}
