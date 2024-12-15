using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButtonHandler : MonoBehaviour
{
    [SerializeField]
    private Color textHighlight;

    [SerializeField]
    private Color regularText;

    [SerializeField]
    private TMP_Text playText;

    [SerializeField]
    private TMP_Text optionsText;

    [SerializeField]
    private TMP_Text creditsText;

    [SerializeField]
    private TMP_Text quitText;

    [SerializeField]
    private GameObject optionsMenu;

    [SerializeField]
    private GameObject creditsMenu;

    public void Play() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Options() {
        gameObject.SetActive(false);
        optionsMenu.SetActive(true);
    }

    public void Credits() {
        gameObject.SetActive(false);
        creditsMenu.SetActive(true);
    }

    public void Quit() {
        Application.Quit();
    }

    public void HighlightPlay() {
        playText.color = textHighlight;
    }

    public void UnhighlightPlay() {
        playText.color = regularText;
    }

    public void HighlightOptions() {
        optionsText.color = textHighlight;
    }

    public void UnhighlightOptions() {
        optionsText.color = regularText;
    }

    public void HighlightCredits() {
        creditsText.color = textHighlight;
    }

    public void UnhighlightCredits() {
        creditsText.color = regularText;
    }

    public void HighlightQuit() {
        quitText.color = textHighlight;
    }

    public void UnhighlightQuit() {
        quitText.color = regularText;
    }
}
