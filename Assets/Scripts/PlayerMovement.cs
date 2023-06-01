using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    internal float moveSpeed;
    public float defaultSpeed;

    public float groundDrag;

    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    bool readyToJump = true;

    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;

    [Header("Ground Check")]
    public float PlayerHeight;
    public LayerMask whatisGround;
    bool grounded;

    public Transform orientation;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;
    Rigidbody rb;

    [Header("Head Bob")]
    public Camera PlayerCam;
    private float defaultYPos = 0;
    public float headBobGap = 0.1f;
    public float headBobSpeed = 10f;
    private float timer;

    private void Start()
    {
        moveSpeed = defaultSpeed;
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }
    private void Update()
    {
        grounded = Physics.Raycast(transform.position, Vector3.down, PlayerHeight * .5f + .2f, whatisGround);
        MyInput();
        SpeedControl();
        HandleHeadBob();

        if (grounded)
            rb.drag = groundDrag;
        else
            rb.drag = 0;
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }
    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        if (Input.GetKey(jumpKey) && readyToJump && grounded)
        {
            readyToJump = false;
            Jump();
            Invoke(nameof(ResetJump), jumpCooldown);
        }
    }

    private void MovePlayer()
    {
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;
        if(grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
        else if(!grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
    }
    
    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        if (flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }
    private void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }
    private void ResetJump()
    {
        readyToJump = true;
    }
    private void HandleHeadBob()
    {
        if (!grounded) return;
        if(Mathf.Abs(moveDirection.x) > 0.1f || Mathf.Abs(moveDirection.z) > 0.1f)
        {
            timer += Time.deltaTime * headBobSpeed;
            PlayerCam.transform.localPosition = new Vector3(
                PlayerCam.transform.localPosition.x,
                defaultYPos + Mathf.Sin(timer) * headBobGap,
                PlayerCam.transform.localPosition.z
                );
        }
    }
}
