using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Climber : Player
{
    [SerializeField] GameObject thrownApplePrefab;
    
    [SerializeField] bool isGrounded;
    Vector3 jumpForce;

    int apples;
    
    // Start is called before the first frame update
    void Start()
    {
        apples = 0;

        thisRigidbody = GetComponent<Rigidbody2D>();
        isGrounded = true;
        jumpForce = new Vector3(0, 5.5f, 0);
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxisRaw("Climber Horizontal");
        bool jump = Input.GetButtonDown("Jump");

        if (jump && isGrounded)
        {
            Jump();
        }

        MoveHorizontal();
        
        if (Input.GetButtonDown("Throw Apple"))
        {
            ThrowApple();
        }
    }

    void Jump()
    {
        Debug.Log("Jumping");
        thisRigidbody.AddForce(jumpForce, ForceMode2D.Impulse);
    }

    void Climb(Vector3 climbingDirection)
    {
        transform.position += climbingDirection * MOVEMENT_SPEED;
    }

    void ThrowApple()
    {
        if (apples > 0)
        {
            apples--;
            Instantiate(thrownApplePrefab, transform.position, Quaternion.identity);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Apple")
        {
            apples++;
            Destroy(collision.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (CheckStandingCollision(collision.gameObject.tag))
        {
            isGrounded = true;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (CheckStandingCollision(collision.gameObject.tag))
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (CheckStandingCollision(collision.gameObject.tag))
        {
            isGrounded = false;
        }
    }

    bool CheckStandingCollision(string tag)
    {
        string[] validStandingCollisions = { "Ground", "Branch" };
        foreach (string vsc in validStandingCollisions)
        {
            if (tag == vsc)
            {
                return true;
            }
        }

        return false;
    }
}
