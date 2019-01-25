using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public float moveSpeed = 20;

    Rigidbody2D rigidbody;
    
    Vector2 desiredMoveDirection;

	// Use this for initialization
	void Start () {
        rigidbody = gameObject.GetComponent<Rigidbody2D>();
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
    }

    private void FixedUpdate()
    {
        
    }
}
