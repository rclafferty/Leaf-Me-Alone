using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Climber : Player
{
    [SerializeField] GameObject thrownApplePrefab;
    
    [SerializeField] bool isGrounded;
    [SerializeField] bool isFalling;
    Vector3 jumpLocation;
    Vector3 jumpForce;
    [SerializeField] int health;
    const int MAX_HEALTH = 10;

    bool isStunned = false;

    [SerializeField] Sprite jumpingSprite;
    [SerializeField] Sprite walkingSprite;

    [SerializeField] Image healthBarImage;
    float healthBarImageWidth;

    [SerializeField] Text applesText;

    [SerializeField] GameObject EndUI;
    
    int apples;
    
    // Start is called before the first frame update
    void Start()
    {
        EndUI.SetActive(false);

        health = MAX_HEALTH;
        apples = 0;

        thisRigidbody = GetComponent<Rigidbody2D>();
        isGrounded = true;
        isFalling = false;
        jumpForce = new Vector3(0, 5.5f, 0);

        healthBarImageWidth = healthBarImage.rectTransform.rect.width;
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxisRaw("Climber Horizontal");
        bool jump = Input.GetButtonDown("Jump");
        bool eatApple = Input.GetButtonDown("Eat Apple");
        bool throwApple = Input.GetButtonDown("Throw Apple");

        if (!isStunned)
        {
            if (jump && isGrounded)
            {
                Jump();
            }

            MoveHorizontal();

            if (horizontalInput > 0.01f)
            {
                GetComponent<SpriteRenderer>().flipX = false;
            }
            else
            {
                GetComponent<SpriteRenderer>().flipX = true;
            }

            if (eatApple)
            {
                EatApple();
            }

            if (throwApple)
            {
                ThrowApple();
            }
        }
    }

    void Jump()
    {
        Debug.Log("Jumping");
        GetComponent<SpriteRenderer>().sprite = jumpingSprite;
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

        DisplayApples();
    }

    void EatApple()
    {
        if (apples <= 0)
            return;

        if (health < MAX_HEALTH)
        {
            // Add 2 but no more than 10 total to health
            health = Mathf.Min(MAX_HEALTH, health + 2);
            apples--;
        }

        DisplayHealth();
        DisplayApples();
    }

    void DisplayApples()
    {
        applesText.text = "Apples: " + apples;
    }

    void DisplayHealth()
    {
        float percentage = (float)health / (float)MAX_HEALTH;

        /* RectTransform rectTransform = healthBarImage.rectTransform;
        Vector3 scale = rectTransform.localScale;
        scale.x = percentage;
        rectTransform.localScale = scale;
        healthBarImage.rectTransform = scale; */

        healthBarImage.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, percentage * healthBarImageWidth);
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

                DisplayApples();
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

        if (collision.gameObject.tag == "Ground" && isFalling)
        {
            TakeDamage();
        }

        isFalling = false;
    }

    private void TakeDamage()
    {
        Vector3 locationDifference = jumpLocation - transform.position;
        const float damageThreshold = 3;
        if (locationDifference.y > damageThreshold)
        {
            int damage = (int)((MAX_HEALTH / 3) + Mathf.Floor(locationDifference.y - damageThreshold));
            health -= damage;
        }

        Debug.Log("Health: " + health);
        if (health <= 0)
        {
            Debug.Log("Stunned!");
            GetComponent<Animator>().SetTrigger("Stun");
            isStunned = true;
            EndUI.SetActive(true);
        }

        DisplayHealth();
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (CheckStandingCollision(collision.gameObject.tag))
        {
            isGrounded = true;
            GetComponent<SpriteRenderer>().sprite = walkingSprite;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (CheckStandingCollision(collision.gameObject.tag))
        {
            isGrounded = false;
            GetComponent<SpriteRenderer>().sprite = jumpingSprite;
        }

        if (collision.gameObject.tag == "Branch")
        {
            isFalling = true;
            jumpLocation = transform.position;
            GetComponent<SpriteRenderer>().sprite = jumpingSprite;
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
