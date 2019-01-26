using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public float moveSpeed = 20;

    Rigidbody rigidbody3D;
    Animator anim;
    SpriteRenderer spriteRenderer;
    GridObject gridObject;

    Vector2 desiredMoveDirection;
    TrackedPosition playerPosition;

    // Use this for initialization
    void Start()
    {
        rigidbody3D = gameObject.GetComponent<Rigidbody>();
        anim = gameObject.GetComponent<Animator>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
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
        rigidbody3D.velocity = desiredMoveDirection * moveSpeed;

        if (rigidbody3D.velocity.magnitude > 0.001f)
        {
            anim.Play("walk");
        } else
        {
            anim.Play("idle");
        }

        var horizontal_velocity = rigidbody3D.velocity.x;

        if (horizontal_velocity > 0.001f)
        {
            spriteRenderer.flipX = false;
        }
        if (horizontal_velocity < -0.001f)
        {
            spriteRenderer.flipX = true;
        }

        updatePlayerPosition();

        if(Input.GetKeyDown(KeyCode.Space)) {
            string property = "activationId";
            Debug.Log(gridObject.GetIntProperty(property));
            List<GridObject> activateObjects = GridObjectManager.main.GetGridObjectsByPropertyValue(property, gridObject.GetIntProperty(property));
            foreach (GridObject activateObject in activateObjects) {
                Debug.Log(activateObject);
            }
        }
    }

    private void updatePlayerPosition()
    {
        playerPosition.Position = transform.position;
    }

    private void FixedUpdate()
    {
        
    }

    public void OnCollisionEnter(Collision collision)
    {
    }

    public void OnTriggerEnter(Collider other)
    {
        Lever lever = other.GetComponent<Lever>();
        if (lever != null)
        {
            lever.Activate();
        }
    }
}
