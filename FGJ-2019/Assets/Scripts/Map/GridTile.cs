// Date   : 25.01.2019 21:24
// Project: FGJ-2019
// Author : bradur

using UnityEngine;
using System.Collections;
using TiledSharp;

public class GridTile : MonoBehaviour {

    private SpriteRenderer spriteRenderer;
    public void Initialize(Sprite sprite, int x, int y) {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        spriteRenderer.sprite = sprite;
        transform.position = new Vector2(x, y);
    }

}
