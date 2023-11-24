using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] public int speed;
    [SerializeField] public int gravity;
    private bool gravityDown;
    private float distance = .2f;

    private RaycastHit2D groundedR;
    private RaycastHit2D groundedL;
    private Rigidbody2D rb;
    private CameraController camController;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        camController = Camera.main.GetComponent<CameraController>();
        gravityDown = true;
    }

    void Update()
    {
        if(Input.GetButton("Horizontal"))
        {
            rb.velocityX = speed * Input.GetAxis("Horizontal");
        }
        else
        {
            rb.velocityX = 0;
        }
        
        float groundCheckY = transform.position.y + (transform.localScale.y + .01f) * (gravityDown ? -1 : 1);
        Vector2 left = new Vector2(transform.position.x - (transform.localScale.x / 2.25f), groundCheckY);
        Vector2 right = new Vector2(transform.position.x + (transform.localScale.x / 2.25f), groundCheckY);
        Vector2 directionY = new Vector2(0, (gravityDown ? -1 : 1));
        groundedR = Physics2D.Raycast(right, directionY, distance);
        // Debug.DrawRay(right, directionY * distance, groundedR.collider != null ? Color.red : Color.green);
        groundedL = Physics2D.Raycast(left, directionY, distance);
        // Debug.DrawRay(left, directionY * distance, groundedL.collider != null ? Color.red : Color.green);

        if (groundedL.collider != null || groundedR.collider != null)
        {
            if (Input.GetButtonDown("Jump"))
            {
                gravityDown = !gravityDown;
            }
        }
        else
        {
            if (gravityDown)
            {
                rb.velocityY = -gravity;
                transform.rotation = Quaternion.identity;
            }
            else
            {
                rb.velocityY = gravity;
                transform.rotation = Quaternion.Euler(0, 0, 180);
            }
        }
    }

    private void OnBecameInvisible()
    {
        camController.MoveCamera(transform.position);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (groundedL.collider != null || groundedR.collider != null)
            rb.velocityY = 0;
    }
}
