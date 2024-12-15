using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OptionsMenuButtonHandler : MonoBehaviour
{
    [SerializeField]
    private Color textHighlight;

    [SerializeField]
    private Color regularText;

    [SerializeField]
    private TMP_Text crtText;

    [SerializeField]
    private TMP_Text backText;

    [SerializeField]
    private GameObject mainMenu;

    [SerializeField]
    private FullScreenPassRendererFeature crtEffect;

    private string crtTextOn = "CRT Effect: On";
    private string crtTextOff = "CRT Effect: Off";

    private void Start() {
        crtEffect.SetActive(false);
        crtText.text = crtTextOff;
    }

    public void ToggleCRT() {
        if(crtEffect.isActive) {
            crtEffect.SetActive(false);
            crtText.text = crtTextOff;
        } else {
            crtEffect.SetActive(true);
            crtText.text = crtTextOn;
        }
    }

    public void Back() {
        gameObject.SetActive(false);
        mainMenu.SetActive(true);
    }

    public void HighlightCrt() {
        crtText.color = textHighlight;
    }

    public void UnhighlightCrt() {
        crtText.color = regularText;
    }

    public void HighlightBack() {
        backText.color = textHighlight;
    }

    public void UnhighlightBack() {
        backText.color = regularText;
    }
}
