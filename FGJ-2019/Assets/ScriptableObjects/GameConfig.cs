
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

    [Header("UI")]
    [SerializeField]
    private Color uiBackgroundColor;
    public Color UIBackgroundColor { get { return uiBackgroundColor; } }
    [SerializeField]
    private Color uiBorderColor;
    public Color UIBorderColor { get { return uiBorderColor; } }
    [SerializeField]
    private Color uiForegroundColor;
    public Color UIForegroundColor { get { return uiForegroundColor; } }

    [SerializeField]
    [Tooltip("Amount of letters per second.")]
    [Range(10f, 50f)]
    private float uiTextAnimationSpeed = 1f;
    public float UITextAnimationSpeed { get { return uiTextAnimationSpeed; } }

    [SerializeField]
    private KeyCode dialogueSkipKey;
    public KeyCode DialogueSkipKey { get { return dialogueSkipKey; } }
}
