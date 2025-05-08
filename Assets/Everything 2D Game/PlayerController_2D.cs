using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController_2D : MonoBehaviour
{
    private int speed = 10;

    Rigidbody2D playerRb2D;

    Vector2 movementInput;

    Vector2 velocity;


    // Start is called before the first frame update
    void Start()
    {
        playerRb2D = GetComponent<Rigidbody2D>();

        velocity = new Vector2(speed,speed);

    }

    // Update is called once per frame
    void Update()
    {
        PlayerInput();
    }

    private void FixedUpdate()
    {
        PlayerMove();
    }

    void PlayerInput()
    {
        movementInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }

    void PlayerMove()
    {
        Vector2 delta = movementInput * velocity * Time.deltaTime;

        Vector2 newPosition = playerRb2D.position + delta;
        
        playerRb2D.MovePosition(newPosition);
    }
}
