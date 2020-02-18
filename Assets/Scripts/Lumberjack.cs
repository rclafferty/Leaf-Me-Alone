using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Lumberjack : Player
{
    bool isStunned = false;
    [SerializeField] GameObject touchingTree;
    bool isTouchingATree = false;
    const float STUN_DURATION = 3.0f;

    bool directionIsRight = false;

    [SerializeField] bool isSwingingAxe;

    Coroutine thisCoroutine;

    [SerializeField] Animator thisAnimator;

    const float MAX_DELAY = 1.0f;
    float inputDelay = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        if (thisAnimator == null)
            thisAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (inputDelay > 0.009f)
            inputDelay -= Time.deltaTime;

        if (SceneManager.GetActiveScene().name != "Title2" && !SceneManager.GetActiveScene().name.Contains("Credits"))
        {
            horizontalInput = Input.GetAxisRaw("Lumberjack Horizontal");
        }

        thisAnimator.SetFloat("Horizontal", horizontalInput);

        if (Mathf.Abs(horizontalInput) > 0.5f)
        {
            GetComponent<SpriteRenderer>().flipX = false;
            if (horizontalInput > 0f)
            {
                directionIsRight = true;
            }
            else
            {
                directionIsRight = false;
            }
        }

        if (isStunned == false)
        {
            MoveHorizontal();

            if (SceneManager.GetActiveScene().name != "Title2" && !SceneManager.GetActiveScene().name.Contains("Credits"))
                isSwingingAxe = Input.GetButtonDown("Swing Axe");

            if (isSwingingAxe && inputDelay < 0.01f)
            {
                if (directionIsRight)
                {
                    GetComponent<SpriteRenderer>().flipX = true;
                }
                else
                {
                    GetComponent<SpriteRenderer>().flipX = false;
                }

                SwingAxe();
            }
        }
    }

    public void MoveHorizontalInput(float hi)
    {
        horizontalInput = hi;

        if (Mathf.Abs(hi) > 0.5f)
        {
            GetComponent<SpriteRenderer>().flipX = false;
            if (horizontalInput > 0f)
            {
                directionIsRight = true;
            }
            else
            {
                directionIsRight = false;
            }
        }
    }

    public void Hit()
    {
        if (thisCoroutine != null)
            StopCoroutine(thisCoroutine);

        thisCoroutine = StartCoroutine(StunMovement(STUN_DURATION));
    }

    public void SwingAxe()
    {
        thisAnimator.SetTrigger("Swing Axe");
        // Debug.Log("swing axe");

        if (touchingTree != null)
        {
            touchingTree.GetComponent<Tree>().Hit();
        }

        inputDelay = MAX_DELAY;
    }

    IEnumerator StunMovement(float duration)
    {
        isStunned = true;
        GetComponent<Animator>().SetBool("Stun", true);

        yield return new WaitForSeconds(duration);

        isStunned = false;
        GetComponent<Animator>().SetBool("Stun", false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name.ToLower().Contains("tree"))
        {
            isTouchingATree = true;
            touchingTree = collision.gameObject;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.name.ToLower().Contains("tree"))
        {
            isTouchingATree = true;
            touchingTree = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.name.ToLower().Contains("tree"))
        {
            isTouchingATree = false;
            touchingTree = null;
        }
    }
}
