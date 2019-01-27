using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGame : MonoBehaviour {

    public static MainGame main;

    public TiledMap tiledMap;

    int currentLevelIndex = -1;
    
    GameObject currentLevel;

    private void Awake()
    {
        main = this;
    }

    // Use this for initialization
    void Start () {
        UIManager.main.Initialize();
        LoadNextLevel();
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
        loadLevel(currentLevelIndex, false);
    }

    public void LoadNextLevel()
    {
        loadLevel(++currentLevelIndex, true);
    }

    private void loadLevel(int level, bool showTexts)
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
        GameManager.main.Reset();
        currentLevel = new GameObject("Level " + level);
        tiledMap.LoadLevel(newLevel.text, currentLevel, showTexts);
    }

    private void finish()
    {
        List<string> endText = new List<string>();
        endText.Add("Now I, too, have a home.");
        UIManager.main.ShowEnd();
        print("GAME FINISHED");
    }
}
