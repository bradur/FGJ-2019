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
        Debug.Log("TRIGGER");
        Debug.Log(collider.gameObject);
    }

    void OnCollisionEnter(Collision collision) {
        Debug.Log("COLLISION");
        Debug.Log(collision.gameObject);
    }
}
