using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class P_PlayerController : MonoBehaviour
{
    // Use for Singleton Pattern
    public static P_PlayerController playerControllerRef;

    private Rigidbody playerRb;

    private float HorizontalInput;
    private float VerticalInput;

    private float speed;
    [SerializeField] private float air_Speed;
    [SerializeField] private float baseSpeed;
    [SerializeField] private float maxSpeed;
  
    [SerializeField] private float jumpForce;

    private bool jumpKey;
    private bool sprintKey;

    [SerializeField] private bool isOnGround;

    private float ground_Drag = 9.0f;
    private float air_Drag = 0.0f;

    private float playerHeight;

    private StatusEffectHandler playerStatus;

    [SerializeField] private Animator playerAnim;


    void Awake()
    {
        if (playerControllerRef == null)
        {
            playerControllerRef = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        playerStatus = gameObject.GetComponent<StatusEffectHandler>();

        speed = maxSpeed;

        baseSpeed = maxSpeed - 100;

        playerRb = GetComponent<Rigidbody>();

        playerHeight = GetComponent<CapsuleCollider>().height;
    }

    // Update is called once per frame
    void Update()
    {

        if (!playerStatus.GetState("STUNNED") && !GameManager.gameManagerRef.GameOver)
        {
            PlayerInput();
        }


        PlayerWalk();

        PlayerJump();

        GroundCheck();
        DragCheck();
    }

    void FixedUpdate()
    {
        PlayerMovement();
    }



    void PlayerInput()
    {
        HorizontalInput = Input.GetAxisRaw("Horizontal");
        VerticalInput = Input.GetAxisRaw("Vertical");

        jumpKey = Input.GetKeyDown(KeyCode.Space);
        sprintKey = Input.GetKey(KeyCode.LeftShift);
    }



    void PlayerMovement()
    {
        Vector3 move = HorizontalInput * transform.right + VerticalInput * transform.forward;

        move = move.normalized;

        if (!isOnGround)
        {
            playerRb.AddForce(move * air_Speed, ForceMode.Force);
        }
        else
        {
            playerRb.AddForce(move * speed, ForceMode.Force);
           
            playerAnim.SetFloat("IsWalking", move.magnitude);
            
        }
    }
        

    void PlayerWalk()
    {
        if(sprintKey && (HorizontalInput != 0.0f || VerticalInput != 0.0f))
        {
            speed = baseSpeed;
        }
        else
        {
            speed = maxSpeed;
        }
   
    }

    void PlayerJump()
    {
        if(jumpKey && isOnGround)
        {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            playerAnim.SetTrigger("IsJumping");
        }
    }

    void GroundCheck()
    {
        float distance = playerHeight * 0.5f;
        float radius = 0.3f;
        RaycastHit hit;

        isOnGround = Physics.SphereCast(transform.position, radius, Vector3.down, out hit, distance, LayerMask.GetMask("Ground"));
    }

    void DragCheck()
    {
        if (playerStatus.GetState("STUNNED"))
        {
            playerRb.drag = 99;
        }
        else if (!isOnGround)
        {
            playerRb.drag = air_Drag;
        }
        else
        {
            playerRb.drag = ground_Drag;
        }
    }

}
