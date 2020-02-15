using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Apple : MonoBehaviour
{
    const float MOVEMENT_SPEED = 3f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Ground")
        {
            Destroy(gameObject);
        }
        else if (collision.gameObject.name == "Lumberjack")
        {
            collision.gameObject.GetComponent<Lumberjack>().Hit();
            Destroy(gameObject);
        }
    }
}
