using System;
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
    [SerializeField] private bool _movementEnabled = false;
    [SerializeField] private bool _verticalMovementEnabled = false;

    private Rigidbody2D _rb;
    private Vector2 moveVelocity;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Player has started!");
        Debug.LogFormat("Object name of {0}", name);
        _rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // We continuous calculate the velocity of the character based on button inputs (up-down, left-right)
        // Use GetAxisRaw for sharper stopping
        if (_movementEnabled)
        {
            float verticalChange;
            if (_verticalMovementEnabled)
                verticalChange = Input.GetAxisRaw("Vertical");
            else
                verticalChange = Math.Min(0, Input.GetAxisRaw("Vertical")); // Make sure the player can't go upward
            Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), verticalChange);

            moveVelocity = moveInput * speed;
        }
        
    }

    private void FixedUpdate()
    {
        _rb.MovePosition(_rb.position + moveVelocity * Time.fixedDeltaTime);

    }
}
