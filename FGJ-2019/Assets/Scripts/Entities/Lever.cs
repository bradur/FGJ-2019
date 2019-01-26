using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour {

    public bool on = false;
    private bool prevOn;

    Animator anim;
    GridObject gridObject;

    List<Gate> linkedGates = new List<Gate>();

    
    string PROPERTY_ACTIVATIONID = "activationId";

    // Use this for initialization
    void Start () {
		
	}
    
    public void Init()
    {
        anim = gameObject.GetComponent<Animator>();
        gridObject = GetComponent<GridObject>();

        int activationId = gridObject.GetIntProperty(PROPERTY_ACTIVATIONID);

        List<GridObject> linkedObjects = GridObjectManager.main.GetGridObjectsByPropertyValue(PROPERTY_ACTIVATIONID, activationId);

        foreach(var o in linkedObjects)
        {
            Gate gate = o.GetComponent<Gate>();
            if (gate != null)
            {
                linkedGates.Add(gate);
            }
        }
        prevOn = !on;
    }

    // Update is called once per frame
    void Update () {
        if (prevOn != on)
        {
            anim.Play(on ? "on" : "off");
            prevOn = on;
        }
    }

    public void Activate()
    {
        foreach(var gate in linkedGates)
        {
            gate.Activate();
        }
        on = !on;
    }
}
