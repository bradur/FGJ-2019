using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour {

    public BoxCollider verticalCollider;
    public BoxCollider horizontalCollider;

    public int activationId;
    public bool horizontal;
    public bool closed;

    bool prevClosed;
    
    Animator anim;
    GridObject gridObject;
    BoxCollider collider;
    
    string PROPERTY_HORIZONTAL = "horizontal";
    string PROPERTY_CLOSED = "closed";

    // Use this for initialization
    void Start () {
		
	}
    
    public void Init()
    {
        anim = gameObject.GetComponent<Animator>();
        gridObject = GetComponent<GridObject>();
        
        horizontal = gridObject.getBoolProperty(PROPERTY_HORIZONTAL);
        closed = gridObject.getBoolProperty(PROPERTY_CLOSED);

        prevClosed = !closed;

        horizontalCollider.enabled = false;
        verticalCollider.enabled = false;
        if (horizontal)
        {
            collider = horizontalCollider;
        }
        else
        {
            collider = verticalCollider;
        }
    }

    // Update is called once per frame
    void Update () {
		if (closed != prevClosed)
        {
            if (horizontal)
            {
                anim.Play(closed ? "closed_horiz" : "open_horiz");
            } else
            {
                anim.Play(closed ? "closed_vert" : "open_vert");
            }
            collider.enabled = closed;
            prevClosed = closed;
        }
	}

    public void Activate()
    {
        closed = !closed;
    }
}
