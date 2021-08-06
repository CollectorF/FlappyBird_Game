using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private PlayerController playerController;
    [SerializeField]
    private UIManager uiManager;
    [SerializeField]
    private PipeTrigger pipeTrigger;
    [SerializeField]
    private LevelManager levelManager;
    [SerializeField]
    private GameObject Level;
    [SerializeField]
    private float minPipeHeight = -0.5f;
    [SerializeField]
    private float maxPipeHeight = 1.2f;
    [SerializeField]
    private float pipeSpawnTime = 1.6f;
    [SerializeField]
    private GameObject pipePrefab;
    [SerializeField]
    private GameObject bonusPrefab;
    [SerializeField]
    private float minBonusHeight = 1f;
    [SerializeField]
    private float maxBonusHeight = 0f;
    [SerializeField]
    private float bonusSpawnPosX = 20f;
    [SerializeField]
    private float bonusSpawnTime = 5f;
    [SerializeField]
    private int scoreForPipePass = 1;
    [SerializeField]
    [Min(0)]
    private int bronzeMedalScore;
    [SerializeField]
    [Min(0)]
    private int silverMedalScore;
    [SerializeField]
    [Min(0)]
    private int goldMedalScore;
    [SerializeField]
    [Min(0)]
    private int platinumMedalScore;

    private Vector3 generatedPoint;
    private int score = 0;
    private int bestScore = 0;
    private float timeRemaningToSpawnPipes;
    private float timeRemaningToSpawnBonus;
    private Coroutine pipeSpawn;
    private Coroutine bonusSpawn;
    private Coroutine levelRoll;
    private List<GameObject> pipes = new List<GameObject>();
    private GameObject bonus;
    private List<GameObject> bonuses = new List<GameObject>();

    private void Start()
    {
        uiManager.OnTap += StartGame;
        uiManager.OnRestart += StartGame;
        playerController.OnStopGame += StopGame;
    }

    private void Update()
    {
        timeRemaningToSpawnPipes -= Time.deltaTime;
        timeRemaningToSpawnBonus -= Time.deltaTime;
    }

    private void StartGame()
    {
        playerController.ResetGame();
        pipeSpawn = StartCoroutine(SpawnPipes());
        bonusSpawn = StartCoroutine(SpawnBonus());
        levelRoll = StartCoroutine(levelManager.RollCoroutine());
        uiManager.OnTap -= StartGame;
        foreach (var pipe in pipes)
        {
            Destroy(pipe);
        }
        foreach (var bonus in bonuses)
        {
            Destroy(bonus);
        }
        score = 0;
    }

    private void StopGame()
    {
        StopCoroutine(levelRoll);
        StopCoroutine(pipeSpawn);
        StopCoroutine(bonusSpawn);
        if (score > bestScore)
        {
            bestScore = score;
        }
        uiManager.OnGameEnd(score, bestScore);
    }

    private int ScoreCount(int addToScore)
    {
        // Maybe there are more straight ways to pass a 'scoreForPipePass' int parameter to 'ScoreCount' method,
        // but I didn't get them...

        if (addToScore == 0)
        {
            addToScore = scoreForPipePass;
        }
        score += addToScore;
        uiManager.UpdateScore(score);
        return score;
    }

    // String parameter is used for the possibility of having more bonuses implementations with other tags to identify them 
    private void OnBonus(string str, int addToScore)
    {
        if (str == "ScoreBonus")
        {
            ScoreCount(addToScore);
        }
    }

    private IEnumerator SpawnPipes()
    {
        var width = Camera.main.orthographicSize * 2;
        while (true)
        {
            if (timeRemaningToSpawnPipes <= 0)
            {
                generatedPoint = new Vector3(width, UnityEngine.Random.Range(minPipeHeight, maxPipeHeight), 0);
                GameObject newPipe = Instantiate(pipePrefab, generatedPoint, Quaternion.identity);
                newPipe.transform.SetParent(Level.transform);
                pipes.Add(newPipe);
                PipeTrigger newPipeScript = newPipe.GetComponent<PipeTrigger>();
                newPipeScript.OnTrigger += ScoreCount;
            }
            yield return new WaitForSeconds(timeRemaningToSpawnPipes = pipeSpawnTime);
        }
    }

    private IEnumerator SpawnBonus()
    {
        while (true)
        {
            if (timeRemaningToSpawnBonus <= 0)
            {
                generatedPoint = new Vector3(bonusSpawnPosX, UnityEngine.Random.Range(minBonusHeight, maxBonusHeight), 0);
                bonus = Instantiate(bonusPrefab, generatedPoint, Quaternion.identity);
                bonus.transform.SetParent(Level.transform);
                bonus.tag = "ScoreBonus";
                bonuses.Add(bonus);
                ScoreBonus bonusScript = bonus.GetComponent<ScoreBonus>();
                bonusScript.OnBonusTrigger += OnBonus;
            }
            yield return new WaitForSeconds(timeRemaningToSpawnBonus = bonusSpawnTime);
        }
    }
}
