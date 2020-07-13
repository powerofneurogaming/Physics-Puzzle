using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Player controller based off of video 
 * "HOW TO MAKE A 2D CHARACTER CONTROLLER IN UNITY - EASY TUTORIAL"from:
 * https://www.youtube.com/watch?v=CeXAiaQOzmY
 */

public class Player : MonoBehaviour
{
    // Class Variables
    // public
    public float speed;
    // Private
    private Rigidbody2D _rb;
    private Vector2 moveVelocity;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // We continuous calculate the velocity of the character based on button inputs (up-down, left-right)
        // Use GetAxisRaw for sharper stopping
        Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        moveVelocity = moveInput * speed;
    }

    private void FixedUpdate()
    {
        _rb.MovePosition(_rb.position + moveVelocity * Time.fixedDeltaTime);

    }
}
