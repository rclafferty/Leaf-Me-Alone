using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    protected Rigidbody2D thisRigidbody;
    
    // Movement
    protected const float MOVEMENT_SPEED = 0.04f;
    protected float horizontalInput;

    // Start is called before the first frame update
    void Start()
    {
        thisRigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected void MoveHorizontal()
    {
        transform.position += Vector3.right * horizontalInput * MOVEMENT_SPEED;
    }
}
