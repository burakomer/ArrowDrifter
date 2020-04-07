using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PhysicsController2D))]
public class PhysicsObject2D : MonoBehaviour
{
    public float gravity = -20;

    private PhysicsController2D _controller2d;
    private Vector2 velocity;

    private void Start()
    {
        _controller2d = GetComponent<PhysicsController2D>();
    }

    private void Update()
    {
        if (_controller2d.collisions.above || _controller2d.collisions.below)
        {
            velocity.y = 0;
        }

        velocity.y += gravity * Time.deltaTime;

        _controller2d.Move(velocity * Time.deltaTime);
    }
}
