using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewColliderConfig", menuName = "New ColliderConfig")]
public class ColliderConfig : ScriptableObject
{
    [SerializeField]
    private List<ColliderParams> colliders;
    public List<ColliderParams> Colliders { get { return colliders; } }

    [SerializeField]
    private string name;
    public string Name { get { return name; } }

    [SerializeField]
    private string description;
    public string Description { get { return description; } }

    public ColliderConfig()
    {
        colliders = new List<ColliderParams>();
    }
}

[Serializable]
public class ColliderParams
{
    [Range(-1, 1)]
    public float centerX;
    [Range(-1, 1)]
    public float centerY;

    [Range(-1, 1)]
    public float width;
    [Range(-1, 1)]
    public float height;
}