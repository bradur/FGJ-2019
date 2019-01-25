// Date   : 25.01.2019 23:49
// Project: FGJ-2019
// Author : bradur

using UnityEngine;
using System.Collections;
using Cinemachine;

public class CameraManager : MonoBehaviour {


    [SerializeField]
    private CinemachineVirtualCamera playerFollowCamera;

    public void FollowPlayer(Transform playerTransform) {
        playerFollowCamera.Follow = playerTransform;
    }
}
