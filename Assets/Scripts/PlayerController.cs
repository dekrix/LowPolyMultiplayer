using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider))]
public class PlayerController : MonoBehaviour
{
    public CharacterController controller;
    public FixedJoystick joystick;
    public Animator animator;
    public float  gravity = -12f, groundDistance = 0.2f;
    public float playerSpeed = 10.0f;
    public int jumpForce = 6;

    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private float jumpHeight = 1.0f;
    private float gravityValue = -9.81f;

    private void Start()
    {
    }

    void Update()
    {
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        Vector3 move = new Vector3(joystick.Horizontal, 0, joystick.Vertical);
        controller.Move(move * Time.deltaTime * playerSpeed);

        if (move != Vector3.zero)
        {
            gameObject.transform.forward = move;
            animator.SetBool("Run", true);
        }
        else
        {
            animator.SetBool("Run", false);
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }

    public void Jump()
    {
        if (Physics.Raycast(transform.position, Vector3.down, 1 + 0.1f))
        {
            playerVelocity.y = Mathf.Sqrt(jumpForce * -2 * gravity);
            animator.SetTrigger("Jump");
        }
    }
    /*
    public Rigidbody rb;
    public FixedJoystick joystick;
    public Animator animator;
    public CharacterController characterController;
    public Transform groundCheck;
    public LayerMask groundMask;

    public float moveSpeed = 5, gravity = -9.81f, groundDistance = 0.2f, turnSmoothTime = 0.00001f;
    public int jumpForce = 5;

    Vector3 velocity;
    float horizontal, vertical, turnSmoothVelocity;
    public bool isGrounded;

    private void Update()
    {
        JoystickMovementAndMoveAnimations();
    }

    private void JoystickMovementAndMoveAnimations()
    {
        horizontal = -joystick.Horizontal;
        vertical = -joystick.Vertical;

        Vector3 move = transform.right * horizontal + transform.forward * vertical;
     
        characterController.Move(move * moveSpeed * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;

        characterController.Move(velocity * Time.deltaTime);
    }

    public void Jump()
    {
        if(Physics.Raycast(transform.position, Vector3.down, 1 + 0.1f))
        {
            velocity.y = Mathf.Sqrt(jumpForce * -2 * gravity);
            animator.SetTrigger("Jump");
        }
    }*/
}
