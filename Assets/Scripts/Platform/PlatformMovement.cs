using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMovement : MonoBehaviour
{

    [Header("Platform Stats")]
    [SerializeField] public float baseSpeed = 5f;

    private Rigidbody rb;
    private Camera mainCam;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        mainCam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = transform.position.z - mainCam.transform.position.z;

        // Convert mouse position to world coordinates
        Vector3 worldMousePos = mainCam.ScreenToWorldPoint(mousePos);

        // Update platform's position only on the x-axis
        rb.MovePosition(new Vector3(worldMousePos.x, transform.position.y, transform.position.z));
    }
}
