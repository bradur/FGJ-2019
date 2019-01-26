// Date   : 25.01.2019 23:36
// Project: FGJ-2019
// Author : bradur

using UnityEngine;
using System.Collections;
using TiledSharp;

public class GridObject : MonoBehaviour
{

    private GridObjectConfig config;
    public GridObjectConfig Config { get { return config; } }

    public PropertyDict Properties;

    public void Initialize(GridObjectConfig gridObjectConfig, Vector2 position, PropertyDict objectProperties)
    {
        Properties = objectProperties;
        config = gridObjectConfig;
        transform.position = position;
        Player player = GetComponent<Player>();
        if (player != null) {
            GameManager.main.SetupPlayer(player);
        }
    }

    public void MapLoaded()
    {
        SendMessage("Init", SendMessageOptions.DontRequireReceiver);
    }

    public string GetStringProperty(string propertyName) {
        return Tools.GetProperty(Properties, propertyName);
    }

    public int GetIntProperty(string propertyName) {
        return Tools.IntParseFast(GetStringProperty(propertyName));
    }

    public float GetFloatProperty(string propertyName)
    {
        string propertyValue = GetStringProperty(propertyName);
        if (propertyValue != null) {
            return Tools.FloatParse(propertyValue);
        }
        return -1;
    }

    public bool getBoolProperty(string propertyName)
    {
        return Tools.BoolParse(GetStringProperty(propertyName));
    }
}
