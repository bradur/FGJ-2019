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

    public void Initialize(Sprite sprite, int x, int y, GridLayerConfig gridLayerConfig, ColliderConfig colliderConfig, string layerName, int layerNumber)
    {
        this.x = x;
        this.y = y;
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            spriteRenderer.sprite = sprite;
            //spriteRenderer.sortingOrder = layerNumber;
            spriteRenderer.sortingOrder = -(y * 2);
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
            gameObject.layer = LayerMask.NameToLayer("Wall");
            //spriteRenderer.sortingOrder = -(y * 10);
            //spriteRenderer.sortingOrder = layerNumber;
            gameObject.layer = LayerMask.NameToLayer("Wall");
            //spriteRenderer.sortingOrder;
        }
        else if(layerName == "ground")
        {
            spriteRenderer.sortingOrder = -2000;

        }
        else
        {
            if (spriteRenderer == null)
            {
                print("asd");
            }
            if (layerName != "decorations") {
                spriteRenderer.sortingOrder += layerNumber + 1;
            } else {
                spriteRenderer.sortingOrder += 1;
            }
        }

        transform.position = new Vector2(x, y);

    }

}
