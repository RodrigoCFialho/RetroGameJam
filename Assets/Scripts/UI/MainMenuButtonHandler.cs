using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButtonHandler : MonoBehaviour
{
    [SerializeField] private TMP_Text crtText;

    [SerializeField] private FullScreenPassRendererFeature crtEffect;

    private string crtTextOn = "CRT Effect: On";
    private string crtTextOff = "CRT Effect: Off";

    private void Start()
    {
        crtEffect.SetActive(false);
        crtText.text = crtTextOff;
    }

    public void Play() 
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Quit() 
    {
        Application.Quit();
    }

    public void ToggleCRT()
    {
        if (crtEffect.isActive)
        {
            crtEffect.SetActive(false);
            crtText.text = crtTextOff;
        }
        else
        {
            crtEffect.SetActive(true);
            crtText.text = crtTextOn;
        }
    }
}
