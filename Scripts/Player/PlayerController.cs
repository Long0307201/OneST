using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Walk,Run,Jump")]
    [SerializeField] private float WalkSpeed ;
    [SerializeField] private float RunSpeed;
    [SerializeField] private float JumpPower;
    private bool CanWalk =true;
    private bool isGrounded;
    private bool isRunning =false;
    private bool isJumping =false;
    private bool isFacingRight = true;
    private float Hozion;
    private Rigidbody2D rb;
    private Animator Anim;
    private void Awake() 
    {
        rb = GetComponent<Rigidbody2D>();
        Anim = GetComponent<Animator>();
    }
    private void Update() 
    {   
        if(CanWalk)
        {
            Walk();
        }
        if(Input.GetKeyDown(KeyCode.Space) && isGrounded && CanWalk)
        {
            Jump();
            isJumping = true;
        }
        Run();
        Flip();
        Anim.SetBool("Walk", Hozion !=0);
        Anim.SetBool("isGround",isGrounded);
        Anim.SetFloat("Jump",rb.velocity.y);
    }
    private void Flip()
    {
        if((isFacingRight && Hozion < 0 )|| (!isFacingRight && Hozion >0))
        {
            isFacingRight = !isFacingRight;
            Vector3 face = transform.localScale;
            face.x = -face.x;
            transform.localScale = face;
        }
    }
    private void Walk()
    {
        Hozion = Input.GetAxis("Horizontal");
        if (!isRunning)
        {
            rb.velocity = new Vector2(Hozion*WalkSpeed,rb.velocity.y);
        }
    }
    private void Run()
    {
        if(Input.GetKey(KeyCode.LeftShift) && Hozion !=0 && isGrounded)
        {
            rb.velocity = new Vector2(RunSpeed*Hozion,rb.velocity.y);
            isRunning = true;
        }
        else
        {
            isRunning = false;
        }
        Anim.SetBool("Run",isRunning);
    }
    private void Jump()
    {
        isGrounded = false;          
        rb.velocity = new Vector2(rb.velocity.x,JumpPower);

    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            isJumping = false;
        }
    }
    private void OnTriggerStay2D(Collider2D other) {
        if(other.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            isJumping = false;
        }
    }
}
