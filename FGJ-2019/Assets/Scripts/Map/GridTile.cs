// Date   : 25.01.2019 21:24
// Project: FGJ-2019
// Author : bradur

using UnityEngine;
using System.Collections;
using TiledSharp;

public class GridTile : MonoBehaviour
{

    private SpriteRenderer spriteRenderer;

    private int x;
    private int y;
    public int X { get { return x; } }
    public int Y { get { return y; } }

    public void Initialize(Sprite sprite, int x, int y, GridLayerConfig gridLayerConfig, int layerNumber)
    {
        this.x = x;
        this.y = y;
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            spriteRenderer.sprite = sprite;
            spriteRenderer.sortingOrder = layerNumber;
            if (gridLayerConfig != null && gridLayerConfig.OverrideMaterial != null)
            {
                spriteRenderer.material = gridLayerConfig.OverrideMaterial;
            }
        }
        transform.position = new Vector2(x, y);
    }

}
