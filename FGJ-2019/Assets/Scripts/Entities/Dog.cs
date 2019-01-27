using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dog : MonoBehaviour
{
    public float moveSpeed = 10;
    public float aggroRange = 10;
    
    public Vector2 leashOrigin;
    float leashRange;
    TrackedPosition playerPosition;

    Rigidbody rigidbody;
    Animator anim;
    SpriteRenderer renderer;
    GridObject gridObject;
    LineRenderer lineRenderer;

    Vector2 desiredMoveDirection;
    bool leashed = false;

    string PROPERTY_LEASHID = "leashId";
    string PROPERTY_LEASHRANGE = "leashRange";
    string PROPERTY_AGGRORANGE = "aggroRange";
    string PROPERTY_SPEED = "speed";

    // Use this for initialization
    void Start ()
    {

    }

    public void Init()
    {
        rigidbody = gameObject.GetComponent<Rigidbody>();
        anim = gameObject.GetComponent<Animator>();
        renderer = gameObject.GetComponent<SpriteRenderer>();
        gridObject = GetComponent<GridObject>();
        lineRenderer = GetComponent<LineRenderer>();
        playerPosition = GameManager.main.Config.PlayerPosition;

        float leashRangeFromMap = gridObject.GetFloatProperty(PROPERTY_LEASHRANGE);
        if (leashRangeFromMap > -1 ) {
            leashRange = leashRangeFromMap;
        }
        float aggroRangeFromMap = gridObject.GetFloatProperty(PROPERTY_AGGRORANGE);
        if (aggroRangeFromMap > -1 ){
            aggroRange = aggroRangeFromMap;
        }
        int leashId = gridObject.GetIntProperty(PROPERTY_LEASHID);
        if (leashId > -1)
        {
            List<GridObject> leashObjects = GridObjectManager.main.GetGridObjectsByPropertyValue(PROPERTY_LEASHID, leashId);
            leashOrigin = leashObjects[0].transform.position;
            leashed = true;
        }
        float aggroSpeed = gridObject.GetFloatProperty(PROPERTY_SPEED);
        if(aggroSpeed > -1)
        {
            moveSpeed = aggroSpeed;
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
        var curPos = getPos();
        var distanceToPlayer = Vector2.Distance(curPos, playerPosition.Position);
        if (distanceToPlayer < aggroRange)
        {
            var playerInsideLeashRange = !leashed || Vector2.Distance(leashOrigin, playerPosition.Position) <= leashRange;
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
            rigidbody.velocity = Vector3.zero;
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

        if (leashed)
        {
            lineRenderer.SetPosition(0, leashOrigin);
            lineRenderer.SetPosition(1, transform.position);
        }
        renderer.sortingOrder = (int)Math.Floor(transform.position.y * 10);
        lineRenderer.sortingOrder = (int)Math.Floor(transform.position.y * 10);
    }

    private void FixedUpdate()
    {
    }

    private Vector2 getPos()
    {
        return new Vector2(transform.position.x, transform.position.y);
    }
}
