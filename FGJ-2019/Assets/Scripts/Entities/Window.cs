using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Window : MonoBehaviour {

    public List<string> texts;

    GridObject gridObject;

    // Use this for initialization
    void Start()
    {
    }

    public void Init()
    {
        gridObject = GetComponent<GridObject>();
        texts = Tools.getTexts(gridObject.Properties);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
