using System.Collections;
using System.Collections.Generic;
using TouchDevUltimate;
using TouchDevUltimate.Gameplay.Character;
using UnityEngine;

[RequireComponent(typeof(PhysicsController2D))]
public class CharacterMovement : CharacterAbility
{
    public Transform scarf;
    public float jumpHeight = 4;
    public float timeToJumpApex = .4f;
    public float stompSpeed = 2;
    public float moveSpeed = 6;
    public float accelerationTimeAirborne = .2f;
    public float accelerationTimeGrounded = .1f;

    [Header("Inputs")]
    public string movementInput;
    
    private float jumpVelocity;
    private float gravity;
    private bool jumpInitiated;
    private bool jumpConsumed;

    private bool stompInitiated;

    private Vector3 velocity;
    private Vector3 actualVelocity;
    private float velocityXSmoothing;
    
    private PhysicsController2D _controller2d;

    protected override void Init()
    {
        _controller2d = GetComponent<PhysicsController2D>();
        
        gravity = -(2 * jumpHeight) / Mathf.Pow(timeToJumpApex, 2);
        jumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;
    }

    protected override void UpdateAbility()
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

        if (jumpInitiated && !jumpConsumed)
        {
            velocity.y = jumpVelocity;
            jumpConsumed = true;
            jumpInitiated = false;
        }

        if (stompInitiated)
        {
            if (!_controller2d.collisions.below)
            {
                velocity.y = -jumpVelocity * stompSpeed;
                jumpConsumed = false;
            }
            stompInitiated = false;
        }

        //if (Input.GetKeyDown(KeyCode.UpArrow) && !jumpConsumed)
        //{
        //    velocity.y = jumpVelocity;
        //    jumpConsumed = true;
        //}

        //float targetVelocityX = input.x * moveSpeed;
        //velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityXSmoothing, (_controller2d.collisions.below) ? accelerationTimeGrounded : accelerationTimeAirborne);
        //velocity.x = input.x * moveSpeed;

        velocity.y += gravity * Time.deltaTime;
        actualVelocity = _controller2d.Move(velocity * Time.deltaTime) / Time.deltaTime;
    }

    public bool groundedOld;
    public bool fallAnimationExecuted;

    #region Animations

    protected override void UpdateAnimator()
    {
        _character.model.SetFloat("VerticalVelocity", actualVelocity.y);
        _character.model.SetBool("Running", velocity.x != 0);
        _character.model.SetBool("Grounded", _controller2d.collisions.below);

        TweeningAnimations();

        scarf.position = transform.position + (Vector3.up * 0.85f);
    }

    #region Tweening

    private void TweeningAnimations()
    {
        if (_controller2d.collisions.below && !groundedOld)
        {
            StartCoroutine(GroundHitAnimation(0.08f, LeanTweenType.easeOutCirc));
            fallAnimationExecuted = false;
        }
        else if (!_controller2d.collisions.below && groundedOld)
        {
            // Jumping animation
            _character.model.gameObject.ScaleTween(new Vector3(0.9f, 1.1f, 1), 0.25f, LeanTweenType.easeOutCirc);
        }
        else if (actualVelocity.y < 0 && !fallAnimationExecuted)
        {
            // Falling animation
            _character.model.gameObject.ScaleTween(new Vector3(1.1f, 0.9f, 1), 0.25f, LeanTweenType.easeOutCirc);
            fallAnimationExecuted = true;
        }

        groundedOld = _controller2d.collisions.below;
    }

    private IEnumerator GroundHitAnimation(float delay, LeanTweenType easeType)
    {
        _character.model.gameObject.ScaleTween(new Vector3(1.5f, 0.6f, 1), delay, easeType);
        yield return new WaitForSeconds(delay);
        _character.model.gameObject.ScaleTween(new Vector3(1, 1.3f, 1), delay, easeType);
        yield return new WaitForSeconds(delay);
        _character.model.gameObject.ScaleTween(Vector3.one, delay, easeType);
    } 

    #endregion

    #endregion

    public override void OnGesture(string name, GestureType type)
    {
        if (name == movementInput)
        {
            if (type == GestureType.SwipeUp && !jumpInitiated && !jumpConsumed)
            {
                jumpInitiated = true; 
            }
            if (type == GestureType.SwipeDown)
            {
                stompInitiated = true;
            }
        }
    }
}
