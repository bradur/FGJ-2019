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

    GameConfig config;
    private void Start()
    {
        config = GameManager.main.Config;
        gridLayerConfigs = Resources.LoadAll<GridLayerConfig>("Configurations/GridLayers");
        gridObjectConfigs = Resources.LoadAll<GridObjectConfig>("Configurations/GridObjects");
        XDocument mapX = XDocument.Parse(config.FirstLevel.text);
        TmxMap map = new TmxMap(mapX);
        Initialize(map);
    }

    public void Initialize(TmxMap map)
    {
        tiledMapTilesetManager.Initialize(map);
        DrawLayers(map);
        SpawnObjects(map);
    }
    /*private PlayerCharacter InstantiatePlayer()
    {
        return Instantiate(playerCharacterPrefab);
    }*/

    private void DrawLayers(TmxMap map)
    {
        int mapHeight = map.Height;
        
        foreach (TmxLayer layer in map.Layers)
        {
            GridLayerConfig gridLayerConfig = GetGridLayerConfig(Tools.GetProperty(layer.Properties, "Type"));
            Transform container = GetContainer(layer.Name);
            foreach (TmxLayerTile tile in layer.Tiles)
            {
                if (tile.Gid != 0)
                {
                    SpawnTile(tile, tile.X, -tile.Y, layer, gridLayerConfig, container);
                }
            }
        }
    }

    private void SpawnTile(TmxLayerTile tile, int x, int y, TmxLayer layer, GridLayerConfig gridLayerConfig, Transform container)
    {
        Sprite sprite = GetTileSprite(tile.Gid);
        GridTile spawnedTile = Instantiate(config.GridTilePrefab);
        spawnedTile.Initialize(sprite, x, y);
        spawnedTile.transform.SetParent(container);
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
            foreach (TmxObject tmxObject in objectGroup.Objects)
            {
                int xPos = (int)(tmxObject.X);
                int yPos = (int)(tmxObject.Y);
                SpawnObject(
                    xPos,
                    yPos,
                    tmxObject
                );
            }
        }
    }

    private void SpawnObject(int x, int y, TmxObject tmxObject)
    {
        if (tmxObject.Tile != null)
        {
            Sprite sprite = GetTileSprite(tmxObject.Tile.Gid);
            //GameObject spawnedObject = CreateObject(x, y, tmxObject, sprite);
        }
    }

    /*private GridObjectConfig GetObjectConfigByName(string configName) {
        GridObjectConfig gridObjectConfig = null;

        foreach(GridObjectConfig config in gridObjectConfigs) {
            if (config.Name == configName) {
                return config;
            }
        }

        return gridObjectConfig;
    }*/

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
            newContainer = Instantiate(config.ContainerPrefab);
            newContainer.name = containerName;
            containers.Add(newContainer);
        }
        return newContainer;
    }


}
