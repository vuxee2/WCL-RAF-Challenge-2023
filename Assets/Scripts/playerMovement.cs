using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class playerMovement : MonoBehaviour
{
    [Header("Movement")]
    public static float moveSpeed;
    public float walkSpeed;
    public float sprintSpeed;
    public float slideSpeed;

    private float desiredMoveSpeed;
    private float lastDesiredMoveSpeed;

    public float speedIncreaseMultiplier;
    public float slopeIncreaseMultiplier;

    public float groundDrag;

    [Header("Jumping")]
    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    bool readyToJump;

    [Header("Crouching")]
    public float crouchSpeed;

    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode sprintKey = KeyCode.LeftShift;
    public KeyCode crouchKey = KeyCode.LeftControl;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatIsGround;
    bool grounded;

    [Header("Slope Handling")]
    public float maxSlopeAngle;
    private RaycastHit slopeHit;
    private bool exitingSlope;
    

    public Transform orientation;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;

    Rigidbody rb;

    public MovementState state;
    public enum MovementState
    {
        walking,
        sprinting,
        crouching,
        sliding,
        air
    }

    public bool sliding;

    PhotonView view;
    public GameObject kamera;

    //ANIMATIONS
    public Animator animator;
    public Transform character;
    public Transform characterPos;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        readyToJump = true;

        view = GetComponent<PhotonView>();
        

        if(!view.IsMine)
        {
            Destroy(kamera);
        }
    }

    private void Update()
    {
        if(view.IsMine)
        {
            grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);

            MyInput();
            SpeedControl();
            StateHandler();

            if (grounded)
                rb.drag = groundDrag;
            else
                rb.drag = 0;
        }
        AnimationsPlayer();
        
    }

    private void FixedUpdate()
    {
        if(view.IsMine)
        {
            MovePlayer();
        }
    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        if(Input.GetKey(jumpKey) && readyToJump && grounded)
        {
            readyToJump = false;

            Jump();

            Invoke(nameof(ResetJump), jumpCooldown);

            character.position = characterPos.position;
        }

        if (Input.GetKeyDown(crouchKey))
        {
            var capsuleCollider = GetComponent ("CapsuleCollider") as CapsuleCollider;
            capsuleCollider.height = 1f;
            capsuleCollider.center = new Vector3(0f, -0.5f, 0f);
            rb.AddForce(Vector3.down * 5f, ForceMode.Impulse);
        }

        if (Input.GetKeyUp(crouchKey))
        {
            var capsuleCollider = GetComponent ("CapsuleCollider") as CapsuleCollider;
            capsuleCollider.height = 2f;
            capsuleCollider.center = new Vector3(0f, 0f, 0f);
        }
    }

    private void StateHandler()
    {
        if (sliding)
        {
            state = MovementState.sliding;

            if (OnSlope() && rb.velocity.y < 0.1f)
                desiredMoveSpeed = slideSpeed;

            else
                desiredMoveSpeed = sprintSpeed;
        }

        else if (Input.GetKey(crouchKey))
        {
            state = MovementState.crouching;
            desiredMoveSpeed = crouchSpeed;
        }

        else if(grounded && Input.GetKey(sprintKey))
        {
            state = MovementState.sprinting;
            desiredMoveSpeed = sprintSpeed;
        }

        else if (grounded)
        {
            state = MovementState.walking;
            desiredMoveSpeed = walkSpeed;
        }

        else
        {
            state = MovementState.air;
        }


        if(Mathf.Abs(desiredMoveSpeed - lastDesiredMoveSpeed) > 4f && moveSpeed != 0)
        {
            StopAllCoroutines();
            StartCoroutine(SmoothlyLerpMoveSpeed());
        }
        else
        {
            moveSpeed = desiredMoveSpeed;
        }

        lastDesiredMoveSpeed = desiredMoveSpeed;
    }

    private IEnumerator SmoothlyLerpMoveSpeed()
    {
        float time = 0;
        float difference = Mathf.Abs(desiredMoveSpeed - moveSpeed);
        float startValue = moveSpeed;

        while (time < difference)
        {
            moveSpeed = Mathf.Lerp(startValue, desiredMoveSpeed, time / difference);

            if (OnSlope())
            {
                float slopeAngle = Vector3.Angle(Vector3.up, slopeHit.normal);
                float slopeAngleIncrease = 1 + (slopeAngle / 90f);

                time += Time.deltaTime * speedIncreaseMultiplier * slopeIncreaseMultiplier * slopeAngleIncrease;
            }
            else
                time += Time.deltaTime * speedIncreaseMultiplier;

            yield return null;
        }

        moveSpeed = desiredMoveSpeed;
    }

    private void MovePlayer()
    {
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        if (OnSlope() && !exitingSlope)
        {
            rb.AddForce(GetSlopeMoveDirection(moveDirection) * moveSpeed * 20f, ForceMode.Force);

            if (rb.velocity.y > 0)
                rb.AddForce(Vector3.down * 80f, ForceMode.Force);
        }

        else if(grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);

        else if(!grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);

        rb.useGravity = !OnSlope();
    }

    private void SpeedControl()
    {
        if (OnSlope() && !exitingSlope)
        {
            if (rb.velocity.magnitude > moveSpeed)
                rb.velocity = rb.velocity.normalized * moveSpeed;
        }

        else
        {
            Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

            if (flatVel.magnitude > moveSpeed)
            {
                Vector3 limitedVel = flatVel.normalized * moveSpeed;
                rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
            }
        }
    }

    private void Jump()
    {
        exitingSlope = true;

        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }
    private void ResetJump()
    {
        readyToJump = true;

        exitingSlope = false;
    }

    public bool OnSlope()
    {
        if(Physics.Raycast(transform.position, Vector3.down, out slopeHit, playerHeight * 0.5f + 0.3f))
        {
            float angle = Vector3.Angle(Vector3.up, slopeHit.normal);
            return angle < maxSlopeAngle && angle != 0;
        }

        return false;
    }

    public Vector3 GetSlopeMoveDirection(Vector3 direction)
    {
        return Vector3.ProjectOnPlane(direction, slopeHit.normal).normalized;
    }

    private void AnimationsPlayer()
    {
        
        if(Input.GetKeyDown("1") && moveDirection == Vector3.zero)
        {
            animator.SetBool("isDancing", true);
            playerCam.isDancing = true;
        }
        else if(Input.GetKeyUp("1") || moveDirection != Vector3.zero)
        {
            animator.SetBool("isDancing", false);
            playerCam.isDancing = false;
        }
        if(Input.GetKeyDown("2") && moveDirection == Vector3.zero)
        {
            animator.SetBool("isDancing2", true);
            playerCam.isDancing = true;
        }
        else if(Input.GetKeyUp("2") || moveDirection != Vector3.zero)
        {
            animator.SetBool("isDancing2", false);
            playerCam.isDancing = false;
        }
        if(moveDirection == Vector3.zero)
        {
            animator.SetBool("isWalking", false);
            animator.SetBool("isRunning", false);
        }
        else if(state == MovementState.walking)
        {
            if(verticalInput>0)
                animator.SetFloat("Speed",1.0f);
            else
                animator.SetFloat("Speed",-1.0f);

            animator.SetBool("isWalking", true);
            animator.SetBool("isRunning", false);
            character.position = characterPos.position;
        }
        else if(state == MovementState.sprinting )
        {
            if(verticalInput>0)
                animator.SetFloat("Speed",1.0f);
            else
                animator.SetFloat("Speed",-1.0f);

            animator.SetBool("isRunning", true);
            character.position = characterPos.position;
        }
        if(state == MovementState.sliding)
        {
            animator.SetBool("isSliding", true);
            character.position = characterPos.position;
        }
        else animator.SetBool("isSliding", false);
        
        if(grounded)
        {
            animator.SetBool("isJumping", false);
            
        }
        else animator.SetBool("isJumping", true);

        if(state == MovementState.crouching && moveDirection == Vector3.zero)
        {
            animator.SetBool("isCrouching", true);
        }
        else animator.SetBool("isCrouching", false);

        if(state == MovementState.crouching && moveDirection != Vector3.zero)
        {
            if(verticalInput>0)
                animator.SetFloat("Speed",1.0f);
            else
                animator.SetFloat("Speed",-1.0f);

            animator.SetBool("isCrouchWalking",true);
        }
        else animator.SetBool("isCrouchWalking",false);
    }
}
