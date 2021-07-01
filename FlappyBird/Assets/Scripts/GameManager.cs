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
    private float spawnTime = 1f;
    [SerializeField]
    private float spawnPosX = 12f;
    [SerializeField]
    private GameObject pipePrefab;
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
    private float timeRemaning;
    private Coroutine pipeSpawn;
    private Coroutine levelRoll;
    private List<GameObject> pipes = new List<GameObject>();

    private void Start()
    {
        uiManager.OnTap += StartGame;
        uiManager.OnRestart += StartGame;
        playerController.OnStopGame += StopGame;
    }

    private void Update()
    {
        timeRemaning -= Time.deltaTime;
    }

    private void StartGame()
    {
        playerController.ResetGame();
        pipeSpawn = StartCoroutine(SpawnPipes());
        levelRoll = StartCoroutine(levelManager.RollCoroutine());
        uiManager.OnTap -= StartGame;
        foreach (var pipe in pipes)
        {
            Destroy(pipe);
        }
        score = 0;
    }

    private void StopGame()
    {
        StopCoroutine(levelRoll);
        StopCoroutine(pipeSpawn);
        if (score > bestScore)
        {
            bestScore = score;
        }
        uiManager.OnGameEnd(score, bestScore, bronzeMedalScore, silverMedalScore, goldMedalScore, platinumMedalScore);
    }

    private int ScoreCount()
    {
        score++;
        uiManager.UpdateScore(score);
        return score;
    }

    private IEnumerator SpawnPipes()
    {
        var width = Camera.main.orthographicSize * 2;
        while (true)
        {
            if (timeRemaning <= 0)
            {
                generatedPoint = new Vector3(spawnPosX, UnityEngine.Random.Range(minPipeHeight, maxPipeHeight), 0);
                GameObject newPipe = Instantiate(pipePrefab, generatedPoint, Quaternion.identity);
                newPipe.transform.SetParent(Level.transform);
                pipes.Add(newPipe);
                PipeTrigger newPipeScript = newPipe.GetComponent<PipeTrigger>();
                newPipeScript.OnTrigger += ScoreCount;
            }
            yield return new WaitForSeconds(timeRemaning = spawnTime);
        }
    }
}
