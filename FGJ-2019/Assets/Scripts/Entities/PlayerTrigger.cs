// Date   : 26.01.2019 01:45
// Project: FGJ-2019
// Author : bradur

using UnityEngine;
using System.Collections;

public class PlayerTrigger : MonoBehaviour {

    void Start () {
    
    }

    void Update () {
    
    }

    private bool wasHit = false;

    void OnTriggerEnter (Collider collider) {
        // detect LightVision hits here
        Debug.Log(string.Format("{0} was hit by {1}", gameObject, collider.gameObject));
        GameManager.main.PlayerDied("You were seen!");
    }

}
