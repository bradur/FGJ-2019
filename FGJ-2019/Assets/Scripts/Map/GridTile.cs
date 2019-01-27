// Date   : 25.01.2019 21:24
// Project: FGJ-2019
// Author : bradur

using UnityEngine;
using System.Collections;
using TiledSharp;
using System.Collections.Generic;

public class GridTile : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    private int x;
    private int y;
    public int X { get { return x; } }
    public int Y { get { return y; } }

    public void Initialize(Sprite sprite, int x, int y, GridLayerConfig gridLayerConfig, ColliderConfig colliderConfig, string layerName)
    {
        this.x = x;
        this.y = y;
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            spriteRenderer.sprite = sprite;
            if (gridLayerConfig != null && gridLayerConfig.OverrideMaterial != null)
            {
                spriteRenderer.material = gridLayerConfig.OverrideMaterial;
            }
        }

        if (layerName == "obstacles")
        {
            foreach (ColliderParams param in colliderConfig.Colliders)
            {
                BoxCollider col = gameObject.AddComponent<BoxCollider>();
                col.center = new Vector3(param.centerX, param.centerY);
                col.size = new Vector3(param.width, param.height, 2f);
            }
            spriteRenderer.sortingOrder = y * 10 - 10;
            gameObject.layer = LayerMask.NameToLayer("Wall");
        }
        else if(layerName == "ground")
        {
            spriteRenderer.sortingOrder = y * 10 - 10;
        }
        else
        {
            spriteRenderer.sortingOrder = y * 9;
        }

        transform.position = new Vector2(x, y);

    }

}
