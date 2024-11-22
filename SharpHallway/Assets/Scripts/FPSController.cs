using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class FPSController : MonoBehaviour
{
    public Camera playerCamera;
    public float walkSpeed = 5f;    //6f
    public float runSpeed = 7f;     //12f
    public float jumpPower = 7f;
    public float gravity = 10f;

    #region Footstep Variables
    float currentSpeed; // variable to calculate the current speed
    public bool canMove = true;
    public bool footsteps = true;
    private bool hittingWall = false;
    public float stepInterval = 0.1f; // a variable to determine interval between steps
    public float stepTimer = 0f;    // tracks how much time passed since last footstep sound
    float inputThreshold = 0.1f; // Define a small threshold so that footsteps stop when player stops, no delay
    private bool isWalking = false;
    private bool isRunning = false;
    float stepSlow = 1.8f;  // variable to slow footsteps down 

    #endregion


    public float lookSpeed = 2f;
    public float lookXLimit = 45f;

    #region WWise Events and such

    [Header("WWise Events")]
    [SerializeField] public AK.Wwise.Event walkFootstep;
    [SerializeField] public AK.Wwise.Event stomp;
  

    #endregion

    Vector3 moveDirection = Vector3.zero;
    float rotationX = 0;

    


    CharacterController characterController;
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {

        #region Handles Movment
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        // Press Left Shift to run
        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        float curSpeedX = canMove ? (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Vertical") : 0;
        float curSpeedY = canMove ? (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Horizontal") : 0;
        float movementDirectionY = moveDirection.y;
        moveDirection = (forward * curSpeedX) + (right * curSpeedY);

        #endregion

        #region Handles Jumping
        if (Input.GetButton("Jump") && canMove && characterController.isGrounded)
        {
            moveDirection.y = jumpPower;
        }
        else
        {
            moveDirection.y = movementDirectionY;
        }

        if (!characterController.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }

        #endregion

        #region Handles Rotation
        characterController.Move(moveDirection * Time.deltaTime);

        if (canMove)
        {
            rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
        }

        #endregion

        #region Handles Footsteps

        if (!hittingWall)
        {
            if (Input.GetKey(KeyCode.LeftShift)) // Assuming LeftShift is the run key
            {
                currentSpeed = runSpeed;
            }
            else
            {
                currentSpeed = walkSpeed;
            }

            // Where we change the time between footsteps, increases if running
            float stepInterval2 = stepInterval * (walkSpeed / currentSpeed) * stepSlow;

            
            // check if input from player
            // vertical = W , S
            // horizontal = A , D

            if (Mathf.Abs(Input.GetAxis("Vertical")) > inputThreshold || Mathf.Abs(Input.GetAxis("Horizontal")) > inputThreshold)
            {
                stepTimer += Time.deltaTime;
                if (stepTimer >= stepInterval2 && footsteps == true)
                {
                    walkFootstep.Post(gameObject);
                    stepTimer = 0;
                }
            }
            else
            {
                stepTimer = 0;
            }
        }
        #endregion

        #region Handles Stomp

        if (isWalking == false && isRunning == false && Input.GetKeyDown(KeyCode.Q))
        {
            stomp.Post(gameObject);
        }

        #endregion
    }


}