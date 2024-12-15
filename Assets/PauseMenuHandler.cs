using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuHandler : MonoBehaviour
{
    [SerializeField]
    private Color textHighlight;

    [SerializeField]
    private Color regularText;

    [SerializeField]
    private TMP_Text resumeText;

    [SerializeField]
    private TMP_Text quitText;

    public void Resume() {
        GameManager.Instance.PauseGame();
    }

    public void Quit() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void HighlightResume() {
        resumeText.color = textHighlight;
    }

    public void UnhighlightResume() {
        resumeText.color = regularText;
    }

    public void HighlightQuit() {
        quitText.color = textHighlight;
    }

    public void UnhighlightQuit() {
        quitText.color = regularText;
    }
}
