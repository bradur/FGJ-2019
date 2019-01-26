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

    private PropertyDict properties;

    public void Initialize(GridObjectConfig gridObjectConfig, Vector2 position, PropertyDict objectProperties)
    {
        properties = objectProperties;
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
        return Tools.GetProperty(properties, propertyName);
    }

    public int GetIntProperty(string propertyName) {
        return Tools.IntParseFast(Tools.GetProperty(properties, propertyName));
    }

    public float getFloatProperty(string propertyName)
    {
        return 0.0f;
    }
}
