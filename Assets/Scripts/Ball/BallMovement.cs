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

    [SerializeField] private Camera mainCam;
    [SerializeField] private Camera spectatorCam;

    private Rigidbody rb;
    private Vector3 lastVel;
    private bool ballStarted;

    private float brickCount = 1;

    [SerializeField] private FreezeFrame freeze;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        freeze = GetComponent<FreezeFrame>();
        ballStarted = false;

        mainCam.enabled = true;
        spectatorCam.enabled = false;
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
            Movement(false);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        lastVel = rb.velocity;

        if (rb.velocity == Vector3.zero)
        {
            Movement(true);
        }
    }

    void Movement( bool random )
    {
        float angle = startAngle;

        if (random)
        {
            angle = Random.Range(30f, 150f);
        }

        // Convert the angle to radians
        float radians = angle * Mathf.Deg2Rad;

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

                if (Mathf.Abs(nReflection.x) == 1 || Mathf.Abs(nReflection.y) < 0.1)
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
                freeze.Freeze();

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

        if (col.gameObject.CompareTag("Spectator"))
        {
            Debug.Log("Spectator");
            spectatorCam.enabled = true;
            mainCam.enabled = false;
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.CompareTag("Spectator"))
        {
            Debug.Log("Main");
            mainCam.enabled = true;
            spectatorCam.enabled = false;
        }
    }

    public Vector3 RBVelocity()
    {
        return rb.velocity;
    }
}
