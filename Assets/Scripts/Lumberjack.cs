using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lumberjack : Player
{
    bool isStunned = false;
    const float STUN_DURATION = 3.0f;

    [SerializeField] bool isSwingingAxe;

    Coroutine thisCoroutine;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxisRaw("Lumberjack Horizontal");

        if (isStunned == false)
        {
            MoveHorizontal();
            isSwingingAxe = Input.GetButtonDown("Swing Axe");
            if (isSwingingAxe)
            {
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
        Debug.Log("swing axe");
    }

    IEnumerator StunMovement(float duration)
    {
        isStunned = true;

        yield return new WaitForSeconds(duration);

        isStunned = false;
    }
}
