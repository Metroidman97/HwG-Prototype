using UnityEngine;
using static Unity.VisualScripting.Member;

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

    [Header("Controller")]
    

    [Header("Crouching")]
    public float crouchSpeed;
    public float crouchYScale;
    private float startYScale;

    [Header("Sound")]
    public AudioSource footstepSound;
    public AudioClip walkSound;
    public AudioClip sprintSound;
    public AudioClip crouchSound;

    public Transform orientation;

    float horizontalInput;
    float verticalInput;

    public bool stickSprint;
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
        if (Grounded && Input.GetKey(sprintKey) && state != MovementState.crouching || stickSprint == true)
        {
            state = MovementState.sprinting;
            moveSpeed = sprintSpeed;
            footstepSound.clip = sprintSound;

        }

        else if (Input.GetKey(crouchKey) || Input.GetKey(KeyCode.JoystickButton1))
        {
            state = MovementState.crouching;
            moveSpeed = crouchSpeed;
            footstepSound.clip = crouchSound;

        }
        else if (Grounded == true)
        {
            state = MovementState.walking;
            moveSpeed = walkSpeed;
            footstepSound.clip = walkSound;

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
        footstepSound.clip = walkSound;

        startYScale = transform.localScale.y;
    }

    private void Update()
    {
        
        SpeedControl();
        StateHandler();
        // GROUND CHECK
        Grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * .5f + .2f, whatisGround);
        // air resistance
        if (Grounded == true)

            rb.drag = GroundDrag;
        else
            rb.drag = 1f;


        //This is how we handle footstep sounds.
        if (rb.velocity.magnitude > .1f && footstepSound.isPlaying != true)
        {
            footstepSound.time = Random.Range(0f, footstepSound.clip.length);
            footstepSound.Play();
        }
        if (rb.velocity.magnitude < .1f && footstepSound.isPlaying)
        {
            footstepSound.Stop();
        }



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
