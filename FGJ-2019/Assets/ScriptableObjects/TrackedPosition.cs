using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "NewTrackedPosition", menuName = "New TrackedPosition")]
public class TrackedPosition : ScriptableObject
{
    [SerializeField]
    private string objectName = "New TrackedPosition";
    public string Name { get { return objectName; } }

    [SerializeField]
    private Vector2 position;
    public Vector2 Position { get { return position; } set { position = value; } }
}
