using BAUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{

    #region Properties
    // Editor
    [Header("Collision Detection")] 
    public LayerMask GroundLayer;
    public Transform GroundCheck;
    public float _groundedRadius = .2f; // Radius of the circle to determine if grounded


    [Header("Physics")]
    public float JumpVelocity = 5f;
    public float BaseSpeed = 10f;
    [Range(0, 1f)]
    public float FallMultiplier = 0.5f;
    public float SprintMultiplier = 1.5f;

    [Header("Movement & Input")]
    [Range(0, .3f)]
    public float MovementSmoothing = .05f;	// How much to smooth out the movement
    public float JumpDelay = 0.25f;
    public float JumpTimer;
    public float GroundedDelay = 0.25f;
    public float GroundedTimer;

    private Rigidbody2D _rigidbody;
    private float _horizontalMove = 0f;
    private bool _isSprinting = false;
    private bool _isGrounded = false;
    private Vector3 _velocity = Vector3.zero;

    private float Speed
    {
        get
        {
            return _isSprinting
                ? BaseSpeed * SprintMultiplier
                : BaseSpeed;
        }
    }

    
    #endregion

    private void Start()
    {
        Trace.Start();
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    // Get input in Update
    private void Update()
    {
        CheckGrounded();
        _horizontalMove = Input.GetAxisRaw("Horizontal");

        // Jump

        JumpTimer -= Time.deltaTime;
        GroundedTimer -= Time.deltaTime;

        if (Input.GetButtonDown("Jump"))
        {
            JumpTimer = Time.deltaTime + JumpDelay;
        }

        if (Input.GetButtonUp("Jump"))
        {
            if (_rigidbody.velocity.y > 0)
            {
                _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _rigidbody.velocity.y * FallMultiplier);
            }
        }

        
        if (_isGrounded)
        {
            GroundedTimer = Time.deltaTime + GroundedDelay;
        }

        _isSprinting = Input.GetButton("Sprint");
       
    }

    // Apply physics in FixedUpdate
    private void FixedUpdate()
    {
       Move();

        // Jump
        if (JumpTimer > 0 && GroundedTimer > 0)
        {
            Jump();
        }
    }

    private void Move()
    {

        // Find target velocity
        Vector3 targetVelocity = new Vector2(_horizontalMove * Time.fixedDeltaTime * 10f, _rigidbody.velocity.y);

        // Apply and smooth
        _rigidbody.velocity = Vector3.SmoothDamp(_rigidbody.velocity, targetVelocity, ref _velocity, MovementSmoothing);
    }

    private void Jump()
    {
        if (JumpTimer > 0 && GroundedTimer > 0)
        {
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, JumpVelocity);
            JumpTimer = 0;
            GroundedTimer = 0;
        }
    }

    private void CheckGrounded()
    {
        _isGrounded = Physics2D.OverlapCircle(GroundCheck.position, _groundedRadius, GroundLayer);
        
    }
}
