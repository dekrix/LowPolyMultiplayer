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
    private float jumpHeight = 50.0f;
    private float gravityValue = -9.81f;
    float ySpeed;

    private void Start()
    {
    }

    private void Update()
    {
        float horizontal = joystick.Horizontal;
        float vertical = joystick.Vertical;

        Vector3 movementDirection = new Vector3(horizontal, 0 , vertical);
        float magnitude = Mathf.Clamp01(movementDirection.magnitude) * playerSpeed;
        movementDirection.Normalize();

        ySpeed += Physics.gravity.y * Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            ySpeed = jumpForce;
        }

        Vector3 velocity = movementDirection * magnitude;
        velocity.y = ySpeed;

        controller.Move(velocity * Time.deltaTime);

        if (movementDirection != Vector3.zero)
        {
            gameObject.transform.forward = movementDirection;
            animator.SetBool("Run", true);
        }
        else
        {
            animator.SetBool("Run", false);
        }
    }
    public void Jump()
    {
        ySpeed = jumpForce;
        animator.SetTrigger("Jump");
    }
}
