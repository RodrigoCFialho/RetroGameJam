using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ButtonHandler : MonoBehaviour
{
    [SerializeField] private Color regularText = Color.white;
    [SerializeField] private Color textHighlight = Color.yellow;

    [SerializeField] private TMP_Text buttonText;

    public void HighlightButton()
    {
        buttonText.color = textHighlight;
    }

    public void UnhighlightButton()
    {
        buttonText.color = regularText;
    }
}
