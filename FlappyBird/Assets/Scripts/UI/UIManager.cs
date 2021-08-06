using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private GameObject startGameUI;
    [SerializeField]
    private GameObject gameplayUI;
    [SerializeField]
    private GameObject gameResultUI;
    [SerializeField]
    private GameObject finalScore;
    [SerializeField]
    private GameObject bestScore;
    [SerializeField]
    private Image medal;
    [SerializeField]
    private GameObject scoreboard;
    [SerializeField]
    private Button playButton;

    private TMP_Text scoreTMP;
    private TMP_Text finalScoreTMP;
    private TMP_Text bestScoreTMP;
    private ButtonAnimation buttonAnimation;
    private ScoreboardAnimation scoreboardAnimation;
    private ScoreboardMedals scoreboardMedals;

    internal event Action OnTap;
    internal event Action OnRestart;

    private void Start()
    {
        startGameUI.SetActive(true);
        gameplayUI.SetActive(false);
        gameResultUI.SetActive(false);
        scoreTMP = gameplayUI.GetComponentInChildren<TMP_Text>();
        finalScoreTMP = finalScore.GetComponent<TMP_Text>();
        bestScoreTMP = bestScore.GetComponent<TMP_Text>();
        buttonAnimation = playButton.GetComponent<ButtonAnimation>();
        scoreboardAnimation = scoreboard.GetComponent<ScoreboardAnimation>();
        scoreboardMedals = scoreboard.GetComponent<ScoreboardMedals>();
    }

    public void OnTapToPlay()
    {
        if (gameResultUI.activeSelf == false)
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

    internal void OnGameEnd(int score, int bestScore)
    {
        gameplayUI.SetActive(false);
        gameResultUI.SetActive(true);
        medal.enabled = false;
        finalScoreTMP.text = score.ToString();
        bestScoreTMP.text = bestScore.ToString();
        medal = scoreboardMedals.SetMedal(score);
        playButton.enabled = false;
        Sequence sequence = DOTween.Sequence()
            .Append(scoreboardAnimation.BoardAnimation())
            .Append(buttonAnimation.PlayButtonAnimation())
            .AppendCallback(() => { playButton.enabled = true; });
    }
    public void OnRestartGame()
    {
        OnRestart?.Invoke();
        gameResultUI.SetActive(false);
        gameplayUI.SetActive(true);
        UpdateScore(0);
    }
}