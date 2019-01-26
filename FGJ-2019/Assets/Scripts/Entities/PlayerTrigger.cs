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

    void OnTriggerEnter (Collider collider) {
        // detect LightVision hits here
        Debug.Log("Entering light!");
    }

}
