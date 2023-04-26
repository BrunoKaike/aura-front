using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotateSpeed = 10f;
    public float jumpForce = 5f;
    public float groundDistance = 0.2f;
    public LayerMask groundMask;
    public float ballSpeedMultiplier = 2f;
    private Animator animator;

    private Rigidbody ballRigidbody;
    private Collider ballCollider;
    private bool hasBall = false;
    private Rigidbody rb;
    private bool isGrounded;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        // Movimentação horizontal
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 moveDirection = new Vector3(horizontal, 0f, vertical).normalized;

        // Rotação do jogador
        if (moveDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);
        }

        // Pulo
        isGrounded = Physics.Raycast(transform.position, -Vector3.up, groundDistance, groundMask);
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }

        Vector3 moveVelocity = moveDirection * moveSpeed * Time.deltaTime;
        rb.MovePosition(rb.position + moveVelocity);

        if (moveVelocity.magnitude > 0)
        {
            animator.SetFloat("Speed", 1f);
        }
        else
        {
            animator.SetFloat("Speed", 0f);
            
        }

        if(!hasBall){

            animator.SetBool("isPassing", false);

        }

        if (hasBall && Input.GetKeyDown(KeyCode.C))
        {
            
            animator.SetBool("isPassing", true);
            //Debug.Log("Passando");
            ballRigidbody.useGravity = true;
            ballRigidbody.isKinematic = false;

            transform.Find("SoccerBall").parent = null;
            ballCollider.gameObject.transform.SetParent(null);
            ballRigidbody.AddForce(transform.forward * 10f, ForceMode.Impulse);
            ballCollider.enabled = true;
            hasBall = false;
            
        }
    }


    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Ball") && !hasBall)
        {
            ballCollider = other;
            ballRigidbody = other.gameObject.GetComponent<Rigidbody>();
            ballRigidbody.useGravity = false;
            ballRigidbody.isKinematic = true;
            ballCollider.enabled = false;
            ballCollider.gameObject.transform.SetParent(transform);
            hasBall = true;
        }
    }
}
