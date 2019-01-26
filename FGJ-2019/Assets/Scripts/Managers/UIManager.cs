// Date   : 26.01.2019 15:49
// Project: FGJ-2019
// Author : bradur

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIManager : MonoBehaviour {

    [SerializeField]
    private UIDialogueBox uIDialogueBox;
    public void ShowMessage(string message) {
        uIDialogueBox.AnimateMessage(message);
    }

}
