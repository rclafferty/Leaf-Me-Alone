using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Climber : MonoBehaviour
{
    Rigidbody2D thisRigidbody;
    [SerializeField] bool isClimbing;
    [SerializeField] bool isGrounded;
    Vector3 jumpForce;

    const float MOVEMENT_SPEED = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        thisRigidbody = GetComponent<Rigidbody2D>();
        isClimbing = false;
        isGrounded = true;
        jumpForce = new Vector3(0, 6.1f, 0);
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical"); // May or may not use this
        bool fire1 = Input.GetButtonDown("Fire1");

        if (fire1 && isGrounded)
        {
            Jump();
        }

        transform.position = transform.position + (Vector3.right * horizontalInput * 0.1f);
        if (isClimbing)
        {
            transform.position = transform.position + (Vector3.up * verticalInput * 0.1f);
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Tree")
        {
            SetCling(0);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Tree")
        {
            SetCling(0);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Tree")
        {
            SetCling(1);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Ground" || collision.gameObject.tag == "Branch")
        {
            isGrounded = true;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Ground" || collision.gameObject.tag == "Branch")
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Ground" || collision.gameObject.tag == "Branch")
        {
            isGrounded = false;
        }
    }

    void SetCling(float gravityScale)
    {
        if (gravityScale < 0.01f)
        {
            gravityScale = 0;
            isClimbing = false;
        }
        else
        {
            thisRigidbody.gravityScale = gravityScale;
            isClimbing = true;
        }
    }
}
