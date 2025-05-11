using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController_2D : MonoBehaviour
{
    public string targetSceneName = "3D_GameScene";
    [SerializeField] private int speed = 20;

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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Bridge"))
        {
            SceneManager.LoadScene(targetSceneName);
        }
    }
}
