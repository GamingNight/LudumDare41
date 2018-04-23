using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{

    float timeLeft = 170f;
    public Text chrono;

    void Update()
    {
        timeLeft -= Time.deltaTime;
        int minutes = (int)timeLeft / 60;
        int secondes = (int)timeLeft % 60;
        string secondesStr = secondes + "";
        if (secondes < 10)
            secondesStr = "0" + secondesStr;
        chrono.text = "0" + minutes + ":" + secondesStr;

        if (timeLeft <= 0)
        {
            GameManager.GetInstance().GameOver(EndGameStats.GameOverType.TIME_OUT);
        }
    }
}