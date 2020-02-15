using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lumberjack : MonoBehaviour
{
    bool isStunned = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical"); // May or may not use this

        if (isStunned = false)
        {
            transform.position = transform.position + (Vector3.right * horizontalInput * 0.1f);
            transform.position = transform.position + (Vector3.up * verticalInput * 0.1f);
        }
    }

    void Hit()
    {
        isStunned = true;
    }
}
