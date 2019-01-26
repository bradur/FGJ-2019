
using UnityEngine;
using System.Collections;
using System.Collections.Generic;


[CreateAssetMenu(fileName = "NewGameConfig", menuName = "New GameConfig")]
public class GameConfig : ScriptableObject
{
    [SerializeField]
    private TextAsset firstLevel;
    public TextAsset FirstLevel { get { return firstLevel; } }

    [Header("Tracked Positions")]
    [SerializeField]
    private TrackedPosition playerPosition;
    public TrackedPosition PlayerPosition { get { return playerPosition; } }

    [Header("Prefabs")]
    [SerializeField]
    private GridTile gridTilePrefab;
    public GridTile GridTilePrefab { get { return gridTilePrefab; } }

    [SerializeField]
    private Transform containerPrefab;
    public Transform ContainerPrefab { get { return containerPrefab; } }

    [SerializeField]
    private Player playerPrefab;
    public Player PlayerPrefab { get { return playerPrefab; } }

    [SerializeField]
    private GridTileLayer gridTileLayerPrefab;
    public GridTileLayer GridTileLayerPrefab { get { return gridTileLayerPrefab; } }

    [Header("Other configs")]
    [SerializeField]
    private List<ColliderConfig> colliderConfigs;
    public List<ColliderConfig> ColliderConfigs { get { return colliderConfigs; } }
}
