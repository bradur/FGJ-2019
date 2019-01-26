using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGame : MonoBehaviour {

    public static MainGame main;

    public TiledMap tiledMap;

    int currentLevelIndex = 0;
    
    GameObject currentLevel;

    private void Awake()
    {
        main = this;
    }

    // Use this for initialization
    void Start () {
        RestartLevel();
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.R))
        {
            RestartLevel();
        }
        
        if (Input.GetKeyDown(KeyCode.N))
        {
            LoadNextLevel();
        }
    }

    public void RestartLevel()
    {
        loadLevel(currentLevelIndex);
    }

    public void LoadNextLevel()
    {
        loadLevel(++currentLevelIndex);
    }

    private void loadLevel(int level)
    {
        Destroy(currentLevel);

        List<TextAsset> levels = GameManager.main.Config.Levels;
        if (level >= levels.Count)
        {
            finish();
            return;
        }

        TextAsset newLevel = GameManager.main.Config.Levels[level];
        GridObjectManager.main.Reset();
        GridTileLayerManager.main.Reset();
        currentLevel = new GameObject("Level " + level);
        tiledMap.LoadLevel(newLevel.text, currentLevel);
    }

    private void finish()
    {
        print("GAME FINISHED");
    }
}
