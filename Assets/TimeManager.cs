using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    [SerializeField]
    private TMP_Text minutesText;

    [SerializeField]
    private TMP_Text secondsText;

    private float startTime;

    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        float totalTime = Time.time - startTime;

        int minutes = (int)(totalTime / 60);
        int seconds = (int)(totalTime % 60);

        minutesText.text = minutes.ToString();

        if(seconds < 10) {
            secondsText.text = "0\n" + seconds.ToString();
        } else {
            secondsText.text = seconds.ToString().Substring(0, 1) + "\n" + seconds.ToString().Substring(1, 1);
        }
    }
}
