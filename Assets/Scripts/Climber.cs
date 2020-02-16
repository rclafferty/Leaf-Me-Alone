using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Climber : Player
{
    [SerializeField] GameObject thrownApplePrefab;
    
    [SerializeField] bool isGrounded;
    Vector3 jumpForce;
    int health;

    int apples;
    
    // Start is called before the first frame update
    void Start()
    {
        health = 10;
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
        bool eatApple = Input.GetButtonDown("Eat Apple");
        bool throwApple = Input.GetButtonDown("Throw Apple");

        if (jump && isGrounded)
        {
            Jump();
        }

        MoveHorizontal();
        
        if (eatApple)
        {
            EatApple();
        }

        if (throwApple)
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

    void EatApple()
    {
        if (apples <= 0)
            return;

        if (health < 10)
        {
            // Add 2 but no more than 10 total to health
            health = Mathf.Min(10, health + 2);
            apples--;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Apple")
        {
            if (collision.name == "Golden Apple")
            {
                // Next level
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
            else
            {
                apples++;
            }
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
