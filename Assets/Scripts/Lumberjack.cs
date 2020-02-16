using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lumberjack : Player
{
    bool isStunned = false;
    [SerializeField] GameObject touchingTree;
    bool isTouchingATree = false;
    const float STUN_DURATION = 3.0f;

    bool directionIsRight = false;

    [SerializeField] bool isSwingingAxe;

    Coroutine thisCoroutine;

    Animator thisAnimator;

    // Start is called before the first frame update
    void Start()
    {
        thisAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxisRaw("Lumberjack Horizontal");

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
            isSwingingAxe = Input.GetButtonDown("Swing Axe");
            if (isSwingingAxe)
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

    public void Hit()
    {
        if (thisCoroutine != null)
            StopCoroutine(thisCoroutine);

        thisCoroutine = StartCoroutine(StunMovement(STUN_DURATION));
    }

    void SwingAxe()
    {
        thisAnimator.SetTrigger("Swing Axe");
        Debug.Log("swing axe");

        if (touchingTree != null)
        {
            touchingTree.GetComponent<Tree>().Hit();
        }
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
