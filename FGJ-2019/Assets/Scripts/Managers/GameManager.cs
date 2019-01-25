// Date   : 25.01.2019 21:31
// Project: FGJ-2019
// Author : bradur

using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{

    [SerializeField]
    private GameConfig config;
    public GameConfig Config { get { return config; } }

    public static GameManager main;

    [SerializeField]
    private CameraManager cameraManager;

    void Awake() {
        main = this;
    }

    public void SetupPlayer(Player player) {
        cameraManager.FollowPlayer(player.transform);
    }

    void Start()
    {

    }

    void Update()
    {

    }
}
