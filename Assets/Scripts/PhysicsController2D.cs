using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class PhysicsController2D : MonoBehaviour
{
    private const float skinWidth = 1f;
    private const float minJumpDistance = 3f;
    
    public int horizontalRayCount = 4;
    public int verticalRayCount = 4;
    public float horizontalRaySpacing;
    public float verticalRaySpacing;

    public LayerMask collisionMask;
    public CollisionInfo collisions;

    private BoxCollider2D _collider;
    private RaycastOrigins raycastOrigins;

    private void Start()
    {
        _collider = GetComponent<BoxCollider2D>();
        CalculateRaySpacing();
    }

    public Vector3 Move(Vector3 velocity)
    {
        UpdateRaycastOrigins();
        collisions.Reset();
        
        if (velocity.x != 0)
            HorizontalCollisions(ref velocity);
        if (velocity.y != 0)
            VerticalCollisions(ref velocity);

        transform.Translate(velocity);

        Physics2D.SyncTransforms();

        return velocity;
    }

    private void HorizontalCollisions(ref Vector3 velocity)
    {
        float directionX = Mathf.Sign(velocity.x);
        float rayLength = Mathf.Abs(velocity.x) + skinWidth;

        for (int i = 0; i < horizontalRayCount; i++)
        {
            Vector2 rayOrigin = (directionX == -1) ? raycastOrigins.bottomLeft : raycastOrigins.bottomRight;
            rayOrigin += Vector2.up * (horizontalRaySpacing * i);

            //Debug.DrawRay(rayOrigin, Vector2.right * directionX * rayLength, Color.red);

            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.right * directionX, rayLength, collisionMask);

            if (hit)
            {
                velocity.x = (hit.distance - skinWidth) * directionX;
                rayLength = hit.distance;

                collisions.left = directionX == -1;
                collisions.right = directionX == 1;
            }
        }
    }

    private void VerticalCollisions(ref Vector3 velocity)
    {
        float directionY = Mathf.Sign(velocity.y);
        float rayLength = Mathf.Abs(velocity.y) + skinWidth;

        for (int i = 0; i < verticalRayCount; i++)
        {
            Vector2 rayOrigin = (directionY == -1) ? raycastOrigins.bottomLeft : raycastOrigins.topLeft;
            rayOrigin += Vector2.right * (verticalRaySpacing * i + velocity.x);

            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.up * directionY, rayLength, collisionMask);

            //Debug.DrawRay(rayOrigin, Vector2.up * directionY * rayLength, Color.red);

            if (hit)
            {
                velocity.y = (hit.distance - skinWidth) * directionY;
                rayLength = hit.distance;

                collisions.below = directionY == -1;
                collisions.above = directionY == 1;
            }
            else
            {
                RaycastHit2D hitJump = Physics2D.Raycast(rayOrigin, Vector2.up * directionY, rayLength + minJumpDistance, collisionMask);

                if (hitJump)
                {
                    collisions.canJump = true;
                }
            }
        }
    }

    private void UpdateRaycastOrigins()
    {
        Bounds bounds = _collider.bounds;
        bounds.Expand(skinWidth * -2);

        raycastOrigins.bottomLeft = new Vector2(bounds.min.x, bounds.min.y);
        raycastOrigins.bottomRight = new Vector2(bounds.max.x, bounds.min.y);
        raycastOrigins.topLeft = new Vector2(bounds.min.x, bounds.max.y);
        raycastOrigins.topRight = new Vector2(bounds.max.x, bounds.max.y);
    }

    private void CalculateRaySpacing()
    {
        Bounds bounds = _collider.bounds;
        bounds.Expand(skinWidth * -2);

        horizontalRayCount = Mathf.Clamp(horizontalRayCount, 2, int.MaxValue);
        verticalRayCount = Mathf.Clamp(verticalRayCount, 2, int.MaxValue);

        horizontalRaySpacing = bounds.size.y / (horizontalRayCount - 1);
        verticalRaySpacing = bounds.size.x / (verticalRayCount - 1);
    }

    [System.Serializable]
    public struct RaycastOrigins
    {
        public Vector2 topLeft, topRight;
        public Vector2 bottomLeft, bottomRight;
    }

    [System.Serializable]
    public struct CollisionInfo
    {
        public bool above, below;
        public bool left, right;
        public bool canJump;

        public void Reset()
        {
            above = below = false;
            left = right = false;
            canJump = false;
        }
    }
}
