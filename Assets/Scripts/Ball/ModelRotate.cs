using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelRotate : MonoBehaviour
{
    [SerializeField] private BallMovement ballMovement;

    private Vector3 ballRotation;

    // Update is called once per frame
    void Update()
    {
        ballRotation = ballMovement.RBVelocity().normalized;
        float angle = Mathf.Atan2(ballRotation.x, ballRotation.y) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }
}
