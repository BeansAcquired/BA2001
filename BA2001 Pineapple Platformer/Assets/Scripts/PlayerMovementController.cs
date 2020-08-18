using BAUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    #region Properties
    [SerializeField] private LayerMask GroundLayer;                          // A mask determining what is ground to the character
    [SerializeField] private Transform GroundCheck;                           // A position marking where to check if the player is grounded.

    // Private
    private Rigidbody2D _rigidbody;
    private float _horizontalMove = 0f;
    private bool _isJumping = false;
    private bool _isSprinting = false;
    private bool _isGrounded;
    private bool _canJump = false;


    private float Speed
    {
        get
        {
            return _isSprinting
                ? _baseSpeed * _sprintMultiplier
                : _baseSpeed;
        }
    }

    // Configuration
    public float _jumpForce = 40f;
    private float _baseSpeed = 40f;
    private float _sprintMultiplier = 1.5f; // Speed multiplier while sprinting
    private float _groundedRadius = .2f; // Radius of the circle to determine if grounded
    private float _fallMultiplier = 2.5f; // Radius of the circle to determine if grounded
    private float _lowJumpMultiplier = 2f; // Radius of the circle to determine if grounded

    [Range(0, .3f)]
    [SerializeField]
    private float _movementSmoothing = .05f;	// How much to smooth out the movement
    private Vector3 _velocity = Vector3.zero;

    #endregion

    private void Start()
    {
        Trace.Info("Start", this);
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    // Get input in Update
    private void Update()
    {
        _horizontalMove = Input.GetAxisRaw("Horizontal") * Speed;

        if (Input.GetButtonDown("Jump") && _isGrounded)
        {
            _isJumping = true;
        }
        _isSprinting = Input.GetButton("Sprint");
       
    }

    // Apply physics in FixedUpdate
    private void FixedUpdate()
    {
        CheckGrounded();
        Move();
    }

    private void Move()
    {

        // Find target velocity
        Vector3 targetVelocity = new Vector2(_horizontalMove * Time.fixedDeltaTime * 10f, _rigidbody.velocity.y);

        // Apply and smooth
        _rigidbody.velocity = Vector3.SmoothDamp(_rigidbody.velocity, targetVelocity, ref _velocity, _movementSmoothing);

        if(_isGrounded && _isJumping)
        {
            Trace.Log("Jump!");
            _rigidbody.AddForce(new Vector2(0f, _jumpForce * 10f));
            if (_rigidbody.velocity.y < 0)
            {
                _rigidbody.velocity += Vector2.up * Physics2D.gravity.y * (_fallMultiplier - 1) * Time.fixedDeltaTime;
            }
            else if (_rigidbody.velocity.y > 0 && !Input.GetButton("Jump"))
            {
                _rigidbody.velocity += Vector2.up * Physics2D.gravity.y * (_lowJumpMultiplier - 1) * Time.fixedDeltaTime;
            }
            _isJumping = false;
        }
    }

    private void CheckGrounded()
    {
        //bool wasGrounded = _isGrounded;
        _isGrounded = false;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(GroundCheck.position, _groundedRadius, GroundLayer);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                _isGrounded = true;
            }
        }
    }
}
