// Date   : 25.01.2019 23:36
// Project: FGJ-2019
// Author : bradur

using UnityEngine;
using System.Collections;

public class GridObject : MonoBehaviour
{

    private GridObjectConfig config;
    public GridObjectConfig Config { get { return config; } }
    public void Initialize(GridObjectConfig gridObjectConfig, Vector2 position)
    {
        config = gridObjectConfig;
        transform.position = position;
        Player player = GetComponent<Player>();
        if (player != null) {
            GameManager.main.SetupPlayer(player);
        }
    }
}
