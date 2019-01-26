using UnityEngine;
using TiledSharp;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Linq;


public class TiledMap : MonoBehaviour
{

    private Mesh mesh;

    private MeshFilter meshFilter;
    private MeshRenderer meshRenderer;
    private MeshCollider meshCollider;

    private TmxMap map;

    private List<Transform> containers = new List<Transform>();

    [SerializeField]
    private TiledMapTilesetManager tiledMapTilesetManager;

    private GridObjectConfig[] gridObjectConfigs;

    private GridLayerConfig[] gridLayerConfigs;

    private Transform scene;

    GameConfig config;

    public void Initialize()
    {
    }

    public void LoadLevel(string levelData, GameObject scene)
    {
        config = GameManager.main.Config;
        gridLayerConfigs = Resources.LoadAll<GridLayerConfig>("Configurations/GridLayers");
        gridObjectConfigs = Resources.LoadAll<GridObjectConfig>("Configurations/GridObjects");
        this.scene = scene.transform;
        containers = new List<Transform>();
        XDocument mapX = XDocument.Parse(levelData);
        TmxMap map = new TmxMap(mapX);
        InitializeMap(map);
    }

    public void InitializeMap(TmxMap map)
    {
        tiledMapTilesetManager.Initialize(map);
        DrawLayers(map);
        SpawnObjects(map);
        GridObjectManager.main.MapLoaded();
    }

    private void DrawLayers(TmxMap map)
    {
        int mapHeight = map.Height;
        int layerNumber = 0;
        foreach (TmxLayer layer in map.Layers)
        {
            GridLayerConfig gridLayerConfig = GetGridLayerConfig(layer.Name);
            foreach (TmxLayerTile tile in layer.Tiles)
            {
                if (tile.Gid != 0)
                {
                    SpawnTile(tile, tile.X, -tile.Y, layer, gridLayerConfig, layerNumber);
                }
            }
            layerNumber += 1;
        }
    }

    private void SpawnTile(TmxLayerTile tile, int x, int y, TmxLayer layer, GridLayerConfig gridLayerConfig, int layerNumber)
    {
        Transform container = GetContainer("TileLayers");
        GridTile spawnedTile = null;
        if (gridLayerConfig != null) {
            if (gridLayerConfig.OverridePrefab) {
                spawnedTile = Instantiate(gridLayerConfig.OverridePrefab, scene.transform);
            } else
            {
                spawnedTile = Instantiate(config.GridTilePrefab, scene.transform);
            }
        } else {
            spawnedTile = Instantiate(config.GridTilePrefab, scene.transform);
        }
        Sprite sprite = GetTileSprite(tile.Gid);
        spawnedTile.Initialize(sprite, x, y, gridLayerConfig, layerNumber);
        GridTileLayerManager.main.AddTile(spawnedTile, layer.Name, container);
    }

    private Sprite GetTileSprite(int tileGid)
    {
        return tiledMapTilesetManager.GetSprite(tileGid);
    }
    private GridLayerConfig GetGridLayerConfig(string name) {
        foreach(GridLayerConfig gridLayerConfig in gridLayerConfigs) {
            if (gridLayerConfig.Name == name) {
                return gridLayerConfig;
            }
        }
        return null;
    }

    private void SpawnObjects(TmxMap map)
    {
        int mapHeight = map.Height;
        foreach (TmxObjectGroup objectGroup in map.ObjectGroups)
        {
            Transform container = GetContainer(objectGroup.Name);
            foreach (TmxObject tmxObject in objectGroup.Objects)
            {
                int xPos = (int)(tmxObject.X);
                int yPos = (int)(-tmxObject.Y);
                SpawnObject(
                    xPos,
                    yPos,
                    tmxObject,
                    container
                );
            }
        }
    }

    private void SpawnObject(int x, int y, TmxObject tmxObject, Transform container)
    {
        if (tmxObject.Tile != null)
        {
            Sprite sprite = GetTileSprite(tmxObject.Tile.Gid);
            GridObjectConfig objectConfig = GetObjectConfigByName(tmxObject.Type);
            if (objectConfig == null || objectConfig.Prefab == null)
            {
                Debug.Log(string.Format("No object config found or no prefab set for {0}", tmxObject.Type));
                return;
            }
            GridObject newObject = Instantiate(objectConfig.Prefab, container);

            Vector2 tileSetSize = tiledMapTilesetManager.GetTileSetSize(tmxObject.Tile.Gid);
            Vector2 objectPosition = new Vector2(
                x / tileSetSize.x,
                y / tileSetSize.y + 1 // unity position starts at bottom
            );

            newObject.Initialize(objectConfig, objectPosition, tmxObject.Properties);
            GridObjectManager.main.AddGridObject(newObject);
        }
    }

    private GridObjectConfig GetObjectConfigByName(string configName) {
        GridObjectConfig gridObjectConfig = null;

        foreach(GridObjectConfig config in gridObjectConfigs) {
            if (config.Name == configName) {
                return config;
            }
        }

        return gridObjectConfig;
    }

    private Transform GetContainer(string containerName)
    {
        Transform newContainer = null;
        foreach (Transform container in containers)
        {
            if (container.name == containerName)
            {
                newContainer = container;
                break;
            }
        }
        if (newContainer == null)
        {
            newContainer = Instantiate(config.ContainerPrefab, scene);
            newContainer.name = containerName;
            containers.Add(newContainer);
        }
        return newContainer;
    }


}
