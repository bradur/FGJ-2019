using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public float moveSpeed = 20;

    Rigidbody2D rigidbody;
    Animator anim;
    SpriteRenderer renderer;
    GridObject gridObject;

    Vector2 desiredMoveDirection;
    TrackedPosition playerPosition;

    // Use this for initialization
    void Start()
    {
        rigidbody = gameObject.GetComponent<Rigidbody2D>();
        anim = gameObject.GetComponent<Animator>();
        renderer = gameObject.GetComponent<SpriteRenderer>();
        gridObject = GetComponent<GridObject>();

        playerPosition = GameManager.main.Config.PlayerPosition;
        updatePlayerPosition();
    }
	
	// Update is called once per frame
	void Update () {

        //reading the input:
        float horizontalAxis = Input.GetAxis("Horizontal");
        float verticalAxis = Input.GetAxis("Vertical");

        //camera forward and right vectors:
        var up = Vector2.up;
        var right = Vector2.right;

        desiredMoveDirection = up * verticalAxis + right * horizontalAxis;
        rigidbody.velocity = desiredMoveDirection * moveSpeed;

        if (rigidbody.velocity.magnitude > 0.001f)
        {
            anim.Play("walk");
        } else
        {
            anim.Play("idle");
        }

        var horizontal_velocity = rigidbody.velocity.x;

        if (horizontal_velocity > 0.001f)
        {
            renderer.flipX = false;
        }
        if (horizontal_velocity < -0.001f)
        {
            renderer.flipX = true;
        }

        updatePlayerPosition();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log(gridObject.GetIntProperty("activateId"));
        }
    }

    private void updatePlayerPosition()
    {
        playerPosition.Position = transform.position;
    }

    private void FixedUpdate()
    {
        
    }
}
