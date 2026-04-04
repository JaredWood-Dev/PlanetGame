using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

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
    public float direction = 0;
    
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
    public float abilityCoolDownTimer = 0.0f;
    
    private Rigidbody2D _rb;
    private Animator _an;
    protected ParticleSystemController AbilitySystem;
    private Collider2D _footCollider;
    private Collider2D _bodyCollider;

    private AudioSource _jumpEffect;
    protected AudioSource BreathEffect;

    private InputAction _jumpAction;
    private InputAction _moveAction;
    private InputAction _specialAbilityAction;

    void Start()
    {
        //Assign the Rigidbody
        _rb = GetComponent<Rigidbody2D>();
        _an = GetComponent<Animator>();
        AbilitySystem = GetComponent<ParticleSystemController>();
        
        _footCollider = GetComponents<Collider2D>()[0];
        _bodyCollider = GetComponents<Collider2D>()[1];
        
        _jumpEffect = GetComponents<AudioSource>()[1];
        BreathEffect = GetComponents<AudioSource>()[0];

        _jumpAction = InputSystem.actions.FindAction("Jump");
        _moveAction = InputSystem.actions.FindAction("Move");
        _specialAbilityAction = InputSystem.actions.FindAction("Attack");
    }

    //Update is where the player's inputs are handled, NOT the Physics
    void Update()
    {
        // Handle Left-Right Inputs
        direction = _moveAction.ReadValue<Vector2>().x;
       
        //Handle the Jump Inputs
        if (_jumpAction.WasPressedThisFrame())
        {
            jumpState = KeyState.Down;
        }

        if (_jumpAction.WasReleasedThisFrame())
        {
            jumpState = KeyState.Up;
        }

        if (_specialAbilityAction.WasPressedThisFrame())
        {
            if (abilityCoolDownTimer >= abilityCoolDown)
            {
                abilityCoolDownTimer = 0;
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
        abilityCoolDownTimer += Time.deltaTime;

        Vector2 dir = Vector2.zero;
        //Calculate the force needed to accelerate the player to the desired speed
        float moveSpeed = 0;
        if (direction > 0)
        {
            dir = transform.right;
            GetComponent<SpriteRenderer>().flipX = false;
        }

        if (direction < 0)
        {
            dir = -transform.right;
            GetComponent<SpriteRenderer>().flipX = true;
        }


        if (direction != 0)
        {
            moveSpeed = Mathf.Max(speed - Vector2.Dot(_rb.linearVelocity , dir), 0) * Mathf.Abs(direction);
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
                _rb.AddForce(-_rb.linearVelocity.normalized * 100, ForceMode2D.Force);
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
        float currentVelocity = Vector2.Dot(_rb.linearVelocity, transform.up);
        currentVelocity = Mathf.Max(currentVelocity, 0f);
        float jumpForce = (jumpHeight - 0/ Time.fixedDeltaTime) * _rb.mass;
        jumpForce /= 2;
        //Resolve Jump Inputs
        if (jumpState == KeyState.Down)
        {
            //When the Jump Key is Pressed
            jumpState = KeyState.Pressed;
            if (onGround || _coyoteTimer <= coyoteTime)
            {
                ZeroUpwardVelocity();
                _rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
                _jumpEffect.Play();
            }

            if (!onGround)
            {
                jumpBuffered = true;
                _bufferTimer = 0.0f;
            }
            _an.SetTrigger("jumped");
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
                ZeroUpwardVelocity();
                
                _rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
                print("jumped with " + (transform.up * jumpForce));
                _bufferTimer = 0;
                
                _jumpEffect.Play();
            }
            jumpBuffered = false;
        }
        
        //Edge Forgiveness
        //Whenever the player is stuck on an edge, give them a little bump to get up the edge
        //If the foot collider and body collider are touching the same object, we are stuck on an edge.
        List<Collider2D> footCollisions = new List<Collider2D>();
        _footCollider.GetContacts(footCollisions);
        
        List<Collider2D> bodyCollisions = new List<Collider2D>();
        _bodyCollider.GetContacts(bodyCollisions);

        var similarColliders = footCollisions.Intersect(bodyCollisions);

    }

    public virtual void SpecialAbility()
    {
        print("Special Ability Used!");
    }

    private void ZeroUpwardVelocity()
    {
        //Get velocity
        Vector2 velocity = _rb.linearVelocity;
        //convert to local frame of reference
        velocity *= transform.up;
        //zero out y
        velocity.y = 0;
        //convert to world frame of reference
        velocity *= Vector2.up;
        //set the velocity
        _rb.linearVelocity = velocity;
    }
}
