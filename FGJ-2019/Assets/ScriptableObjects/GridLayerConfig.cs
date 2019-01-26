// Date   : 07.01.2019 20:52
// Project: VillageCaveGame
// Author : bradur

using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "NewGridLayerConfig", menuName = "New GridLayerConfig")]
public class GridLayerConfig : ScriptableObject
{
    [SerializeField]
    private string objectName = "New GridLayerConfig";
    public string Name { get { return objectName; } }

    [SerializeField]
    private GridTile overridePrefab;
    public GridTile OverridePrefab { get { return overridePrefab; } }

    [SerializeField]
    private Material overrideMaterial;
    public Material OverrideMaterial { get { return overrideMaterial; } }
}
