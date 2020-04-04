using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PhysicsController2D))]
public class Player : MonoBehaviour
{
    public float jumpHeight = 4;
    public float timeToJumpApex = .4f;
    public float accelerationTimeAirborne = .2f;
    public float accelerationTimeGrounded = .1f;
    public float moveSpeed = 6;

    private float jumpVelocity;
    private float gravity = -20;

    private Vector3 velocity;
    private float velocityXSmoothing;
    private PhysicsController2D _controller2d;

    private bool jumpConsumed;
    
    private void Start()
    {
        _controller2d = GetComponent<PhysicsController2D>();

        gravity = -(2 * jumpHeight) / Mathf.Pow(timeToJumpApex, 2);
        jumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;
    }

    private void Update()
    {
        if (_controller2d.collisions.above || _controller2d.collisions.below)
        {
            velocity.y = 0;
            jumpConsumed = false;
        }
        else if (_controller2d.collisions.canJump)
        {
            jumpConsumed = false;
        }

        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        if(Input.GetKeyDown(KeyCode.UpArrow) && !jumpConsumed)
        {
            velocity.y = jumpVelocity;
            jumpConsumed = true;
        }

        velocity.x = input.x * moveSpeed;
        //float targetVelocityX = input.x * moveSpeed;
        //velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityXSmoothing, (_controller2d.collisions.below) ? accelerationTimeGrounded : accelerationTimeAirborne);
        
        velocity.y += gravity * Time.deltaTime;
        _controller2d.Move(velocity * Time.deltaTime);
    }
}
