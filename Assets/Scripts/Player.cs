using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] public int speed;
    [SerializeField] public int gravity;
    private bool gravityDown;
    private float distance = .2f;
    public bool canMove;

    private RaycastHit2D groundedR;
    private RaycastHit2D groundedL;

    private GameController gC;
    private Rigidbody2D rb;
    private Animator animator;
    private AudioSource aS;
    public AudioClip jump;
    public AudioClip death;

    void Start()
    {
        gC = GameObject.Find("GameController").GetComponent<GameController>();
        this.transform.position = gC.GetSpawnpoint().transform.position;
        rb = GetComponent<Rigidbody2D>();
        gravityDown = true;
        animator = GetComponent<Animator>();
        aS = GetComponent<AudioSource>();
        canMove = true;
    }

    void Update()
    {
        if(Input.GetButton("Horizontal") && canMove)
        {
            rb.velocityX = speed * Input.GetAxis("Horizontal");
            animator.SetBool("Walk", true);
        }
        else
        {
            rb.velocityX = 0;
            animator.SetBool("Walk", false);
        }
        
        if (canMove) {
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
                    aS.clip = jump;
                    aS.Play();
                }
            }
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

    IEnumerator Respawn()
    {
        animator.SetBool("Death", true);
        aS.clip = death;
        aS.Play();
        canMove = false;
        yield return new WaitForSeconds(2f);
        animator.SetBool("Death", false);
        transform.position = gC.GetSpawnpoint().transform.position;
        transform.rotation = gC.GetSpawnpoint().transform.rotation;
        gravityDown = transform.rotation.z == 0;
        gC.move = true;
        canMove = true;
    }

    private void OnBecameInvisible()
    {
        gC.MoveCamera(transform.position);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (groundedL.collider != null || groundedR.collider != null)
        {
            rb.velocityY = 0;
        }
        if (collision.gameObject.tag == "Spikes")
        {
            gC.move = false;
            rb.velocityY = 0;
            StartCoroutine(Respawn());
        }
        if (collision.gameObject.name.Contains("Platform"))
        {
            this.transform.SetParent(collision.transform, true);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.name.Contains("Platform"))
        {
            collision.transform.DetachChildren();
        }
    }
}
