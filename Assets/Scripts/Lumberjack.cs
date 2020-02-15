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
        float fire1 = Input.GetAxisRaw("Fire1");

        if (isStunned == false)
        {
            transform.position = transform.position + (Vector3.right * horizontalInput * 0.1f);

            bool isSwingingAxe = Input.GetButtonDown("Fire1");

            if (isSwingingAxe)
            {
                Debug.Log("swing axe");
            }
        }


    }

    void Hit()
    {
        isStunned = true;
    }
}
