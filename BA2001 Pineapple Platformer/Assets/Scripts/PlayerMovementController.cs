using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    #region Properties
    // Public
    public float Speed;
    public float JumpForce;
    

    // Private
    private Rigidbody2D _rigidbody;
    private float _horizontalMove = 0f;
    private bool isJumping = false;

    [Range(0, .3f)]
    [SerializeField]
    private float _movementSmoothing = .05f;	// How much to smooth out the movement
    private Vector3 _velocity = Vector3.zero;

    #endregion

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    // Get input in Update
    private void Update()
    {
        _horizontalMove = Input.GetAxisRaw("Horizontal") * Speed;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isJumping = true;
        }
    }

    // Apply physics in FixedUpdate
    private void FixedUpdate()
    {
        // TODO: Add debug logging
        // TODO: Add isGrounded check
        Move();
        Jump();
    }

    private void Move()
    {
        // Find target velocity
        Vector3 targetVelocity = new Vector2(_horizontalMove * Time.fixedDeltaTime * 10f, _rigidbody.velocity.y);

        // Apply and smooth
        _rigidbody.velocity = Vector3.SmoothDamp(_rigidbody.velocity, targetVelocity, ref _velocity, _movementSmoothing);
    }

    private void Jump()
    {
        if (isJumping)
        {
            _rigidbody.AddForce(new Vector2(0f, JumpForce * 10f));
            isJumping = false;

        }
    }
}
