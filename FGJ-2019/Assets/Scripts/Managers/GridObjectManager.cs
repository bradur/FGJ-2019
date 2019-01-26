// Date   : 26.01.2019 09:15
// Project: FGJ-2019
// Author : bradur

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GridObjectManager : MonoBehaviour {

    public static GridObjectManager main;

    private List<GridObject> gridObjects = new List<GridObject>();
    void Start () {
        main = this;
    }

    public void AddGridObject(GridObject gridObject) {
        gridObjects.Add(gridObject);
    }

    public List<GridObject> GetGridObjectsByPropertyValue(string propertyName, string propertyValue) {
        List<GridObject> foundObjects = new List<GridObject>();
        foreach(GridObject gridObject in gridObjects) {
            if (gridObject.GetStringProperty(propertyName) == propertyValue) {
                foundObjects.Add(gridObject);
            }
        }
        return foundObjects;
    }

    public List<GridObject> GetGridObjectsByPropertyValue(string propertyName, int propertyValue) {
        List<GridObject> foundObjects = new List<GridObject>();
        foreach(GridObject gridObject in gridObjects) {
            if (gridObject.GetIntProperty(propertyName) == propertyValue) {
                foundObjects.Add(gridObject);
            }
        }
        return foundObjects;
    }
}
