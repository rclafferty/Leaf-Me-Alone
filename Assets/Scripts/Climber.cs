using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Climber : MonoBehaviour
{
    bool isStunned = false;
    Rigidbody2D thisRigidbody;

    // Start is called before the first frame update
    void Start()
    {
        thisRigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical"); // May or may not use this
        float fire1 = Input.GetAxisRaw("Fire1");

        transform.position = transform.position + (Vector3.right * horizontalInput * 0.1f);
        transform.position = transform.position + (Vector3.up * verticalInput * 0.1f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Tree")
        {
            thisRigidbody.gravityScale = 0;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Tree")
        {
            thisRigidbody.gravityScale = 0;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Tree")
        {
            thisRigidbody.gravityScale = 1;
        }
    }
}
