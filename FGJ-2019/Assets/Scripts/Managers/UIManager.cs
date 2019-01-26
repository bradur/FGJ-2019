// Date   : 26.01.2019 15:49
// Project: FGJ-2019
// Author : bradur

using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class UIManager : MonoBehaviour {

    public static UIManager main;

    void Awake()
    {
        main = this;
    }

    [SerializeField]
    private UIDialogueBox uIDialogueBox;
    public void ShowMessage(string message) {
        uIDialogueBox.AnimateMessage(message);
    }

    public void Initialize()
    {
        uIDialogueBox.Initialize();
    }

    public void ShowMessages(List<string> messages)
    {
        uIDialogueBox.AnimateListOfMessages(messages);
    }

    public void Hide()
    {
        uIDialogueBox.Hide();
    }

}
