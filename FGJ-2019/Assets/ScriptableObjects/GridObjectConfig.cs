using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "NewGridObjectConfig", menuName = "New GridObjectConfig")]
public class GridObjectConfig : ScriptableObject
{
    [SerializeField]
    private string objectName = "New GridObjectConfig";
    public string Name { get { return objectName; } }

    [SerializeField]
    private GridObject prefab;
    public GridObject Prefab { get { return prefab; } }
}
