// Date   : 26.01.2019 15:55
// Project: FGJ-2019
// Author : bradur

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class UIDialogueBox : MonoBehaviour
{

    [SerializeField]
    private Text txtDialogue;

    [SerializeField]
    private Image imgBorder;

    [SerializeField]
    private Image imgBackGround;

    private GameConfig config;

    private bool animatingText = false;

    private string fullMessage;

    private float textAnimationSpeed;
    private float textAnimationTimer = 0f;

    private static string ANIMATION_HIDE = "Hide";
    private static string ANIMATION_SHOW = "Show";

    private Animator animator;
    private bool hidden = true;

    private List<string> animatedMessages;

    public void AnimateMessage(string message)
    {
        if (hidden) {
            animator.SetTrigger(ANIMATION_SHOW);
        }
        if (message.Length > 0)
        {
            textAnimationTimer = 0f;
            fullMessage = message;
            txtDialogue.text = "";
            animatingText = true;
        }
    }

    public void AnimateListOfMessages(List<string> messages)
    {
        animatedMessages = messages;
        HideOrSkip();
    }

    public void WasHidden() {
        Time.timeScale = 1f;
        hidden = true;
    }

    public void WashShown() {
        Time.timeScale = 0f;
        hidden = false;
    }

    public void HideOrSkip() {
        if (animatedMessages.Count > 0) {
            string message = animatedMessages[0];
            AnimateMessage(message);
            animatedMessages.Remove(message);
        } else if (!GameManager.main.PlayerIsDead) {
            Hide();
        }
    }

    public void Hide()
    {
        if (!hidden)
        {
            animator.SetTrigger(ANIMATION_HIDE);
        }
    }

    private void Update()
    {
        if (!hidden && Input.GetKeyDown(config.DialogueSkipKey)) {
            if (animatingText) {
                animatingText = false;
                txtDialogue.text = fullMessage;
            } else {
                HideOrSkip();
            }
        }
        if (animatingText && !hidden)
        {
            textAnimationTimer += Time.unscaledDeltaTime;
            if (textAnimationTimer > 1f / textAnimationSpeed)
            {
                string dialogueText = txtDialogue.text;
                dialogueText = fullMessage.Substring(0, dialogueText.Length + 1);
                if (fullMessage == dialogueText)
                {
                    animatingText = false;
                }
                textAnimationTimer = 0f;
                txtDialogue.text = dialogueText;
            }
        }
    }

    public void Initialize()
    {
        animator = GetComponent<Animator>();
        config = GameManager.main.Config;
        imgBorder.color = config.UIBorderColor;
        imgBackGround.color = config.UIBackgroundColor;
        txtDialogue.color = config.UIForegroundColor;
        textAnimationSpeed = config.UITextAnimationSpeed;
        txtDialogue.text = "";
    }



}
