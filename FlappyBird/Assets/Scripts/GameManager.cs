using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private PlayerController playerController;

    private int score = 0;
    private bool isGameRunning = false;

    private void Start()
    {
        isGameRunning = true;
        playerController.OnResetGame += StopGame;
    }

    private void StopGame()
    {
        isGameRunning = false;
        Debug.Log("Game over!");
    }
}
