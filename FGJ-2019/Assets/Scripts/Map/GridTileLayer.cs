// Date   : 26.01.2019 09:15
// Project: FGJ-2019
// Author : bradur

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GridTileLayer : MonoBehaviour
{

    private List<GridTile> gridTiles = new List<GridTile>();

    private string layerName;

    public string Name { get { return layerName; } }

    public void Initialize(string layerName, Transform container)
    {
        name = string.Format("[Layer]: {0}", layerName);
        this.layerName = layerName;
        transform.SetParent(container);
    }

    public void AddGridTile(GridTile gridTile) {
        gridTile.transform.SetParent(transform, true);
        gridTiles.Add(gridTile);
    }

    public GridTile GetGridTileByPosition(int x, int y)
    {
        foreach (GridTile gridTile in gridTiles)
        {
            if (gridTile.X == x && gridTile.Y == y)
            {
                return gridTile;
            }
        }
        return null;
    }

}
