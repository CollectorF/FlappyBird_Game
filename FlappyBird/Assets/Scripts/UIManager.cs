using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private GameObject startGameUI;
    [SerializeField]
    private GameObject gameplayUI;
    [SerializeField]
    private GameObject gameresultUI;
    [SerializeField]
    private GameObject finalScore;
    [SerializeField]
    private GameObject bestScore;
    [SerializeField]
    private Image medal;
    [SerializeField]
    private Sprite bronzeMedal;
    [SerializeField]
    private Sprite silverMedal;
    [SerializeField]
    private Sprite goldMedal;
    [SerializeField]
    private Sprite platinumMedal;

    private TMPro.TMP_Text scoreTMP;
    private TMPro.TMP_Text finalScoreTMP;
    private TMPro.TMP_Text bestScoreTMP;

    internal event Action OnTap;
    internal event Action OnRestart;

    private void Start()
    {
        startGameUI.SetActive(true);
        gameplayUI.SetActive(false);
        gameresultUI.SetActive(false);
        scoreTMP = gameplayUI.GetComponentInChildren<TMPro.TMP_Text>();
        finalScoreTMP = finalScore.GetComponent<TMPro.TMP_Text>();
        bestScoreTMP = bestScore.GetComponent<TMPro.TMP_Text>();
    }

    public void OnTapToPlay()
    {
        if (gameresultUI.activeSelf == false)
        {
            OnTap?.Invoke();
            startGameUI.SetActive(false);
            gameplayUI.SetActive(true);
        }
    }
    public void UpdateScore(int score)
    {
        scoreTMP.text = score.ToString();
    }

    public void OnGameEnd(int score, int bestScore, int bronzeMedalScore, int silverMedalScore, int goldMedalScore, int platinumMedalScore)
    {
        gameplayUI.SetActive(false);
        gameresultUI.SetActive(true);
        medal.enabled = false;
        finalScoreTMP.text = score.ToString();
        bestScoreTMP.text = bestScore.ToString();
        if (score >= bronzeMedalScore)
        {
            medal.enabled = true;
            medal.sprite = bronzeMedal;
        }
        if (score >= silverMedalScore)
        {
            medal.sprite = silverMedal;
        }
        if (score >= goldMedalScore)
        {
            medal.sprite = goldMedal;
        }
        if (score >= platinumMedalScore)
        {
            medal.sprite = platinumMedal;
        }
    }
    public void OnRestartGame()
    {
        OnRestart?.Invoke();
        gameresultUI.SetActive(false);
        gameplayUI.SetActive(true);
        UpdateScore(0);
    }
}