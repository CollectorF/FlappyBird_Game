using System.Collections;
using System.Collections.Generic;
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

    private Vector3 generatedPoint;
    private int score = 0;
    private int bestScore = 0;
    private bool isGameRunning = false;
    private float timeRemaning;
    private Coroutine pipeSpawn;

    private void Start()
    {
        uiManager.OnTap += StartGame;
        playerController.OnResetGame += StopGame;

    }

    private void Update()
    {
        timeRemaning -= Time.deltaTime;
    }

    private void StartGame()
    {
        isGameRunning = true;
        playerController.Reset();
        pipeSpawn = StartCoroutine(SpawnPipes());
    }

    private void StopGame()
    {
        isGameRunning = false;
        Debug.Log("Game over!");
        levelManager.StopCoroutine(levelManager.levelRoll);
        StopCoroutine(pipeSpawn);
        Debug.Log("Score: " + score);
    }

    private int ScoreCount()
    {
        return score++;
    }

    private IEnumerator SpawnPipes()
    {
        var width = Camera.main.orthographicSize * 2;
        while (true)
        {
            if (timeRemaning <= 0)
            {
                generatedPoint = new Vector3(spawnPosX, Random.Range(minPipeHeight, maxPipeHeight), 0);
                GameObject newPipe = Instantiate(pipePrefab, generatedPoint, Quaternion.identity);
                newPipe.transform.SetParent(Level.transform);
                PipeTrigger newPipeScript = newPipe.GetComponent<PipeTrigger>();
                newPipeScript.OnTrigger += ScoreCount;
            }
            yield return new WaitForSeconds(timeRemaning = spawnTime);
        }
    }
}
