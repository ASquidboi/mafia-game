using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    float MoveSpeed = 5.0f;
    public Vector2 MovementInput;
    public float movementx;
    public float movementy;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        MovementInput.x = Input.GetKey(KeyCode.A);
        movementx = MovementInput.x;
        MovementInput.y = Input.GetAxis("Vertical");
        movementy = MovementInput.y;
        

        if (MovementInput.x == 0 && MovementInput.y == 0)
        {
            rb.velocity = Vector2.zero;
        }

        //MovementInput.Normalize();
        rb.velocity = MovementInput * MoveSpeed;

    }

    void FixedUpdate()
    {
        
    }
}
