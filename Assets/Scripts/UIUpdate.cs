using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIUpdate : MonoBehaviour
{
    public TMP_Text killsCounter;
    public TMP_Text health;
    public TMP_Text finalKillsNumber;
    public GameObject restartButton;
    public GameObject quitButton;
    public GameObject finalScreen;
    private int killsNumber;

    public void UpdateDeathCounter(int newKillsNumber)
    {
        killsNumber += newKillsNumber;
        killsCounter.SetText("Kills: " + killsNumber);
    }

    public void UpdateHealth(int newHealthNumber)
    {
        health.SetText("Health: " + newHealthNumber);
    }

    public void DeathScreen()
    {
        killsCounter.gameObject.SetActive(false);
        health.gameObject.SetActive(false);
        finalKillsNumber.SetText("Total kills: " + killsNumber);
        finalKillsNumber.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
        quitButton.gameObject.SetActive(true);
        finalScreen.gameObject.SetActive(true);
    }
}
