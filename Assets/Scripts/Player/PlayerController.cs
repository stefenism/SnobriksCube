using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Animator))]
public class PlayerController : MonoBehaviour
{

    private Rigidbody2D rb;
    private Animator anim;
    private BoxCollider2D boxCollider;

    private PlayerStateManager playerState;
    private PlayerHealth playerHealth;

    public bool grounded;
    private bool facingRight = true;

    private bool jumping = false;
    private bool jumpAllowed = true;
    private bool canJump = true;

    //jump variables
    public float jumpForce;
    private float jumpDuration = 0;
    public float jumpTime = .3f;
    [Range(0.1f, 1f)]
    public float drag = 1;
    public float gravityDropModifier = 2;
    private float maxFallSpeed = -10f;
    public float normalFallSpeed = -10f;
    public float quickFallSpeed = -15f;

    private float horMov;
    public float speed;
    public float maxSpeed;
    [Range(0.1f, 1f)]
    public float accelerationFactor;
    [Range(0.1f, 1f)]
    public float aircellerationFactor;
    [Range(0.1f, 1f)]
    public float decel;
    [Range(0.1f, 1f)]
    public float airDecel;
    [Range(0.1f, 1f)]
    public float autoBrakeFactor;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();

        playerState = GetComponent<PlayerStateManager>();
        playerHealth = GetComponent<PlayerHealth>();
    }

    void Update()
    {
        if(playerState.playerIsControllable())
            CheckInput();
    }

    void FixedUpdate()
    {
        Run(horMov);

        if(jumping)
            Jump();

        if(!grounded && jumping)
            jumpDuration += Time.fixedDeltaTime;
    

        if(grounded)
            jumpDuration = 0;
    }

    void CheckInput()
    {

        horMov = Input.GetAxisRaw("Horizontal");

        if(grounded)
        {
            setGravityScale(1);
            jumping = false;
            canJump = true;
            DetermineJumpButton();
        }

         JumpButton();

        if(rb.velocity.x > 0 && !facingRight)
            Flip();
        else if(rb.velocity.x < 0 && facingRight)
            Flip();

        anim.SetFloat("Speed", Mathf.Abs(horMov));
        anim.SetFloat("Vspeed", rb.velocity.y);
        anim.SetBool("Grounded" , grounded);
    }

    void Run(float horMov)
    {
        Vector2 runForce = rb.velocity;

        if(horMov != 0)
        {
            if(!grounded)
            {
                runForce += (speed * horMov * Vector2.right * accelerationFactor * aircellerationFactor);

                if(Mathf.Abs(rb.velocity.x) > maxSpeed)
                {
                    float sign = Mathf.Sign(rb.velocity.x);
                    runForce.x -= rb.velocity.x * autoBrakeFactor * aircellerationFactor;
                }
            }

            else
            {
                runForce += (speed * horMov) * Vector2.right * accelerationFactor;

                if(Mathf.Abs(rb.velocity.x) > maxSpeed)
                {
                    float sign = Mathf.Sign(rb.velocity.x);
                    runForce.x = maxSpeed * sign;
                }
            }
            rb.velocity = runForce;
        }

        else
        {
            if(!grounded)
            {
                if(Mathf.Abs(rb.velocity.x) > maxSpeed)
                    runForce.x -= rb.velocity.x * autoBrakeFactor * airDecel;
            }
            else
            {
                if(Mathf.Abs(rb.velocity.x) > 0)
                    runForce.x -= rb.velocity.x * autoBrakeFactor * decel;
            }
            rb.velocity = runForce;
        }
    }

    void Jump()
    {
        Vector2 jumpVector = rb.velocity;
        jumpVector.y = jumpForce;

        if(jumpDuration < jumpTime)
        {
            rb.velocity = jumpVector;
        }
    }

    void JumpButton()
    {
        if(!Input.GetButton(ProjectConstants.JUMP_BUTTON))
        {
            if(rb.velocity.y >= 0)
            {
                Vector2 dragForce = rb.velocity;
                dragForce.y = rb.velocity.y * drag;

                rb.velocity = dragForce;

                if(!grounded)
                {
                    jumpDuration = jumpTime;
                }
            }
        }

        if(Input.GetButton(ProjectConstants.JUMP_BUTTON) && jumpAllowed)
        {
            jumping = true;
            canJump = false;

            //if(grounded)
                //queue jumpdust
        }

        if(jumpDuration >= jumpTime)
        {
            jumping = false;
            jumpAllowed = false;

            if(rb.velocity.y < 0)
                setGravityScale(gravityDropModifier);
        }
    }

    void QuickFallButton()
    {
        if(!grounded)
            if(Input.GetButton(ProjectConstants.QUICK_FALL_BUTTON_NAME))
            {
                maxFallSpeed = quickFallSpeed;
                Vector2 newSpeed = rb.velocity;
                newSpeed.y = quickFallSpeed;
                rb.velocity = newSpeed;
            }
            else
                maxFallSpeed = normalFallSpeed;

            if(rb.velocity.y < maxFallSpeed)
            {
                Vector2 newSpeed = rb.velocity;
                newSpeed.y = maxFallSpeed;
                rb.velocity = newSpeed;
            }
    }

    void DetermineJumpButton()
    {
        if(grounded && !Input.GetButton(ProjectConstants.JUMP_BUTTON))
        {
            jumpAllowed = true;
        }
    }

    private void stop_jumping()
    {
        if(grounded)
            return;

        Vector2 jumpVelocity = rb.velocity;
        jumpVelocity.y = 0;
        rb.velocity = jumpVelocity;

        jumping = false;
        jumpAllowed = false;
        jumpDuration = jumpTime/3;
    }

    public Rigidbody2D GetRigidbody(){return rb;}

    public void setGravityScale(float newScale){rb.gravityScale = newScale;}

    public PlayerStateManager GetPlayerState(){return playerState;}
    public PlayerHealth GetPlayerHealth(){return playerHealth;}

    void Flip()
    {
        facingRight = !facingRight;

        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
