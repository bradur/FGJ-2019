using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dog : MonoBehaviour
{
    public float moveSpeed = 10;
    public float aggroRange = 10;
    public float leashRange;
    
    public Vector2 leashOrigin;
    TrackedPosition playerPosition;

    Rigidbody2D rigidbody;
    Animator anim;
    SpriteRenderer renderer;

    Vector2 desiredMoveDirection;

    // Use this for initialization
    void Start ()
    {
        rigidbody = gameObject.GetComponent<Rigidbody2D>();
        anim = gameObject.GetComponent<Animator>();
        renderer = gameObject.GetComponent<SpriteRenderer>();

        playerPosition = GameManager.main.Config.PlayerPosition;
    }
	
	// Update is called once per frame
	void Update ()
    {
        var curPos = getPos();
        var distanceToPlayer = Vector2.Distance(curPos, playerPosition.Position);
        if (distanceToPlayer < aggroRange)
        {
            var playerInsideLeashRange = leashOrigin != null && Vector2.Distance(leashOrigin, playerPosition.Position) <= leashRange;
            Vector2 targetPos;
            if (playerInsideLeashRange)
            {
                targetPos = playerPosition.Position;
            }
            else
            {
                targetPos = leashOrigin + (playerPosition.Position - leashOrigin).normalized * leashRange;
            }
            desiredMoveDirection = targetPos - curPos;
            rigidbody.velocity = desiredMoveDirection.normalized * moveSpeed;
        } else
        {
            rigidbody.velocity = Vector2.zero;
        }

        if (rigidbody.velocity.magnitude > 0.001f)
        {
            anim.Play("walk");
        }
        else
        {
            anim.Play("idle");
        }

        var horizontalDistance = (playerPosition.Position - curPos).x;
        if (horizontalDistance > 0.001f)
        {
            renderer.flipX = false;
        }
        if (horizontalDistance < -0.001f)
        {
            renderer.flipX = true;
        }
    }

    private void FixedUpdate()
    {
    }

    private Vector2 getPos()
    {
        return new Vector2(transform.position.x, transform.position.y);
    }
}
