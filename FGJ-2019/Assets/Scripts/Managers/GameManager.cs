// Date   : 25.01.2019 21:31
// Project: FGJ-2019
// Author : bradur

using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    [SerializeField]
    private GameConfig config;
    public GameConfig Config { get { return config; } }

    public static GameManager main;

    [SerializeField]
    private CameraManager cameraManager;

    [SerializeField]
    private UIManager uIManager;

    private bool playerIsDead = false;
    public bool PlayerIsDead { get { return playerIsDead; } }

    private bool levelFinished = false;
    public bool LevelFinished { get { return levelFinished; } }

    void Awake()
    {
        main = this;
    }

    public void SetupPlayer(Player player)
    {
        Time.timeScale = 1f;
        cameraManager.FollowPlayer(player.transform);
    }

    public void PlayerDied(string reason)
    {
        //Debug.Log(string.Format("Player died: {0}", reason));
        Time.timeScale = 0f;
        uIManager.ShowMessage(string.Format("{0}\n{1}", reason, "Press R to restart level."));
        playerIsDead = true;
    }

    public void FinishLevel()
    {
        levelFinished = true;
    }

    void Start()
    {

    }

    void Update()
    {
    }

    public void Reset()
    {
        levelFinished = false;
        uIManager.Hide();
    }
}
