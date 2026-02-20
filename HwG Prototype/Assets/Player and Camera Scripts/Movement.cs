using UnityEngine;

public class Movement : MonoBehaviour
{

    private float moveSpeed;
    public float walkSpeed;
    public float sprintSpeed;


    [Header("GroundCheck")]
    public float playerHeight;
    public LayerMask whatisGround;
    public bool Grounded;
    public float GroundDrag;

    [Header("jumping")]
    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    public bool readyToJump;

    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode sprintKey = KeyCode.LeftShift;
    public KeyCode crouchKey = KeyCode.LeftControl;

    [Header("Crouching")]
    public float crouchSpeed;
    public float crouchYScale;
    private float startYScale;
    
    public Transform orientation;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;

    Rigidbody rb;

    public MovementState state;
    // This enum defines the movement states in a way that other scripts can access. Useful for sound effects and animations.
    public enum MovementState
    {
        walking,
        sprinting,
        air,
        crouching
    }

   private void StateHandler()
    {
        // This is how we change movement speed depending on state.
        if( Grounded && Input.GetKey(sprintKey) && state != MovementState.crouching)
        {
            state = MovementState.sprinting;
            moveSpeed = sprintSpeed;
        }

        else if (Input.GetKey(crouchKey))
        {
            state = MovementState.crouching;
            moveSpeed = crouchSpeed;
        }
        else if (Grounded == true)
        {
            state = MovementState.walking;
            moveSpeed = walkSpeed;
        }

        else
        {
            state = MovementState.air;
        }
        
    }

    

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        readyToJump = true;

        startYScale = transform.localScale.y;
    }

    private void Update()
    {
        MyInput();
        SpeedControl(); 
        StateHandler();
        // GROUND CHECK
        Grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * .5f + .2f, whatisGround);
        // air resistance
        if (Grounded == true)

            rb.angularDrag = GroundDrag;
        else
            rb.angularDrag = 0;


    }
    private void FixedUpdate()
    {
        MovePlayer();
        
    }
    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        // Jumping input

        if(Input.GetKey(jumpKey) &&readyToJump && Grounded)
        {
            readyToJump = false;

            Jump();

            Invoke(nameof(ResetJump), jumpCooldown);
        }
        // crouching input

        if (Input.GetKeyDown(crouchKey))
        {
            transform.localScale = new Vector3(transform.localScale.x, crouchYScale, transform.localScale.z);
            rb.AddForce(Vector3.down *5f,ForceMode.Impulse);
        }

        if (Input.GetKeyUp(crouchKey))
        {
            transform.localScale = new Vector3(transform.localScale.x, startYScale, transform.localScale.z);
        }
    }

    private void MovePlayer()
    {
        //This is how we calculate movement direction based on player input and orientation.
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        // This is how we apply movement force. We use different multipliers for air and ground to make the player feel more responsive on the ground and less so in the air.
        if (Grounded == true)

            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
        else if (Grounded != true)

            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
    }

    private void SpeedControl()
    {
        //this makes it so you don't fly away
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        if (flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }
    // This is how we make the player jump. We reset the y velocity to 0 before applying the jump force to make the jump more consistent.
    private void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);

    }
    private void ResetJump()
    {
        readyToJump = true;
    }
}
