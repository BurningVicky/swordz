using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 5f;
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;

    public float dashDistance = 5f;
    public float dashCooldown = 1f;
    public float dashDuration = 0.2f;

    private Rigidbody rb;
    private Vector3 moveInput;
    private bool isGrounded;

    private bool isDashing = false;
    private float dashTimer = 0f;
    private float dashCooldownTimer = 0f;
    private Vector3 dashDirection;

    public Transform cameraHolder;

    [SerializeField]
    private float extraGravity = 20f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Atualiza isGrounded
        isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckRadius, groundLayer);

        if (!isDashing)
        {
            float moveX = Input.GetAxis("Horizontal");
            float moveZ = Input.GetAxis("Vertical");

            moveInput = new Vector3(moveX, 0f, moveZ).normalized;
        }

        if (isGrounded && Input.GetButtonDown("Jump") && !isDashing)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && dashCooldownTimer <= 0f && !isDashing)
        {
            if (moveInput.magnitude > 0f)
            {
                StartDash();
            }
        }

        if (dashCooldownTimer > 0f)
            dashCooldownTimer -= Time.deltaTime;

        if (isDashing)
        {
            dashTimer -= Time.deltaTime;
            if (dashTimer <= 0f)
                EndDash();
        }

        // Sempre alinha o Player ao eixo Y da câmera
        Vector3 cameraForward = cameraHolder.forward;
        cameraForward.y = 0f;

        if (cameraForward != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(cameraForward);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
        }
    }

    void FixedUpdate()
    {
        if (!isDashing)
        {
            if (moveInput.magnitude > 0)
            {
                Vector3 moveDirection = cameraHolder.forward * moveInput.z + cameraHolder.right * moveInput.x;
                moveDirection.y = 0f;

                Vector3 force = moveDirection.normalized * moveSpeed;
                rb.AddForce(force, ForceMode.Force);
            }
        }
        else
        {
            Vector3 dashForce = dashDirection.normalized * (dashDistance / dashDuration);
            rb.AddForce(new Vector3(dashForce.x, 0, dashForce.z), ForceMode.Impulse);
        }

        ApplyExtraGravity();
    }

    void ApplyExtraGravity()
    {
        if (!isGrounded)
        {
            rb.AddForce(Vector3.down * extraGravity, ForceMode.Acceleration);
        }
    }

    void StartDash()
    {
        isDashing = true;
        dashTimer = dashDuration;
        dashCooldownTimer = dashCooldown;

        Vector3 moveDirection = cameraHolder.forward * moveInput.z + cameraHolder.right * moveInput.x;
        dashDirection = moveDirection.normalized;
    }

    void EndDash()
    {
        isDashing = false;
    }

    void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }
}
