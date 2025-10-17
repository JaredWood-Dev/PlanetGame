using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    /*
     * A script to enable controlling the player character.
     * This script will have the following features:
     * Variable Jump Height
     * Jump Buffering
     * Tight Acceleration and Deceleration
     * Coyote Time
     * Edge Forgiveness
     * Breath Weapon Launch
     */

    public enum KeyState
    {
        Down,
        Pressed,
        Up,
        Off
    }
    public enum DirectionState
    {
        Left,
        Right,
        Off
    }
    [Header("Controls")]
    public KeyState jumpState = KeyState.Off;
    public DirectionState direction = DirectionState.Off;
    
    [Header("Movement")] 
    [Tooltip("The speed that the player can accelerate to using controls.")]
    public float speed;
    [Tooltip("The height, in units, that the player can jump to.")]
    public float jumpHeight;
    [Tooltip("How much the player's ability to move in the air is modifed.")]
    public float arialMovementModifer = 0.5f;
    
    [Header("Ground Detection")]
    [Tooltip("The current state of the player being on the ground.")]
    public bool onGround = false;
    [Tooltip("Distance at which the player can detect the ground.")]
    public float groundCheckDistance = 0.2f;
    [Tooltip("Layers that the player treats as ground.")]
    public LayerMask groundLayer;

    [Header("Jump Buffering")] 
    public bool jumpBuffered = false;
    public float jumpBufferTime = 0.2f;
    private float _bufferTimer = 0.0f;
    
    [Header("Coyote Time")]
    public float coyoteTime = 0.2f;
    private float _coyoteTimer = 0.0f;
    
    [Header("Special Ability")]
    public float abilityCoolDown = 0.2f;
    private float _abilityCoolDownTimer = 0.0f;
    
    private Rigidbody2D _rb;
    private Animator _an;

    void Start()
    {
        //Assign the Rigidbody
        _rb = GetComponent<Rigidbody2D>();
        _an = GetComponent<Animator>();
    }

    //Update is where the player's inputs are handled, NOT the Physics
    void Update()
    {
        // Handle Left-Right Inputs
        if (Input.GetAxis("Horizontal") > 0)
        {
            direction = DirectionState.Right;
            GetComponent<SpriteRenderer>().flipX = false;
        }
        if (Input.GetAxis("Horizontal") < 0)
        {
            direction = DirectionState.Left;
            GetComponent<SpriteRenderer>().flipX = true;
        }
        if (Input.GetAxis("Horizontal") == 0)
        {
            direction = DirectionState.Off;
        }
        
        //Handle the Jump Inputs
        if (Input.GetButtonDown("Jump"))
        {
            jumpState = KeyState.Down;
        }

        if (Input.GetButtonUp("Jump"))
        {
            jumpState = KeyState.Up;
        }

        if (Input.GetButtonDown("Fire3"))
        {
            if (_abilityCoolDownTimer >= abilityCoolDown)
            {
                _abilityCoolDownTimer = 0;
                SpecialAbility();
            }
        }
    }
    
    //Fixed Update is where the Physics calculations occur
    void FixedUpdate()
    {
        //Update Timers
        _bufferTimer += Time.deltaTime;
        _coyoteTimer += Time.deltaTime;
        _abilityCoolDownTimer += Time.deltaTime;

        Vector2 dir = Vector2.zero;
        //Calculate the force needed to accelerate the player to the desired speed
        float moveSpeed = 0;
        if (direction == DirectionState.Right)
            dir = transform.right;
        if (direction == DirectionState.Left)
            dir = -transform.right;


        if (direction != DirectionState.Off)
        {
            moveSpeed = (speed - Vector2.Dot(_rb.linearVelocity , dir));
            float acceleration = moveSpeed / Time.fixedDeltaTime;
            float force = acceleration * _rb.mass;
            if (!onGround)
                force *= arialMovementModifer;
            _rb.AddForce(force * dir, ForceMode2D.Force);
            _an.SetBool("isRunning", true);
        }
        else
        {
            _an.SetBool("isRunning", false);
            if (!onGround)
            {
                
                _rb.AddForce(_rb.linearVelocity.normalized * 10, ForceMode2D.Force);
            }
                
        }

        Physics2D.queriesHitTriggers = false;
        var groundCheckLeft = Physics2D.Raycast(transform.position - (0.5f * transform.right), -transform.up, groundCheckDistance, groundLayer);
        var groundCheckRight = Physics2D.Raycast(transform.position + (0.5f * transform.right), -transform.up, groundCheckDistance, groundLayer);
        Debug.DrawRay(transform.position - (0.5f * transform.right), -transform.up * groundCheckDistance, Color.green);
        if (groundCheckLeft.collider || groundCheckRight.collider)
        {
            onGround = true;
            _coyoteTimer = 0;
            _an.SetBool("onGround", true);
        }
        else
        {
            onGround = false;
            _an.SetBool("onGround", false);
        }

        //Calculate the force needed to jump to the desired height
        //print(Vector2.Dot(_rb.linearVelocity, transform.up));
        float jumpForce = (jumpHeight - Vector2.Dot(_rb.linearVelocity , transform.up) / Time.fixedDeltaTime) * _rb.mass;
        jumpForce /= 2;
        //Resolve Jump Inputs
        if (jumpState == KeyState.Down)
        {
            //When the Jump Key is Pressed
            jumpState = KeyState.Pressed;
            if (onGround || _coyoteTimer <= coyoteTime)
                _rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
            if (!onGround)
            {
                jumpBuffered = true;
                _bufferTimer = 0.0f;
            }
        }
        if (jumpState == KeyState.Pressed)
        {
            //When the Jump Key is Held
        }
        if (jumpState == KeyState.Up)
        {
            //When the Jump Key is Released
            jumpState = KeyState.Off;
            
            
            //If we are going up and release, half the upward velocity
            if ((_rb.linearVelocity * transform.up).y > 0)
                _rb.linearVelocity *= 0.5f;
        }
        
        //Resolve Buffered Jumps
        if (onGround)
        {
            if (jumpBuffered && _bufferTimer <= jumpBufferTime)
            {
                _rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
                _bufferTimer = 0;
            }
            jumpBuffered = false;
        }
    }

    public virtual void SpecialAbility()
    {
        print("Special Ability Used!");
    }
}
