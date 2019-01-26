// Date   : 16.12.2018 19:48
// Project: VillageCaveGame
// Author : bradur

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TiledSharp;
using System.Xml.Linq;

public class TiledMapTilesetManager : MonoBehaviour {

    private Dictionary<int, Sprite[]> tileSprites;
    private int[] firstGids; // First global ids for tilesets.
    private Dictionary<int, Texture2D> fullSprites;

    private TmxMap tmxMap;

    //private List<TextAsset> tileSetTextAssets;

    public void Initialize(TmxMap map) {
        tmxMap = map;
        LoadTilesetFiles();
        LoadSprites();
    }

    private TmxList<TmxTileset> tilesets;

    void LoadTilesetFiles() {
        List<XElement> files = new List<XElement>();

        foreach(string path in tmxMap.GetTilesetPaths()){
            string resourcePath = Tools.ReplaceString(
                path,
                "../",
                ""
            );
            resourcePath = Tools.ReplaceString(
                resourcePath,
                ".xml",
                ""
            );
            TextAsset textAsset = (TextAsset) Resources.Load(resourcePath);
            XDocument tilesetX = XDocument.Parse(textAsset.text);
            files.Add(tilesetX.Element("tileset"));
        }
        tmxMap.AddTilesetsAsXContainers(files);
    }

    /**
     * Load tileset images.
    **/
    void LoadSprites()
    {
        tilesets = tmxMap.Tilesets;
        tileSprites = new Dictionary<int, Sprite[]>();
        firstGids = new int[tilesets.Count];
        fullSprites = new Dictionary<int, Texture2D>();

        int index = 0;
        foreach(TmxTileset tileset in tilesets)
        {
            // Get the path of the source image file
            string tilesetPath = tileset.Image.Source;
            tilesetPath = Tools.ReplaceString(tilesetPath, "../", "");
            tilesetPath = Tools.ReplaceString(tilesetPath, ".png", "");

            // Load the Resource as an array of Sprites(when sliced)
            int firstGid = tileset.FirstGid;
            firstGids[index] = firstGid;
            Object[] spriteObjs = Resources.LoadAll(tilesetPath);

            try
            {
                tileSprites[firstGid] = new Sprite[spriteObjs.Length - 1];
            }
            catch (System.OverflowException)
            {
                Debug.Log("ERROR: Spritesheet \"" + tilesetPath + "\" is missing! Check filenames!");
            }

            fullSprites[firstGid] = spriteObjs[0] as Texture2D;
            // Start from 1 since element 0 will be empty.
            for (int spriteObjIndex = 1; spriteObjIndex < spriteObjs.Length; spriteObjIndex++)
            {
                Sprite sprite = spriteObjs[spriteObjIndex] as Sprite;
                tileSprites[firstGid][spriteObjIndex - 1] = sprite;
            }
            index += 1;
        }
    }

    /*
     *  The tiles are referred to by their global id (gid).
     *  For example, if we have a map that has two tilesheets
     *  that contain 10 tiles each, the global id of the last
     *  tile of the second sheet is 19 (0-indexed).
     *  
     *  To find out the index of the tilesheet for a particular gid,
     *  you must find out the tilesheet with the largest FirstGid
     *  that is lower or equal with the tile gid.
     *  
     *  Example:
     *   TileSheets:
     *   1. 10 tiles (FirstGid: 0)
     *   2. 15 tiles (FirstGid: 10)
     *   3. 5  tiles (FirstGid: 15)
     *   
     *   Gids:
     *   12 -> largest FirstGid smaller or equal: 10  TileSheet: 2
     *   9  -> largest FirstGid smaller or equal: 0   TileSheet: 1
     *   15 -> largest FirstGid smaller or equal: 15  TileSheet: 3
     */
    int FindTileSetIndex(int gid)
    {
        for (int index = firstGids.Length - 1; index >= 0; index--)
        {
            if (gid >= firstGids[index])
            {
                return index;
            }
        }
        // error: no such tileset
        return -1;
    }

    public Vector2 GetTileSetSize(int gid) {
        int index = FindTileSetIndex(gid);
        if (index != -1 ) {
            TmxTileset tileset = tilesets[index];
            return new Vector2(tileset.TileWidth, tileset.TileHeight);
        }
        return default(Vector2);
    }

    public Sprite GetSprite(int tileGid)
    {
        int tileSetIndex = FindTileSetIndex(tileGid);
        if (tileSetIndex != -1) {
            int tilesetFirstGid = firstGids[tileSetIndex];
            return tileSprites[tilesetFirstGid][tileGid - tilesetFirstGid];
        }
        return null;
    }

    public int GetTileIdFromGID(int gid)
    {
        int tileId = 0;
        int index = FindTileSetIndex(gid);
        if (index != -1)
        {
            int tilesetFirstGid = firstGids[index];
            tileId = gid - tilesetFirstGid;
        }

        return tileId;
    }

}
