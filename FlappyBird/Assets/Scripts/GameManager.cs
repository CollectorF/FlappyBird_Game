using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private PlayerController playerController;
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
    private bool isGameRunning = false;
    private float timeRemaning;
    private Coroutine pipeSpawn;


    private void Start()
    {
        isGameRunning = true;
        playerController.OnResetGame += StopGame;
        pipeSpawn =  StartCoroutine(SpawnPipes());
    }

    private void Update()
    {
        timeRemaning -= Time.deltaTime;
    }

    private void StopGame()
    {
        isGameRunning = false;
        Debug.Log("Game over!");
        StopCoroutine(levelManager.levelRoll);
        StopCoroutine(pipeSpawn);
    }

    private void StartGame()
    {

    }

    private IEnumerator SpawnPipes()
    {
        while (true)
        {
            if (timeRemaning <= 0)
            {
                generatedPoint = new Vector3(spawnPosX, Random.Range(minPipeHeight, maxPipeHeight), 0);
                GameObject newPipe = Instantiate(pipePrefab, generatedPoint, Quaternion.identity);
                newPipe.transform.SetParent(Level.transform);
            }
            yield return new WaitForSeconds(timeRemaning = spawnTime);
        }
    }
}
