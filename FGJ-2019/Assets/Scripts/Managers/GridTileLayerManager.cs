// Date   : 26.01.2019 09:15
// Project: FGJ-2019
// Author : bradur

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GridTileLayerManager : MonoBehaviour {

    public static GridTileLayerManager main;

    private List<GridTileLayer> layers = new List<GridTileLayer>();
    private GameConfig config;

    void Awake() {
        main = this;
    }

    void Start () {
        config = GameManager.main.Config;
    }

    public void Reset()
    {
        layers = new List<GridTileLayer>();
    }

    private GridTileLayer AddLayer(string layerName, Transform container) {
        if (config == null)
        {
            config = GameManager.main.Config;
        }
        GridTileLayer layer = Instantiate(config.GridTileLayerPrefab, container);
        layer.Initialize(layerName, container);
        layers.Add(layer);
        return layer;
    }

    private GridTileLayer GetLayer(string layerName) {
        foreach(GridTileLayer layer in layers) {
            if (layer.Name == layerName) {
                return layer;
            }
        }
        return null;
    }

    public void AddTile(GridTile tile, string layerName, Transform container) {
        GridTileLayer layer = GetLayer(layerName);
        if (layer == null) {
            layer = AddLayer(layerName, container);
        }
        layer.AddGridTile(tile);
    }


    public GridTile GetGridTileByPosition(string layerName, int x, int y) {
        GridTileLayer layer = GetLayer(layerName);
        return layer.GetGridTileByPosition(x, y);
    }

}
